using MicroServices.Domain.Reportworks;
using MicroServices.Models.Dtos.RBACDtos;
using MicroServices.Models.Dtos.Reportworks;
using MricoServices.Shared.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Application.IService.Reportworks
{
    public interface  IReportworkRecordService
    {
      /// <summary>
      /// 报工记录列表
      /// </summary>
      /// <param name="searchReportworkRecordDto"></param>
      /// <returns></returns>
        Task<ApiResult<ApiPaging<List<ReportworkRecordDto>>>> GetAllReportworkRecordAsync(SearchReportworkRecordDto searchReportworkRecordDto);
        /// <summary>
        /// 报工记录的软删除
        /// </summary>
        /// <param name="reportworkRecordid">报工记录id</param>
        /// <returns>返回受影响行数</returns>
        Task<ApiResult> DeleteReportworkRecordAsync(int reportworkRecordid);
        /// <summary>
        /// 根据工单任务生成报工记录和报工质检
        /// </summary>
        /// <param name="dto">包含工单任务id及前端传参字段的dto</param>
        /// <returns>返回操作结果</returns>
        Task<ApiResult> GenerateReportworkRecordByWorkOrderTaskAsync(int workerorderId, ReportworkRecordAndReportworkQualityInspectionDto dto);
        /// <summary>
        /// 根据工单任务ID反填报工记录信息
        /// </summary>
        /// <param name="reportworkRecordId">工单任务ID</param>
        /// <returns>返回反填的报工记录信息</returns>
        Task<ApiResult<ReportworkRecordDto>> GetReportworkRecordInfoByWorkOrderTaskAsync(int reportworkRecordId);
    }
}
