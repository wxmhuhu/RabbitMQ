using MicroServices.Domain.Product_Plan;
using MicroServices.Models.Dtos.Product_PlanDtos;
using MicroServices.Models.Dtos.RBACDtos;
using MricoServices.Shared.ApiResult;
using static MicroServices.Models.Dtos.Product_PlanDtos.SearchWorkOrderDtos;

namespace MicroServices.Application.IService.Product_Plan
{
    public interface IWorkOrderService
    {
        Task<ApiResult<WorkOrderDtos>> GetWorkOrderServiceByIdAsync(int id);
        Task<ApiResult<List<WorkOrderDtos>>> GetAllWorkOrderServiceAsync();
        Task<ApiResult<ApiPaging<List<WorkOrderDtos>>>> PagingWorkOrderServiceAsync(SearchWorkOrderDtos searchWorkOrderDtos);
        Task<ApiResult<WorkOrder>> UpdateWorkOrderServiceAsync(int id,CreateUpdateWorkOrderDtos updateWorkOrderDtos);
        Task<ApiResult<WorkOrder>> UpdateWorkOrderStatus(int id, int status, CreateUpdateWorkOrderTasksDtos createUpdateWorkOrderTasksDtos);
        Task<ApiResult<ApiPaging<List<WorkOrderDtos>>>> GetAllWorkOrderServiceAsync(SearchWorkOrderDto search);
        Task<ApiResult<int>> DeleteWorkOrderServiceAsync(int id);
        Task<ApiResult<List<ProductionSchedulingDto>>> ProductionScheduling(int id);
    }
}
