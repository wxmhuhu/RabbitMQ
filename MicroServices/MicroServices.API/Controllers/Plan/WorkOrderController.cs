using MicroServices.Application.IService.ProductPlan;
using MicroServices.Domain.Product_Plan;
using MicroServices.Models.Dtos.Product_PlanDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MricoServices.Shared.ApiResult;

namespace MicroServices.API.Controllers.Plan
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WorkOrderController : ControllerBase
    {
        private readonly IWorkOrderService workOrderService;

        public WorkOrderController(IWorkOrderService workOrderService)
        {
            this.workOrderService = workOrderService;
        }

        /// <summary>
        /// 获取所有生产计划（不带分页，请谨慎使用在大数据量场景）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<WorkOrderDtos>>> GetAllWorkOrderServiceAsync()
        {
            return await workOrderService.GetAllWorkOrderServiceAsync();
        }
        /// <summary>
        /// 根据ID获取单个生产计划
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<WorkOrderDtos>> GetWorkOrderServiceByIdAsync(int id)
        {
            try
            {
                return await workOrderService.GetWorkOrderServiceByIdAsync(id);
            }
            catch (Exception ex)
            {
                // 可以在这里记录异常日志
                Console.WriteLine($"根据ID获取生产计划时发生异常: {ex.Message}");
                return ApiResult<WorkOrderDtos>.Fail(ResultCode.Fail, "获取生产计划时发生内部错误。");
            }
        }
        /// <summary>
        /// 分页获取生产计划列表
        /// </summary>
        /// <param name="searchWorkOrderDtos"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ApiPaging<List<WorkOrderDtos>>>> PagingWorkOrderServiceAsync([FromQuery]SearchWorkOrderDtos searchWorkOrderDtos)
        {
            try
            {
                return await workOrderService.PagingWorkOrderServiceAsync(searchWorkOrderDtos);
            }
            catch (Exception ex)
            {
                // 可以在这里记录异常日志
                Console.WriteLine($"分页获取生产计划列表时发生异常: {ex.Message}");
                return ApiResult<ApiPaging<List<WorkOrderDtos>>>.Fail(ResultCode.Fail, "分页获取生产计划列表时发生内部错误。");
            }
        }

        /// <summary>
        /// 软删除生产计划
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ApiResult<int>> DeleteWorkOrderServiceAsync(int id)
        {
            try
            {
                return await workOrderService.DeleteWorkOrderServiceAsync(id);
            }
            catch (Exception ex)
            {
                // 可以在这里记录异常日志
                Console.WriteLine($"软删除生产计划时发生异常: {ex.Message}");
                return ApiResult<int>.Fail(ResultCode.Fail, "软删除生产计划时发生内部错误。");
            }
        }
        /// <summary>
        /// 更新生产计划
        /// </summary>
        /// <param name="updateWorkOrderDtos"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ApiResult<WorkOrder>> UpdateWorkOrderServiceAsync(int id, CreateUpdateWorkOrderDtos updateWorkOrderDtos)
        {
            return await workOrderService.UpdateWorkOrderServiceAsync(id, updateWorkOrderDtos);
        }
        /// <summary>
        /// 更新工单状态并创建工单任务
        /// </summary>
        /// <param name="id">工单ID</param>
        /// <param name="status">要更新的状态</param>
        /// <param name="createUpdateWorkOrderTasksDtos">工单任务数据</param>
        /// <returns>更新结果</returns>
        [HttpPut]
        public async Task<ApiResult<WorkOrder>> UpdateWorkOrderStatus(int id, int status, CreateUpdateWorkOrderTasksDtos createUpdateWorkOrderTasksDtos)
        {
            try
            {
                return await workOrderService.UpdateWorkOrderStatus(id, status, createUpdateWorkOrderTasksDtos);
            }
            catch (Exception ex)
            {
                // 可以在这里记录异常日志
                Console.WriteLine($"更新工单状态时发生异常: {ex.Message}");
                return ApiResult<WorkOrder>.Fail(ResultCode.Fail, "更新工单状态时发生内部错误。");
            }
        }
    }
}
