using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Models.Dtos.Reportworks
{
    /// <summary>
    /// 报工记录报工质检同时添加
    /// </summary>
    public class ReportworkRecordAndReportworkQualityInspectionDto
    {
        public CreateReportworkQualityInspectionDto reportworkqualityinspectionDto { get; set; }
        public CreateReportworkRecordDto reportworkRecorddto { get; set; }
    }
    /// <summary>
    /// 质检
    /// </summary>
    public class CreateReportworkRecordAndReportworkQualityInspection
    {
        /// <summary>
        /// 检测项目表
        /// </summary>
        public CreateInspectionItem inspectionItem { get; set; }
        /// <summary>
        /// 检测结果表
        /// </summary>
        public CreateInspectionResult inspectionResult { get; set; }
      
    }
}
