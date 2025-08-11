using MicroServices.Application.IService.ProductPlan;
using MicroServices.Models.Dtos.Product_PlanDtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace MicroServices.Application.Services.Product_Plan
{
    /// <summary>
    /// RabbitMQ消息服务实现
    /// </summary>
    public class RabbitMQService : IRabbitMQService, IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<RabbitMQService> _logger;
        private IConnection _connection;
        private IModel _channel;
        private readonly string _hostName;
        private readonly int _port;
        private readonly string _userName;
        private readonly string _password;
        private readonly string _virtualHost;

        public RabbitMQService(IConfiguration configuration, ILogger<RabbitMQService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            
            // 从配置文件读取RabbitMQ配置
            _hostName = _configuration["RabbitMQ:HostName"] ?? "localhost";
            _port = int.Parse(_configuration["RabbitMQ:Port"] ?? "5672");
            _userName = _configuration["RabbitMQ:UserName"] ?? "guest";
            _password = _configuration["RabbitMQ:Password"] ?? "guest";
            _virtualHost = _configuration["RabbitMQ:VirtualHost"] ?? "/";
        }

        /// <summary>
        /// 初始化RabbitMQ连接和交换机
        /// </summary>
        /// <returns></returns>
        public async Task<bool> InitializeAsync()
        {
            try
            {
                // 创建连接工厂
                var factory = new ConnectionFactory
                {
                    HostName = _hostName,
                    Port = _port,
                    UserName = _userName,
                    Password = _password,
                    VirtualHost = _virtualHost
                };

                // 建立连接
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                // 声明交换机
                _channel.ExchangeDeclare(
                    exchange: "product_plan_exchange",
                    type: ExchangeType.Topic,
                    durable: true,
                    autoDelete: false
                );

                // 声明队列
                _channel.QueueDeclare(
                    queue: "product_plan_queue",
                    durable: true,
                    exclusive: false,
                    autoDelete: false
                );

                // 绑定队列到交换机
                _channel.QueueBind(
                    queue: "product_plan_queue",
                    exchange: "product_plan_exchange",
                    routingKey: "product.plan.created"
                );

                _logger.LogInformation("RabbitMQ连接和交换机初始化成功");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RabbitMQ初始化失败");
                return false;
            }
        }

        /// <summary>
        /// 发布生产计划创建完成消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <returns></returns>
        public async Task<bool> PublishProductPlanCreatedMessageAsync(ProductPlanCreatedMessage message)
        {
            try
            {
                if (_channel == null || _channel.IsClosed)
                {
                    await InitializeAsync();
                }

                var jsonMessage = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(jsonMessage);

                var properties = _channel.CreateBasicProperties();
                properties.Persistent = true; // 消息持久化
                properties.MessageId = message.MessageId;
                properties.Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                properties.Type = message.MessageType;

                _channel.BasicPublish(
                    exchange: "product_plan_exchange",
                    routingKey: "product.plan.created",
                    basicProperties: properties,
                    body: body
                );

                _logger.LogInformation($"生产计划创建消息发布成功: {message.PlanId}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"发布生产计划创建消息失败: {message.PlanId}");
                return false;
            }
        }

        /// <summary>
        /// 发布通用消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="routingKey">路由键</param>
        /// <param name="exchangeName">交换机名称</param>
        /// <returns></returns>
        public async Task<bool> PublishMessageAsync(string message, string routingKey, string exchangeName = "product_plan_exchange")
        {
            try
            {
                if (_channel == null || _channel.IsClosed)
                {
                    await InitializeAsync();
                }

                var body = Encoding.UTF8.GetBytes(message);

                var properties = _channel.CreateBasicProperties();
                properties.Persistent = true;

                _channel.BasicPublish(
                    exchange: exchangeName,
                    routingKey: routingKey,
                    basicProperties: properties,
                    body: body
                );

                _logger.LogInformation($"消息发布成功: {routingKey}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"发布消息失败: {routingKey}");
                return false;
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
