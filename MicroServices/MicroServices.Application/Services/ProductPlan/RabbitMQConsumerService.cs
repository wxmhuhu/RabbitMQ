using MicroServices.Application.IService.ProductPlan;
using MicroServices.Models.Dtos.Product_PlanDtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace MicroServices.Application.Services.Product_Plan
{
    /// <summary>
    /// RabbitMQ消息消费者服务
    /// </summary>
    public class RabbitMQConsumerService : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<RabbitMQConsumerService> _logger;
        private IConnection _connection;
        private IModel _channel;
        private readonly string _hostName;
        private readonly int _port;
        private readonly string _userName;
        private readonly string _password;
        private readonly string _virtualHost;

        public RabbitMQConsumerService(IConfiguration configuration, ILogger<RabbitMQConsumerService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            
            _hostName = _configuration["RabbitMQ:HostName"] ?? "localhost";
            _port = int.Parse(_configuration["RabbitMQ:Port"] ?? "5672");
            _userName = _configuration["RabbitMQ:UserName"] ?? "guest";
            _password = _configuration["RabbitMQ:Password"] ?? "guest";
            _virtualHost = _configuration["RabbitMQ:VirtualHost"] ?? "/";
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await InitializeConsumerAsync();
                
                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(1000, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RabbitMQ消费者服务执行异常");
            }
        }

        private async Task InitializeConsumerAsync()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostName,
                    Port = _port,
                    UserName = _userName,
                    Password = _password,
                    VirtualHost = _virtualHost
                };

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

                // 设置消费者
                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += OnMessageReceived;

                _channel.BasicConsume(
                    queue: "product_plan_queue",
                    autoAck: true,
                    consumer: consumer
                );

                _logger.LogInformation("RabbitMQ消费者服务初始化成功，开始监听消息...");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RabbitMQ消费者服务初始化失败");
            }
        }

        private void OnMessageReceived(object sender, BasicDeliverEventArgs e)
        {
            try
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                
                _logger.LogInformation($"收到消息: {message}");

                // 尝试反序列化消息
                if (e.BasicProperties.Type == "ProductPlanCreated")
                {
                    var productPlanMessage = JsonSerializer.Deserialize<ProductPlanCreatedMessage>(message);
                    if (productPlanMessage != null)
                    {
                        _logger.LogInformation($"生产计划创建消息: 计划编号={productPlanMessage.PlanId}, 计划名称={productPlanMessage.PlanName}");
                        
                        // 这里可以添加具体的业务逻辑处理
                        // 例如：发送通知、更新缓存、触发其他业务流程等
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理RabbitMQ消息时发生异常");
            }
        }

        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            _channel?.Dispose();
            _connection?.Dispose();
            base.Dispose();
        }
    }
}
