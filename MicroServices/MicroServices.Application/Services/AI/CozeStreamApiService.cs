using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace MicroServices.Application.Services.AI
{
    public class CozeStreamApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _cozeApiKey;
        private readonly string _cozeBotId;
        private readonly string _cozeApiEndpoint;
        private readonly ILogger<CozeStreamApiService> _logger;

        public CozeStreamApiService(HttpClient httpClient, IConfiguration configuration, ILogger<CozeStreamApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;

            _cozeApiKey = configuration["CozeSettings:ApiKey"] ??
                          throw new ArgumentNullException("CozeSettings:ApiKey not found in configuration.");
            _cozeBotId = configuration["CozeSettings:BotId"] ??
                         throw new ArgumentNullException("CozeSettings:BotId not found in configuration.");
            _cozeApiEndpoint = configuration["CozeSettings:ApiEndpoint"] ??
                               throw new ArgumentNullException("CozeSettings:ApiEndpoint not found in configuration.");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_cozeApiKey}");
            _httpClient.DefaultRequestHeaders.Add("Accept", "text/event-stream");

            _logger.LogInformation($"CozeStreamApiService initialized with API key: {_cozeApiKey?.Substring(0, Math.Min(10, _cozeApiKey.Length))}...");
        }

        /// <summary>
        /// 流式：发送消息到扣子AI并实时获取回复
        /// </summary>
        public async IAsyncEnumerable<StreamChunk> GetAiResponseStreamAsync(string userMessage, string userId, string conversationId = null)
        {
            if (string.IsNullOrEmpty(conversationId))
            {
                conversationId = Guid.NewGuid().ToString();
                _logger.LogInformation($"New streaming conversation started with ID: {conversationId}");
            }

            await foreach (var chunk in GetStreamChunksAsync(userMessage, userId, conversationId))
            {
                yield return chunk;
            }
        }

        private async IAsyncEnumerable<StreamChunk> GetStreamChunksAsync(string userMessage, string userId, string conversationId)
        {
            var requestBody = CreateStreamRequest(userMessage, userId, conversationId);
            var jsonBody = JsonSerializer.Serialize(requestBody, new JsonSerializerOptions { WriteIndented = true });
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            _logger.LogInformation($"发送流式请求: {jsonBody}");

            using var request = new HttpRequestMessage(HttpMethod.Post, _cozeApiEndpoint);
            request.Headers.Add("Authorization", $"Bearer {_cozeApiKey}");
            request.Headers.Add("Accept", "text/event-stream");
            request.Content = content;

            // 发送请求并获取响应
            var responseResult = await SendRequestSafelyAsync(request);
            if (!responseResult.IsSuccess)
            {
                yield return new StreamChunk
                {
                    Type = StreamChunkType.Error,
                    Content = responseResult.ErrorMessage,
                    ConversationId = conversationId
                };
                yield break;
            }

            // 处理响应流
            using (responseResult.Response)
            {
                var streamResult = await GetStreamReaderSafelyAsync(responseResult.Response);
                if (!streamResult.IsSuccess)
                {
                    yield return new StreamChunk
                    {
                        Type = StreamChunkType.Error,
                        Content = streamResult.ErrorMessage,
                        ConversationId = conversationId
                    };
                    yield break;
                }

                using (streamResult.Stream)
                using (streamResult.Reader)
                {
                    await foreach (var chunk in ProcessStreamResponseSafely(streamResult.Reader, conversationId))
                    {
                        yield return chunk;
                    }
                }
            }
        }

        private async Task<RequestResult> SendRequestSafelyAsync(HttpRequestMessage request)
        {
            try
            {
                var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                return new RequestResult { IsSuccess = true, Response = response };
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"流式AI请求失败: {e.Message}");
                return new RequestResult { IsSuccess = false, ErrorMessage = $"AI服务发生错误: {e.Message}" };
            }
        }

        private async Task<StreamReaderResult> GetStreamReaderSafelyAsync(HttpResponseMessage response)
        {
            try
            {
                var stream = await response.Content.ReadAsStreamAsync();
                var reader = new StreamReader(stream);
                return new StreamReaderResult { IsSuccess = true, Stream = stream, Reader = reader };
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"获取响应流失败: {e.Message}");
                return new StreamReaderResult { IsSuccess = false, ErrorMessage = $"获取响应流失败: {e.Message}" };
            }
        }

        private async IAsyncEnumerable<StreamChunk> ProcessStreamResponseSafely(StreamReader reader, string conversationId)
        {
            string line;
            while (true)
            {
                var readResult = await ReadLineSafelyAsync(reader);
                if (!readResult.IsSuccess)
                {
                    yield return new StreamChunk
                    {
                        Type = StreamChunkType.Error,
                        Content = readResult.ErrorMessage,
                        ConversationId = conversationId
                    };
                    yield break;
                }

                line = readResult.Line;
                if (line == null) break;

                // 减少日志输出，只记录重要信息
                if (line.StartsWith("event:"))
                {
                    var eventType = line.Substring(6);
                    _logger.LogDebug($"收到事件: '{eventType}'");

                    if (eventType == "conversation.chat.completed")
                    {
                        yield return new StreamChunk
                        {
                            Type = StreamChunkType.MessageCompleted,
                            Content = "",
                            ConversationId = conversationId
                        };
                    }
                    continue;
                }

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                if (line.StartsWith("data:"))
                {
                    var jsonData = line.Substring(5);

                    if (jsonData == "\"[DONE]\"" || jsonData == "[DONE]")
                    {
                        _logger.LogInformation("流式响应完成");
                        yield return new StreamChunk
                        {
                            Type = StreamChunkType.Done,
                            Content = "",
                            ConversationId = conversationId
                        };
                        break;
                    }

                    // 解析内容并逐字符发送
                    var content = ParseContentFromJson(jsonData);
                    if (!string.IsNullOrEmpty(content))
                    {
                        foreach (char c in content)
                        {
                            yield return new StreamChunk
                            {
                                Type = StreamChunkType.Content,
                                Content = c.ToString(),
                                ConversationId = conversationId
                            };

                            // 添加小延迟模拟打字效果
                            await Task.Delay(30);
                        }
                    }
                }
            }
        }

        private string ParseContentFromJson(string jsonData)
        {
            try
            {
                using (JsonDocument doc = JsonDocument.Parse(jsonData))
                {
                    if (doc.RootElement.TryGetProperty("type", out JsonElement typeElement))
                    {
                        var messageType = typeElement.GetString();

                        if (messageType == "answer" && doc.RootElement.TryGetProperty("content", out JsonElement contentElement))
                        {
                            var content = contentElement.GetString();
                            if (!string.IsNullOrEmpty(content) && content != "~")
                            {
                                return content;
                            }
                        }
                    }
                }
            }
            catch (JsonException ex)
            {
                _logger.LogWarning($"解析JSON失败: {ex.Message}");
            }
            return null;
        }
      
        private async Task<ReadLineResult> ReadLineSafelyAsync(StreamReader reader)
        {
            try
            {
                var line = await reader.ReadLineAsync();
                return new ReadLineResult { IsSuccess = true, Line = line };
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"读取流数据失败: {e.Message}");
                return new ReadLineResult { IsSuccess = false, ErrorMessage = $"读取流数据失败: {e.Message}" };
            }
        }

        private class ReadLineResult
        {
            public bool IsSuccess { get; set; }
            public string Line { get; set; }
            public string ErrorMessage { get; set; }
        }

        /// <summary>
        /// 流式：通过回调函数处理实时回复
        /// </summary>
        public async Task<StreamResult> GetAiResponseStreamWithCallbackAsync(
            string userMessage,
            string userId,
            Func<StreamChunk, Task> onChunkReceived,
            string conversationId = null)
        {
            var fullResponse = new StringBuilder();
            var result = new StreamResult
            {
                ConversationId = conversationId,
                StartTime = DateTime.UtcNow
            };

            try
            {
                await foreach (var chunk in GetAiResponseStreamAsync(userMessage, userId, conversationId))
                {
                    result.ConversationId = chunk.ConversationId;

                    if (chunk.Type == StreamChunkType.Content)
                    {
                        fullResponse.Append(chunk.Content);
                    }

                    await onChunkReceived(chunk);

                    if (chunk.Type == StreamChunkType.Error)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessage = chunk.Content;
                        break;
                    }
                    else if (chunk.Type == StreamChunkType.Done)
                    {
                        result.IsSuccess = true;
                        break;
                    }
                }

                result.FullResponse = fullResponse.ToString();
                result.EndTime = DateTime.UtcNow;
                result.Duration = result.EndTime - result.StartTime;

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"流式回调AI请求失败: {e.Message}");
                result.IsSuccess = false;
                result.ErrorMessage = e.Message;
                result.EndTime = DateTime.UtcNow;
                result.Duration = result.EndTime - result.StartTime;
                return result;
            }
        }

        private Dictionary<string, object> CreateStreamRequest(string userMessage, string userId, string conversationId)
        {
            var userMessageObject = new
            {
                role = "user",
                content = userMessage,
                content_type = "text"
            };

            return new Dictionary<string, object>
            {
                ["conversation_id"] = conversationId,
                ["bot_id"] = _cozeBotId,
                ["user_id"] = userId,
                ["stream"] = true,
                ["auto_save_history"] = true,
                ["additional_messages"] = new[] { userMessageObject }
            };
        }

        // 辅助类
        private class RequestResult
        {
            public bool IsSuccess { get; set; }
            public HttpResponseMessage Response { get; set; }
            public string ErrorMessage { get; set; }
        }

        private class StreamReaderResult
        {
            public bool IsSuccess { get; set; }
            public Stream Stream { get; set; }
            public StreamReader Reader { get; set; }
            public string ErrorMessage { get; set; }
        }
    }

    // 流式响应数据块
    public class StreamChunk
    {
        public StreamChunkType Type { get; set; }
        public string Content { get; set; }
        public string ConversationId { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

    // 流式响应类型
    public enum StreamChunkType
    {
        Content,
        MessageCompleted,
        Done,
        Error
    }

    // 流式响应结果
    public class StreamResult
    {
        public bool IsSuccess { get; set; }
        public string FullResponse { get; set; }
        public string ConversationId { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration { get; set; }
    }
}