# RabbitMQ 集成说明

## 概述

本项目已成功集成 RabbitMQ 消息队列，用于在生产计划创建完成后发布消息通知。

## 功能特性

- ✅ 生产计划创建完成后自动发布消息到 RabbitMQ
- ✅ 支持消息持久化
- ✅ 使用 Topic 交换机模式
- ✅ 自动创建交换机和队列
- ✅ 提供消息消费者服务
- ✅ 支持测试接口

## 配置说明

### 1. RabbitMQ 配置

在 `appsettings.json` 中添加以下配置：

```json
{
  "RabbitMQ": {
    "HostName": "localhost",
    "Port": 5672,
    "UserName": "guest",
    "Password": "guest",
    "VirtualHost": "/"
  }
}
```

### 2. 消息结构

生产计划创建消息包含以下字段：

```csharp
public class ProductPlanCreatedMessage
{
    public string MessageId { get; set; }        // 消息ID
    public string MessageType { get; set; }     // 消息类型
    public DateTime Timestamp { get; set; }     // 时间戳
    public int Id { get; set; }                 // 生产计划ID
    public string PlanId { get; set; }          // 计划编号
    public string PlanName { get; set; }        // 计划名称
    public string ProductName { get; set; }     // 产品名称
    public int? PlanNums { get; set; }         // 计划数量
    public DateTime? StartTime { get; set; }    // 开工日期
    public DateTime? EndTime { get; set; }      // 完工日期
    public int? Status { get; set; }           // 状态
    public string Remark { get; set; }         // 备注
}
```

## 使用方法

### 1. 自动发布消息

当调用生产计划创建接口时，系统会自动发布消息到 RabbitMQ：

```csharp
// 接口地址
POST /api/Plan/AddIProductionPlanServiceAsync

// 消息会自动发布到以下路由
Exchange: product_plan_exchange
Queue: product_plan_queue
Routing Key: product.plan.created
```

### 2. 手动测试消息发布

使用测试接口手动发布消息：

```csharp
// 测试生产计划消息发布
POST /api/RabbitMQTest/TestPublishProductPlanMessage

// 测试通用消息发布
POST /api/RabbitMQTest/TestPublishMessage
Body: "测试消息内容"
Query: routingKey=test.message
```

## 架构说明

### 1. 交换机类型

- **类型**: Topic Exchange
- **名称**: `product_plan_exchange`
- **持久化**: 是
- **自动删除**: 否

### 2. 队列配置

- **队列名**: `product_plan_queue`
- **持久化**: 是
- **排他性**: 否
- **自动删除**: 否

### 3. 路由绑定

- **队列**: `product_plan_queue`
- **交换机**: `product_plan_exchange`
- **路由键**: `product.plan.created`

## 服务注册

### 1. 依赖注入

以下服务已自动注册：

```csharp
// RabbitMQ服务
services.AddScoped<IRabbitMQService, RabbitMQService>();

// 消费者服务（后台服务）
services.AddHostedService<RabbitMQConsumerService>();
```

### 2. 服务接口

- `IRabbitMQService`: RabbitMQ 消息发布服务
- `RabbitMQConsumerService`: 消息消费者服务

## 监控和日志

### 1. 日志记录

系统会记录以下日志：

- RabbitMQ 连接状态
- 消息发布成功/失败
- 消息接收和处理
- 异常信息

### 2. RabbitMQ 管理界面

访问 `http://localhost:15672` 查看：

- 交换机状态
- 队列状态
- 消息流量
- 连接状态

## 故障排除

### 1. 连接问题

- 检查 RabbitMQ 服务是否启动
- 验证连接配置是否正确
- 确认防火墙设置

### 2. 消息发布失败

- 检查 RabbitMQ 服务状态
- 查看应用程序日志
- 验证交换机和队列是否正确创建

### 3. 消息接收问题

- 检查消费者服务是否正常运行
- 验证队列绑定是否正确
- 查看消费者日志

## 扩展功能

### 1. 添加新的消息类型

1. 在 `ProductPlanDto.cs` 中定义新的消息模型
2. 在 `IRabbitMQService` 中添加新的发布方法
3. 在 `RabbitMQService` 中实现新方法
4. 在业务逻辑中调用新方法

### 2. 添加新的消费者

1. 创建新的消费者服务类
2. 继承 `BackgroundService`
3. 实现消息处理逻辑
4. 在 `Program.cs` 中注册服务

### 3. 消息过滤

可以通过修改路由键来实现消息过滤：

```csharp
// 例如：按产品类型过滤
routingKey: "product.plan.created.electronics"
routingKey: "product.plan.created.mechanical"
```

## 性能优化建议

1. **连接池**: 考虑使用连接池管理 RabbitMQ 连接
2. **批量发布**: 对于大量消息，考虑批量发布
3. **消息确认**: 在生产环境中启用消息确认机制
4. **监控告警**: 添加 RabbitMQ 性能监控和告警

## 注意事项

1. **消息持久化**: 消息已设置为持久化，确保在 RabbitMQ 重启后不丢失
2. **异常处理**: 消息发布失败不会影响生产计划创建流程
3. **资源管理**: 服务实现了 `IDisposable` 接口，确保资源正确释放
4. **配置验证**: 启动时会自动验证 RabbitMQ 配置并尝试连接
