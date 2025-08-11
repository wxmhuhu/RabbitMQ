using MicroServices.Application.IService.ProductPlan;
using MicroServices.Models.Dtos.Product_PlanDtos;
using Microsoft.AspNetCore.Mvc;
using MricoServices.Shared.ApiResult;

namespace MicroServices.API.Controllers.Plan
{
    /// <summary>
    /// RabbitMQ测试控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RabbitMQTestController : ControllerBase
    {
        private readonly IRabbitMQService _rabbitMQService;

        public RabbitMQTestController(IRabbitMQService rabbitMQService)
        {
            _rabbitMQService = rabbitMQService;
        }

        /// <summary>
        /// 测试发布生产计划创建消息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> TestPublishProductPlanMessage()
        {
            try
            {
                var message = new ProductPlanCreatedMessage
                {
                    Id = 999,
                    PlanId = "TEST001",
                    PlanName = "测试生产计划",
                    ProductName = "测试产品",
                    PlanNums = 100,
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddDays(30),
                    Status = 1,
                    Remark = "这是一个测试消息"
                };

                var result = await _rabbitMQService.PublishProductPlanCreatedMessageAsync(message);
                
                if (result)
                {
                    return ApiResult.Success(ResultCode.Ok);
                }
                else
                {
                    return ApiResult.Fail(ResultCode.Fail, "测试消息发布失败");
                }
            }
            catch (Exception ex)
            {
                return ApiResult.Fail(ResultCode.Fail, $"测试消息发布异常: {ex.Message}");
            }
        }

        /// <summary>
        /// 测试发布通用消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="routingKey">路由键</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> TestPublishMessage([FromBody] string message, [FromQuery] string routingKey = "test.message")
        {
            try
            {
                var result = await _rabbitMQService.PublishMessageAsync(message, routingKey);
                
                if (result)
                {
                    return ApiResult.Success(ResultCode.Ok);
                }
                else
                {
                    return ApiResult.Fail(ResultCode.Fail, "消息发布失败");
                }
            }
            catch (Exception ex)
            {
                return ApiResult.Fail(ResultCode.Fail, $"消息发布异常: {ex.Message}");
            }
        }
    }
}
