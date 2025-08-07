using MicroServices.Domain.Reportworks;
using MicroServices.Models.Dtos.Reportworks;
using MricoServices.Shared.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Application.IService.Reportworks
{
    public interface IReportworkQualityInspectionService
    {
        /// <summary>
        /// 报工质检列表 查询分页
        /// </summary>
        /// <param name="searchReportworkQualityInspectionDto">查询条件</param>
        /// <returns>返回报工质检列表 查询分页</returns>
        Task<ApiResult<ApiPaging<List<ReportworkQualityInspectionDto>>>> GetAllReportworkRecordAsync(SearchReportworkQualityInspectionDto searchReportworkQualityInspectionDto);
        
        /// <summary>
        /// 报工质检的软删除
        /// </summary>
        /// <param name="reportworkQualityInspectionid">报工质检id</param>
        /// <returns>返回受影响行数</returns>
        Task<ApiResult> DeleteReportworkQualityInspectionAsync(int reportworkQualityInspectionid);
        /// <summary>
        /// 报工质检的反填
        /// </summary>
        /// <param name="ReportworkQualityInspectionId"></param>
        /// <returns></returns>
        Task<ApiResult<ReportworkQualityInspectionDto>> GetFTReportworkQualityInspection(int ReportworkQualityInspectionId);
        /// <summary>
        /// 质检
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult<ReportworkQualityInspectionDto>> Quality(int ReportworkQualityInspectionId,CreateReportworkRecordAndReportworkQualityInspection dto);
        
        /// <summary>
        /// 根据报工质检ID获取检测项目信息
        /// </summary>
        /// <param name="reportworkQualityInspectionId">报工质检ID</param>
        /// <returns>返回检测项目详细信息</returns>
        Task<ApiResult<InspectionItemDisplayDto>> GetInspectionItemByQualityInspectionIdAsync(int reportworkQualityInspectionId);
    }
}
