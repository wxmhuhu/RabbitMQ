using MicroServices.Application.Services.AI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace MicroServices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AiController : ControllerBase
    {
        private readonly CozeApiService _cozeApiService;
        private readonly ILogger<AiController> _logger;
        private readonly CozeStreamApiService _cozeStreamApiService;

        public AiController(CozeApiService cozeApiService, ILogger<AiController> logger, CozeStreamApiService cozeStreamApiService)
        {
            _cozeApiService = cozeApiService;
            _logger = logger;
            _cozeStreamApiService = cozeStreamApiService;
        }

        public class AiRequestDto
        {
            public string Message { get; set; }
            public string UserId { get; set; } = "default_user";
            public string ConversationId { get; set; }
        }

        /// <summary>
        /// 处理AI查询请求的非流式方法
        /// 路由：POST /api/Ai/Ask
        /// </summary>
        [HttpPost("Ask")]
        public async Task<IActionResult> AskAi([FromBody] AiRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Message))
            {
                _logger.LogWarning("Received an AI query with empty message from user: {UserId}", request.UserId);
                return BadRequest("消息内容不能为空。");
            }

            _logger.LogInformation("Received AI query from user {UserId}: {Message}", request.UserId, request.Message);

            try
            {
                string aiResponse = await _cozeApiService.GetAiResponseAsync(request.Message, request.UserId, request.ConversationId);
                return Ok(new { Response = aiResponse, ConversationId = request.ConversationId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing AI request from user {UserId}. Message: {Message}", request.UserId, request.Message);
                return StatusCode(500, $"AI服务处理失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 处理流式AI查询请求 - Server-Sent Events
        /// 路由：POST /api/Ai/AskStream
        /// </summary>
        [HttpPost("AskStream")]
        public async Task AskStream([FromBody] AiRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Message))
            {
                _logger.LogWarning("Received streaming AI query with empty message from user: {UserId}", request.UserId);
                Response.StatusCode = 400;
                await Response.WriteAsync("消息内容不能为空。");
                return;
            }

            _logger.LogInformation("Received streaming AI query from user {UserId}: {Message}", request.UserId, request.Message);

            try
            {
                Response.Headers.Add("Content-Type", "text/event-stream");
                Response.Headers.Add("Cache-Control", "no-cache");
                Response.Headers.Add("Connection", "keep-alive");
                Response.Headers.Add("Access-Control-Allow-Origin", "*");

                await foreach (var chunk in _cozeStreamApiService.GetAiResponseStreamAsync(request.Message, request.UserId, request.ConversationId))
                {
                    string sseData = $"data: {JsonSerializer.Serialize(new { type = chunk.Type.ToString(), content = chunk.Content, conversationId = chunk.ConversationId, timestamp = chunk.Timestamp })}\n\n";

                    await Response.WriteAsync(sseData);
                    await Response.Body.FlushAsync();

                    if (chunk.Type == StreamChunkType.Done || chunk.Type == StreamChunkType.Error)
                    {
                        break;
                    }
                }

                await Response.WriteAsync("data: [DONE]\n\n");
                await Response.Body.FlushAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing streaming AI request from user {UserId}", request.UserId);
                string errorData = $"data: {JsonSerializer.Serialize(new { type = "Error", content = $"AI服务处理失败: {ex.Message}", timestamp = DateTime.UtcNow })}\n\n";
                await Response.WriteAsync(errorData);
                await Response.Body.FlushAsync();
            }
        }

        /// <summary>
        /// 处理流式AI查询请求 - 回调方式
        /// 路由：POST /api/Ai/AskCallback
        /// </summary>
        [HttpPost("AskCallback")]
        public async Task<IActionResult> AskStreamCallback([FromBody] AiRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Message))
            {
                _logger.LogWarning("Received streaming callback AI query with empty message from user: {UserId}", request.UserId);
                return BadRequest("消息内容不能为空。");
            }

            _logger.LogInformation("Received streaming callback AI query from user {UserId}: {Message}", request.UserId, request.Message);

            try
            {
                var chunks = new List<object>();

                var result = await _cozeStreamApiService.GetAiResponseStreamWithCallbackAsync(
                    request.Message,
                    request.UserId,
                    async (chunk) =>
                    {
                        chunks.Add(new
                        {
                            type = chunk.Type.ToString(),
                            content = chunk.Content, 
                            timestamp = chunk.Timestamp
                        });
                        _logger.LogInformation($"Received chunk [{chunk.Type}]: {chunk.Content}");
                    },
                    request.ConversationId);

                return Ok(new
                {
                    success = result.IsSuccess,
                    response = result.FullResponse,
                    conversationId = result.ConversationId,
                    duration = result.Duration.TotalMilliseconds,
                    chunks = chunks,
                    error = result.ErrorMessage
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing streaming callback AI request from user {UserId}", request.UserId);
                return StatusCode(500, $"AI服务处理失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 获取流式服务状态
        /// 路由：GET /api/Ai/Status
        /// </summary>
        [HttpGet("Status")]
        public IActionResult GetStatus()
        {
            return Ok(new
            {
                service = "CozeStreamApiService",
                status = "Running",
                timestamp = DateTime.UtcNow,
                version = "1.0.0"
            });
        }
    }
}