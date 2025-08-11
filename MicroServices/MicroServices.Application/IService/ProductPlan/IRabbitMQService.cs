using MicroServices.Models.Dtos.Product_PlanDtos;

namespace MicroServices.Application.IService.ProductPlan
{
    /// <summary>
    /// RabbitMQ消息服务接口
    /// </summary>
    public interface IRabbitMQService
    {
        /// <summary>
        /// 发布生产计划创建完成消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <returns></returns>
        Task<bool> PublishProductPlanCreatedMessageAsync(ProductPlanCreatedMessage message);

        /// <summary>
        /// 发布通用消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="routingKey">路由键</param>
        /// <param name="exchangeName">交换机名称</param>
        /// <returns></returns>
        Task<bool> PublishMessageAsync(string message, string routingKey, string exchangeName = "product_plan_exchange");

        /// <summary>
        /// 初始化RabbitMQ连接和交换机
        /// </summary>
        /// <returns></returns>
        Task<bool> InitializeAsync();
    }
}
