using MicroServices.Application.IService.ProductPlan;
using MicroServices.Models.Dtos.Product_PlanDtos;
using Microsoft.AspNetCore.Mvc;
using MricoServices.Shared.ApiResult;
using static MicroServices.Models.Dtos.Product_PlanDtos.SearchWorkOrderDtos;

namespace MicroServices.API.Controllers.Plan
{
    /// <summary>
    /// 生产计划
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IProductPlanService productPlanService;
        private readonly IWorkOrderService workOrderService;

        public PlanController(IProductPlanService productPlanService,IWorkOrderService workOrderService)
        {
            this.productPlanService = productPlanService;
            this.workOrderService = workOrderService;
        }
        /// <summary>
        /// 生产计划添加
        /// </summary>
        /// <param name="createProductionPlanDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> AddIProductionPlanServiceAsync(CreateUpdateProductionPlanDto createProductionPlanDto)
        {
            return await productPlanService.AddIProductionPlanServiceAsync(createProductionPlanDto);
        }
        /// <summary>
        /// 生产计划列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ApiPaging<List<ProductPlanDto>>>> GetIProductionPlanServiceByIdAsync([FromQuery]Models.Dtos.Product_PlanDtos.Search search)
        {
            return await productPlanService.GetIProductionPlanServiceByIdAsync(search);
        }
        /// <summary>
        /// 分解生产计划
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        public async Task<ApiResult> ProductionDismantle(int id)
        {
            try
            {
                return await productPlanService.ProductionDismantle(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 生产计划更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="createUpdateProductionPlanDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ApiResult<ProductPlanDto>> UpdateIProductionPlanServiceAsync(int id, CreateUpdateProductionPlanDto UpdateProductionPlanDto)
        {
            return await productPlanService.UpdateIProductionPlanServiceAsync(id, UpdateProductionPlanDto);
        }
        /// <summary>
        /// 生产计划删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ApiResult> DeleteIProductionPlanServiceAsync(int id)
        {
            return await productPlanService.DeleteIProductionPlanServiceAsync(id);
        }

        /// <summary>
        /// 点击排产获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ProductionSchedulingDto>>> ProductionScheduling(int id)
        {
            try
            {
                return await workOrderService.ProductionScheduling(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 获取工单列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ApiPaging<List<WorkOrderDtos>>>> GetWorkOrderListAsync([FromQuery] SearchWorkOrderDto search)
        {
            try
            {
                return await workOrderService.GetAllWorkOrderServiceAsync(search);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
