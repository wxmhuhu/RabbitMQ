using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MicroServices.Application.Services.AI
{
    public class CozeApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _cozeApiKey;
        private readonly string _cozeBotId;
        private readonly string _cozeApiEndpoint;
        private readonly ILogger<CozeApiService> _logger;

        // 构造函数：通过依赖注入获取 HttpClient、配置和日志器
        public CozeApiService(HttpClient httpClient, IConfiguration configuration, ILogger<CozeApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;

            // 从 appsettings.json 中读取配置
            // 如果配置项不存在，则抛出异常，提醒你需要配置
            _cozeApiKey = configuration["CozeSettings:ApiKey"] ??
                          throw new ArgumentNullException("CozeSettings:ApiKey not found in configuration. Please check appsettings.json.");
            _cozeBotId = configuration["CozeSettings:BotId"] ??
                         throw new ArgumentNullException("CozeSettings:BotId not found in configuration. Please check appsettings.json.");
            _cozeApiEndpoint = configuration["CozeSettings:ApiEndpoint"] ??
                               throw new ArgumentNullException("CozeSettings:ApiEndpoint not found in configuration. Please check appsettings.json.");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_cozeApiKey}");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            _logger.LogInformation($"设置的Authorization头: Bearer {_cozeApiKey?.Substring(0, Math.Min(10, _cozeApiKey.Length))}...");

            // 检查认证头是否正确设置
            var authHeader = _httpClient.DefaultRequestHeaders.Authorization;
            _logger.LogInformation($"认证头: {authHeader?.Scheme} {authHeader?.Parameter?.Substring(0, Math.Min(10, authHeader.Parameter?.Length ?? 0))}...");

        }

        /// <summary>
        /// 非流式：发送消息到扣子AI并轮询获取完整回复
        /// </summary>
        public async Task<string> GetAiResponseAsync(string userMessage, string userId, string conversationId = null)
        {
            if (string.IsNullOrEmpty(conversationId))
            {
                conversationId = Guid.NewGuid().ToString();
                _logger.LogInformation($"New conversation started with ID: {conversationId}");
            }

            try
            {
                // 发送初始请求
                var response = await SendChatRequestWithFullResponseAsync(userMessage, userId, conversationId);
                if (response == null)
                {
                    return "发送请求失败";
                }

                // 从响应中获取chat_id和实际的conversation_id
                var chatId = response.ChatId;
                var actualConversationId = response.ConversationId ?? conversationId;

                _logger.LogInformation($"使用ChatId: {chatId}, ConversationId: {actualConversationId}");

                if (string.IsNullOrEmpty(chatId))
                {
                    return "发送请求失败";
                }

                // 轮询获取结果
                return await PollForChatResultAsync(chatId, actualConversationId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"非流式AI请求失败: {e.Message}");
                return $"AI服务发生错误: {e.Message}";
            }
        }

        /// <summary>
        /// 发送聊天请求，返回完整响应信息
        /// </summary>
        private async Task<ChatResponse> SendChatRequestWithFullResponseAsync(string userMessage, string userId, string conversationId)
        {
            var userMessageObject = new
            {
                role = "user",
                content = userMessage,
                content_type = "text"
            };

            var requestBody = new Dictionary<string, object>
            {
                ["conversation_id"] = conversationId,
                ["bot_id"] = _cozeBotId,
                ["user_id"] = userId,
                ["stream"] = false,
                ["auto_save_history"] = true,
                ["additional_messages"] = new[] { userMessageObject }
            };

            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            var jsonBody = JsonSerializer.Serialize(requestBody, jsonOptions);
            var content = new StringContent(jsonBody, System.Text.Encoding.UTF8, "application/json");

            _logger.LogInformation($"发送非流式请求: {jsonBody}");

            var response = await _httpClient.PostAsync(_cozeApiEndpoint, content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            _logger.LogInformation($"收到响应: {responseBody}");

            using (JsonDocument doc = JsonDocument.Parse(responseBody))
            {
                if (doc.RootElement.TryGetProperty("code", out JsonElement codeElement) && codeElement.GetInt32() == 0)
                {
                    if (doc.RootElement.TryGetProperty("data", out JsonElement dataElement))
                    {
                        var chatId = dataElement.TryGetProperty("id", out JsonElement idElement) ? idElement.GetString() : null;
                        var returnedConversationId = dataElement.TryGetProperty("conversation_id", out JsonElement convIdElement) ? convIdElement.GetString() : null;

                        return new ChatResponse
                        {
                            ChatId = chatId,
                            ConversationId = returnedConversationId
                        };
                    }
                }
            }
            return null;
        }

        // 添加响应类
        private class ChatResponse
        {
            public string ChatId { get; set; }
            public string ConversationId { get; set; }
        }

        /// <summary>
        /// 发送聊天请求，返回chat_id
        /// </summary>
        private async Task<string> SendChatRequestAsync(string userMessage, string userId, string conversationId)
        {
            var userMessageObject = new
            {
                role = "user",
                content = userMessage,
                content_type = "text"
            };

            var requestBody = new Dictionary<string, object>
            {
                ["conversation_id"] = conversationId,
                ["bot_id"] = _cozeBotId,
                ["user_id"] = userId,
                ["stream"] = false,
                ["auto_save_history"] = true,
                ["additional_messages"] = new[] { userMessageObject }
            };

            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            var jsonBody = JsonSerializer.Serialize(requestBody, jsonOptions);
            var content = new StringContent(jsonBody, System.Text.Encoding.UTF8, "application/json");

            _logger.LogInformation($"发送非流式请求: {jsonBody}");

            var response = await _httpClient.PostAsync(_cozeApiEndpoint, content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            _logger.LogInformation($"收到响应: {responseBody}");

            using (JsonDocument doc = JsonDocument.Parse(responseBody))
            {
                if (doc.RootElement.TryGetProperty("code", out JsonElement codeElement) && codeElement.GetInt32() == 0)
                {
                    if (doc.RootElement.TryGetProperty("data", out JsonElement dataElement))
                    {
                        // 记录完整的data内容，看看是否包含conversation_id
                        _logger.LogInformation($"API返回的data: {dataElement.GetRawText()}");

                        if (dataElement.TryGetProperty("id", out JsonElement idElement))
                        {
                            return idElement.GetString();
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 轮询获取聊天结果
        /// </summary>
        private async Task<string> PollForChatResultAsync(string chatId, string conversationId)
        {
            var maxAttempts = 30;
            var delayMs = 2000;

            _logger.LogInformation($"开始轮询 - ChatId: {chatId}, ConversationId: {conversationId}");

            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                try
                {
                    // 重新加上 conversation_id 参数
                    var statusUrl = $"https://api.coze.cn/v3/chat/retrieve?chat_id={chatId}&conversation_id={conversationId}";
                    _logger.LogInformation($"轮询第{attempt + 1}次，URL: {statusUrl}");

                    // 创建请求消息并手动设置头信息
                    using var request = new HttpRequestMessage(HttpMethod.Get, statusUrl);
                    request.Headers.Add("Authorization", $"Bearer {_cozeApiKey}");
                    request.Headers.Add("Accept", "application/json");

                    var response = await _httpClient.SendAsync(request);
                    var responseBody = await response.Content.ReadAsStringAsync();

                    _logger.LogInformation($"轮询第{attempt + 1}次响应状态: {response.StatusCode}");
                    _logger.LogInformation($"轮询第{attempt + 1}次响应内容: {responseBody}");

                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.LogWarning($"轮询请求失败，状态码: {response.StatusCode}");
                        continue;
                    }

                    using (JsonDocument doc = JsonDocument.Parse(responseBody))
                    {
                        if (doc.RootElement.TryGetProperty("code", out JsonElement codeElement))
                        {
                            var code = codeElement.GetInt32();
                            _logger.LogInformation($"API返回码: {code}");

                            if (code != 0)
                            {
                                if (doc.RootElement.TryGetProperty("msg", out JsonElement msgElement))
                                {
                                    _logger.LogWarning($"API返回错误: {msgElement.GetString()}");
                                }
                                continue;
                            }
                        }

                        if (doc.RootElement.TryGetProperty("data", out JsonElement dataElement) &&
                            dataElement.TryGetProperty("status", out JsonElement statusElement))
                        {
                            var status = statusElement.GetString();
                            _logger.LogInformation($"对话状态: {status}");

                            if (status == "completed")
                            {
                                return await GetChatMessagesAsync(chatId, conversationId);
                            }
                            else if (status == "failed")
                            {
                                return "AI处理失败";
                            }
                        }
                        else
                        {
                            _logger.LogWarning("响应中未找到status字段");
                        }
                    }

                    await Task.Delay(delayMs);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"轮询第{attempt + 1}次异常: {e.Message}");
                }
            }

            return "AI响应超时，请稍后重试";
        }

        /// <summary>
        /// 获取对话消息内容
        /// </summary>
        private async Task<string> GetChatMessagesAsync(string chatId, string conversationId)
        {
            try
            {
                var messagesUrl = $"https://api.coze.cn/v3/chat/message/list?chat_id={chatId}&conversation_id={conversationId}";

                // 创建请求消息并手动设置头信息
                using var request = new HttpRequestMessage(HttpMethod.Get, messagesUrl);
                request.Headers.Add("Authorization", $"Bearer {_cozeApiKey}");
                request.Headers.Add("Accept", "application/json");

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"获取消息列表: {responseBody}");

                using (JsonDocument doc = JsonDocument.Parse(responseBody))
                {
                    if (doc.RootElement.TryGetProperty("data", out JsonElement dataElement))
                    {
                        // data 直接是消息数组，遍历查找 type 为 "answer" 的消息
                        foreach (JsonElement message in dataElement.EnumerateArray())
                        {
                            if (message.TryGetProperty("type", out JsonElement typeElement) &&
                                typeElement.GetString() == "answer" &&
                                message.TryGetProperty("content", out JsonElement contentElement))
                            {
                                return contentElement.GetString();
                            }
                        }
                    }
                }

                return "未找到AI回复内容";
            }
            catch (Exception e)
            {
                _logger.LogError(e, "获取消息内容失败");
                return "获取AI回复失败";
            }
        }
    }
}