using MicroServices.Models.Dtos.Product_PlanDtos;
using MicroServices.Models.Dtos.RBACDtos;
using MricoServices.Shared.ApiResult;

namespace MicroServices.Application.IService.ProductPlan
{
    public interface IProductPlanService
    {
        Task<ApiResult<ApiPaging<List<ProductPlanDto>>>> GetIProductionPlanServiceByIdAsync(Search search);
        Task<ApiResult> AddIProductionPlanServiceAsync(CreateUpdateProductionPlanDto createProductionPlanDto);
        Task<ApiResult<ProductPlanDto>> UpdateIProductionPlanServiceAsync(int id,CreateUpdateProductionPlanDto UpdateProductionPlanDto);
        Task<ApiResult> DeleteIProductionPlanServiceAsync(int id);
        Task<ApiResult> ProductionDismantle(int id);
    }
}
