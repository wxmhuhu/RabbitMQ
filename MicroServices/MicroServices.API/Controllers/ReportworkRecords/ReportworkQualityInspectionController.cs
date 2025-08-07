using MicroServices.Application.IService.Reportworks;
using MicroServices.Models.Dtos.Reportworks;
using Microsoft.AspNetCore.Mvc;
using MricoServices.Shared.ApiResult;

namespace MicroServices.API.Controllers.ReportworkRecords
{
    /// <summary>
    /// 报工质检
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportworkQualityInspectionController : ControllerBase
    {
        private readonly IReportworkQualityInspectionService reportworkQualityInspectionService;

        public ReportworkQualityInspectionController(IReportworkQualityInspectionService reportworkQualityInspectionService)
        {
            this.reportworkQualityInspectionService = reportworkQualityInspectionService;
        }
        /// <summary>
        /// 报工质检列表 查询分页
        /// </summary>
        /// <param name="searchReportworkQualityInspectionDto">查询条件</param>
        /// <returns>返回报工质检列表 查询分页</returns>
        [HttpGet]
        public async Task<ApiResult<ApiPaging<List<ReportworkQualityInspectionDto>>>> GetAllReportworkRecordAsync([FromQuery]SearchReportworkQualityInspectionDto searchReportworkQualityInspectionDto)
        {
            return await reportworkQualityInspectionService.GetAllReportworkRecordAsync(searchReportworkQualityInspectionDto);
        }
        /// <summary>
        /// 报工质检的软删除
        /// </summary>
        /// <param name="reportworkQualityInspectionid">报工质检id</param>
        /// <returns>返回受影响行数</returns>
        [HttpDelete]
        public async Task<ApiResult> DeleteReportworkQualityInspectionAsync(int reportworkQualityInspectionid)
        {
            return await reportworkQualityInspectionService.DeleteReportworkQualityInspectionAsync(reportworkQualityInspectionid);
        }
        /// <summary>
        /// 报工质检的反填
        /// </summary>
        /// <param name="ReportworkQualityInspectionId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ReportworkQualityInspectionDto>> GetFTReportworkQualityInspection(int ReportworkQualityInspectionId)
        {
            return await reportworkQualityInspectionService.GetFTReportworkQualityInspection(ReportworkQualityInspectionId);
        }
        /// <summary>
        /// 质检
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<ReportworkQualityInspectionDto>> Quality(int ReportworkQualityInspectionId,CreateReportworkRecordAndReportworkQualityInspection dto)
        {
            return await reportworkQualityInspectionService.Quality(ReportworkQualityInspectionId,dto);
        }
        
        /// <summary>
        /// 根据报工质检ID获取检测项目信息
        /// </summary>
        /// <param name="reportworkQualityInspectionId">报工质检ID</param>
        /// <returns>返回检测项目详细信息</returns>
        [HttpGet]
        public async Task<ApiResult<InspectionItemDisplayDto>> GetInspectionItemByQualityInspectionIdAsync(int reportworkQualityInspectionId)
        {
            return await reportworkQualityInspectionService.GetInspectionItemByQualityInspectionIdAsync(reportworkQualityInspectionId);
        }
    }
}
