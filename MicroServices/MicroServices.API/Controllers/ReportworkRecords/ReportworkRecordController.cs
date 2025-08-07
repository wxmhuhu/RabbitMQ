using MicroServices.Application.IService.Reportworks;
using MicroServices.Domain.Reportworks;
using MicroServices.Models.Dtos.RBACDtos;
using MicroServices.Models.Dtos.Reportworks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MricoServices.Application.IService.RBAC;
using MricoServices.Application.Services.RBAC;
using MricoServices.Domain.RBAC;
using MricoServices.Shared.ApiResult;

namespace MicroServices.API.Controllers.ReportworkRecords
{
    /// <summary>
    /// 报工记录
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportworkRecordController : ControllerBase
    {
        private readonly IReportworkRecordService reportworkRecordService;

        public ReportworkRecordController(IReportworkRecordService reportworkRecordService)
        {
            this.reportworkRecordService = reportworkRecordService;
        }
        /// <summary>
        /// 报工记录列表分页查询
        /// </summary>
        /// <param name="searchReportworkRecordDto">报工记录查询dto</param>
        /// <returns>返回报工记录列表分页查询</returns>
        [HttpGet]
        public async Task<ApiResult<ApiPaging<List<ReportworkRecordDto>>>> GetAllReportworkRecordAsync([FromQuery]SearchReportworkRecordDto searchReportworkRecordDto)
        {
            return await reportworkRecordService.GetAllReportworkRecordAsync(searchReportworkRecordDto);
        }
        /// <summary>
        /// 报工记录的软删除
        /// </summary>
        /// <param name="reportworkRecordid">报工记录id</param>
        /// <returns>返回受影响行数</returns>
        [HttpDelete]
        public async Task<ApiResult> DeleteReportworkRecordAsync(int reportworkRecordid)
        {
            return await reportworkRecordService.DeleteReportworkRecordAsync(reportworkRecordid);
        }
        /// <summary>
        /// 根据工单任务同时生成报工记录和质检记录
        /// </summary>
        /// <param name="dto">包含工单任务id及前端传参字段的dto</param>
        /// <returns>返回操作结果</returns>
        [HttpPost]
        public async Task<ApiResult> GenerateReportworkRecordByWorkOrderTaskAsync(int workerorderId, ReportworkRecordAndReportworkQualityInspectionDto dto)
        {
            return await reportworkRecordService.GenerateReportworkRecordByWorkOrderTaskAsync(workerorderId,dto);
        }
        /// <summary>
        /// 根据工单任务ID反填报工记录信息
        /// </summary>
        /// <param name="reportworkRecordId">工单任务ID</param>
        /// <returns>返回反填的报工记录信息</returns>
        [HttpGet]
        public async Task<ApiResult<ReportworkRecordDto>> GetReportworkRecordInfoByWorkOrderTaskAsync(int reportworkRecordId)
        {
            return await reportworkRecordService.GetReportworkRecordInfoByWorkOrderTaskAsync(reportworkRecordId);
        }
    }
}
