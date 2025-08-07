using MricoServices.Shared;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Models.Dtos.Reportworks
{
    /// <summary>
    /// 报工质检列表dto
    /// </summary>
    public class ReportworkQualityInspectionDto : AuditableEntity
    {
        /// <summary>
        /// 检验单id
        /// </summary>
        public int InspectionItemId { get; set; }
        /// <summary>
        /// 检测项目名称
        /// </summary>
        public string ItemsName { get; set; }
        /// <summary>
        /// 检测项目编号
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// 检测类型
        /// </summary>
        public int InspectionItemTypeId { get; set; }
        /// <summary>
        /// 检测项目类型名称
        /// </summary>
        public string InspectionItemTypeName { get; set; }
        /// <summary>
        /// 检验部门（如页面中的“质检部门”）
        /// </summary>
        public string InspectionDept { get; set; }

        /// <summary>
        /// 检验人（如页面中的“李丽丽”）
        /// </summary>
        public string Inspector { get; set; }
        /// <summary>
        /// 报工记录关联ID（关联 ReportworkRecord 表主键）
        /// 用于关联对应的报工记录
        /// </summary>
        public int ReportworkRecordId { get; set; }
        /// <summary>
        /// 报工人员
        /// </summary>
        public string ReportingPerson { get; set; }
        /// <summary>
        /// 报工时间
        /// </summary>
        public DateTime ReportingDate { get; set; }
        /// <summary>
        /// 检验时间
        /// </summary>
        public DateTime? QualityDate { get; set; }
        /// <summary>
        /// 报工数量
        /// </summary>
        public int ReportingQuantity { get; set; }

        /// <summary>
        /// 生产工单id
        /// </summary>
        public int WorkOrderId { get; set; }
        /// <summary>
        /// 工单编号1
        /// </summary>
        public string OrderNumber { get; set; }
        /// <summary>
        /// 工单名称
        /// </summary>
        public string OrderName { get; set; }

        /// <summary>
        /// 工单任务id
        /// </summary>
        public int WorkOrderTasksId { get; set; }
        /// <summary>
        /// 任务编号1
        /// </summary>
        public string TaskNumber { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 站点id
        /// </summary>
        public int SiteId { get; set; }
        /// <summary>
        /// 站点名称
        /// </summary>
        public string SiteName { get; set; }
        
        /// <summary>
        /// 工序id
        /// </summary>
        public int ProcessId { get; set; }
        /// <summary>
        /// 工序编号1
        /// </summary>
        public string ProcessCode { get; set; }
        /// <summary>
        /// 工序名称
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// 班组id
        /// </summary>
        public int TeamId { get; set; }
        /// <summary>
		/// 班组名称
		/// </summary>
        public string TeamName { get; set; }
        /// <summary>
		/// 任务负责人
		/// </summary>
        public string Director { get; set; }
       
        /// <summary>
        /// 产品id
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 规格型号
        /// </summary>
        public string Specification { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 产品类型(成品/半成品等)
        /// </summary>
        public string ProductType { get; set; }
       
        /// <summary>
        /// 合格数量
        /// </summary>
        public int? Qualified { get; set; }

        /// <summary>
        /// 不合格数量
        /// </summary>
        public int? Unqualified { get; set; }

        /// <summary>
        /// 合格率（格式如页面中的“60%”）
        /// </summary>
        public string? QualifiedRate { get; set; }

        /// <summary>
        /// 检测结果（如页面中的“合格”）
        /// </summary>
        public string? InspectionResult { get; set; }

        /// <summary>
        /// 质检状态（如页面中的“未质检/已质检”）
        /// </summary>
        public int QualityStatus { get; set; }
    }
    /// <summary>
    /// 报工质检查询dto
    /// </summary>
    public class SearchReportworkQualityInspectionDto : PageModel
    {
        /// <summary>
        /// 检测项目名称
        /// </summary>
        public string? ItemName { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string? TaskName { get; set; }
        /// <summary>
        /// 工单名称
        /// </summary>
        public string? OrderName { get; set; }
        /// <summary>
        /// 质检状态（如页面中的“未质检/已质检”）
        /// </summary>
        public int? QualityStatus { get; set; }
        /// <summary>
        /// 报工记录关联ID（关联 ReportworkRecord 表主键）
        /// 用于关联对应的报工记录
        /// </summary>
        public int? ReportworkRecordId { get; set; }
        /// <summary>
        /// 报工时间
        /// </summary>
        public string? ReportingDate { get; set; }
        /// <summary>
        /// 检验开始时间
        /// </summary>
        public string? QualitystartDate { get; set; }
        /// <summary>
        /// 检验结束时间
        /// </summary>
        public string? QualityendDate { get; set; }
        /// <summary>
        /// 工序id
        /// </summary>
        public int? ProcessId { get; set; }
        /// <summary>
        /// 产品id
        /// </summary>
        public int? ProductId { get; set; }
        /// <summary>
        /// 站点id
        /// </summary>
        public int? SiteId { get; set; }
        /// <summary>
        /// 班组id
        /// </summary>
        public int? TeamId { get; set; }
        /// <summary>
        /// 检测类型
        /// </summary>
        public int? InspectionItemTypeId { get; set; }
        public string? Inspector { get; set; }
        public string? InspectionDept { get; set; }
        public string? InspectionResult { get; set; }
    }
    /// <summary>
    /// 报工质检添加dto
    /// </summary>
    public class CreateReportworkQualityInspectionDto 
    {
        public int Id {  get; set; }
        /// <summary>
        /// 检验单id
        /// </summary>
        public int InspectionItemId { get; set; }
        /// <summary>
        /// 检测项目名称
        /// </summary>
        public string ItemsName { get; set; }
        /// <summary>
        /// 检测项目编号
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// 检测类型
        /// </summary>
        public int InspectionItemTypeId { get; set; }
        /// <summary>
        /// 检测项目类型名称
        /// </summary>
        public string InspectionItemTypeName { get; set; }
        /// <summary>
        /// 检验部门（如页面中的“质检部门”）
        /// </summary>
        public string InspectionDept { get; set; }

        /// <summary>
        /// 检验人（如页面中的“李丽丽”）
        /// </summary>
        public string Inspector { get; set; }
        /// <summary>
        /// 报工记录关联ID（关联 ReportworkRecord 表主键）
        /// 用于关联对应的报工记录
        /// </summary>
        public int ReportworkRecordId { get; set; }
        /// <summary>
        /// 报工人员
        /// </summary>
        public string ReportingPerson { get; set; }
        /// <summary>
        /// 报工时间
        /// </summary>
        public DateTime ReportingDate { get; set; }
        /// <summary>
        /// 检验时间
        /// </summary>
        public DateTime? QualityDate { get; set; }
        /// <summary>
        /// 报工数量
        /// </summary>
        public int ReportingQuantity { get; set; }

        /// <summary>
        /// 生产工单id
        /// </summary>
        public int WorkOrderId { get; set; }
        /// <summary>
        /// 工单编号1
        /// </summary>
        public string OrderNumber { get; set; }
        /// <summary>
        /// 工单名称
        /// </summary>
        public string OrderName { get; set; }

        /// <summary>
        /// 工单任务id
        /// </summary>
        public int WorkOrderTasksId { get; set; }
        /// <summary>
        /// 任务编号1
        /// </summary>
        public string TaskNumber { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 站点id
        /// </summary>
        public int SiteId { get; set; }
        /// <summary>
        /// 站点名称
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// 工序id
        /// </summary>
        public int ProcessId { get; set; }
        /// <summary>
        /// 工序编号1
        /// </summary>
        public string ProcessCode { get; set; }
        /// <summary>
        /// 工序名称
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// 班组id
        /// </summary>
        public int TeamId { get; set; }
        /// <summary>
		/// 班组名称
		/// </summary>
        public string TeamName { get; set; }
        /// <summary>
		/// 任务负责人
		/// </summary>
        public string Director { get; set; }

        /// <summary>
        /// 产品id
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 规格型号
        /// </summary>
        public string Specification { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 产品类型(成品/半成品等)
        /// </summary>
        public string ProductType { get; set; }
        /// <summary>
        /// 合格数量
        /// </summary>
        public int? Qualified { get; set; }

        /// <summary>
        /// 不合格数量
        /// </summary>
        public int? Unqualified { get; set; }

        /// <summary>
        /// 合格率（格式如页面中的“60%”）
        /// </summary>
        public string? QualifiedRate { get; set; }

        /// <summary>
        /// 检测结果（如页面中的“合格”）
        /// </summary>
        public string? InspectionResult { get; set; }

        /// <summary>
        /// 质检状态（如页面中的“未质检/已质检”）
        /// </summary>
        public int QualityStatus { get; set; }
    }
    /// <summary>
    /// 创建检测项目表
    /// </summary>
    public class CreateInspectionItem
    {
        public int Id { get; set; }
        /// <summary>
        /// 检测项目名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 检测项目编号
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// 检测类型
        /// </summary>
        public int InspectionItemTypeId { get; set; }

        /// <summary>
        /// 检测工具（如：卡尺、显微镜等）(工装夹具类型表 )
        /// </summary>
        public int ToolingFixtureTypeId { get; set; }

        /// <summary>
        /// 检测要求（文字描述检测规范）
        /// </summary>
        public string InspectionRequirement { get; set; }

        /// <summary>
        /// 标准值（检测的基准数值）
        /// </summary>
        public string StandardValue { get; set; }

        /// <summary>
        /// 单位（如：mm、kg、℃等）
        /// </summary>
        public int UnitId { get; set; }

        /// <summary>
        /// 误差上限
        /// </summary>
        public decimal? ToleranceUpper { get; set; }

        /// <summary>
        /// 误差下限
        /// </summary>
        public decimal? ToleranceLower { get; set; }

        /// <summary>
        /// 致命缺陷数
        /// </summary>
        public int CriticalDefectCount { get; set; }

        /// <summary>
        /// 严重缺陷数
        /// </summary>
        public int SeriousDefectCount { get; set; }

        /// <summary>
        /// 轻微缺陷数
        /// </summary>
        public int MinorDefectCount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
    public class CreateInspectionResult
    {
        /// <summary>
        /// 关联的检验项目ID（外键，关联检验单主表）
        /// 用于串联“检验单 - 检验结果”数据
        /// </summary>
        public int InspectionSheetId { get; set; }
        /// <summary>
        /// 检验时间
        /// </summary>
        public DateTime? QualityDate { get; set; }
        /// <summary>
        /// 检测数量
        /// 对应页面“检测数量”字段
        /// </summary>
        public int InspectionQuantity { get; set; }

        /// <summary>
        /// 合格数量
        /// 对应页面“合格数量”字段
        /// </summary
        public int QualifiedQuantity { get; set; }

        /// <summary>
        /// 不合格数量
        /// 对应页面“不合格数量”字段
        /// </summary>
        public int UnqualifiedQuantity { get; set; }
        /// <summary>
        /// 合格率
        /// </summary>
        public string QualifiedRate { get; set; }
        /// <summary>
        /// 致命缺陷率（格式如：0%）
        /// 对应页面“致命缺陷率”字段
        /// </summary>
        public string CriticalDefectRate { get; set; }

        /// <summary>
        /// 严重缺陷率（格式如：0%）
        /// 对应页面“严重缺陷率”字段
        /// </summary>
        public string SeriousDefectRate { get; set; }

        /// <summary>
        /// 轻微缺陷率（格式如：0%）
        /// 对应页面“轻微缺陷率”字段
        /// </summary>
        public string MinorDefectRate { get; set; }

        /// <summary>
        /// 检测结果（如：合格、不合格）
        /// 对应页面“检测结果”字段，可通过枚举管理
        /// </summary>
        public string InspectionConclusion { get; set; }

        /// <summary>
        /// 备注
        /// 对应页面“备注”输入框，用于扩展说明
        /// </summary>
        public string Remarks { get; set; }
    }
}
