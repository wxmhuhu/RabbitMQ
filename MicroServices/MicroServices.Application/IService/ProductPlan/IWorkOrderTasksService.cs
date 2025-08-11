using MicroServices.Domain.Product_Plan;
using MicroServices.Models.Dtos.Product_PlanDtos;
using MricoServices.Shared.ApiResult;

namespace MicroServices.Application.IService.ProductPlan
{
    public interface IWorkOrderTasksService
    {
        /// <summary>
        /// 反填
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResult<WorkOrderTasksDto>> GetWorkOrderTasksServiceByIdAsync(int id);
        /// <summary>
        /// 根据id获取工单任务
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<WorkOrderTasksDto>>> GetAllWorkOrderTasksServiceAsync();
        /// <summary>
        /// 工单任务 带分页的显示
        /// </summary>
        /// <param name="searchWorkOrderTasksDto"></param>
        /// <returns></returns>
        Task<ApiResult<ApiPaging<List<WorkOrderTasksDto>>>> PageWorkOrderTasksAsync(SearchWorkOrderTasksDto searchWorkOrderTasksDto);
        /// <summary>
        /// 派工之后修改状态已下达
        /// </summary>
        /// <param name="workerTasksId"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateWorkOrderTasksServiceAsync(int workTasksId, CreateUpdateWorkOrderTasksDtos dto);
        Task<ApiResult<int>> DeleteWorkOrderTasksServiceAsync(int id);
        /// <summary>
        /// 开工功能 - 根据班组Id是否有值来修改状态
        /// </summary>
        /// <param name="workerTaskId">工单任务Id</param>
        /// <returns></returns>
        Task<ApiResult> Meugah(int workerTaskId);
    }
}
