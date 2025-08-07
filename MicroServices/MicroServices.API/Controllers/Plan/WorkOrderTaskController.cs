using MicroServices.Application.IService.Product_Plan;
using MicroServices.Models.Dtos.Product_PlanDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MricoServices.Shared.ApiResult;

namespace MicroServices.API.Controllers.Plan
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WorkOrderTaskController : ControllerBase
    {
        private readonly IWorkOrderTasksService workOrderTasksService;

        public WorkOrderTaskController(IWorkOrderTasksService workOrderTasksService)
        {
            this.workOrderTasksService = workOrderTasksService;
        }
        /// <summary>
        /// 无分页工单任务显示
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<WorkOrderTasksDto>>> GetAllWorkOrderTasksServiceAsync()
        {
            return await workOrderTasksService.GetAllWorkOrderTasksServiceAsync();
        }
        /// <summary>
        /// 根据id获取工单任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<WorkOrderTasksDto>> GetWorkOrderTasksServiceByIdAsync(int id)
        {
            return await workOrderTasksService.GetWorkOrderTasksServiceByIdAsync(id);
        }
        /// <summary>
        /// 有分页工单任务
        /// </summary>
        /// <param name="searchWorkOrderTasksDto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ApiPaging<List<WorkOrderTasksDto>>>> PageWorkOrderTasksAsync([FromQuery]SearchWorkOrderTasksDto searchWorkOrderTasksDto)
        {
            return await workOrderTasksService.PageWorkOrderTasksAsync(searchWorkOrderTasksDto);
        }
        /// <summary>
        /// 派工之后修改状态已下达
        /// </summary>
        [HttpPut]
        public async Task<ApiResult> UpdateWorkOrderTasksServiceAsync(int workTasksId, CreateUpdateWorkOrderTasksDtos dto)
        {
            return await workOrderTasksService.UpdateWorkOrderTasksServiceAsync(workTasksId,dto);
        }
        /// <summary>
        /// 开工功能 
        /// </summary>
        /// <param name="workerTaskId">工单任务Id</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ApiResult> Meugah(int workerTaskId)
        {
            return await workOrderTasksService.Meugah(workerTaskId);
        }
    }
}
