using MricoServices.Shared;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Domain.Reportworks
{
    /// <summary>
    /// 报工质检
    /// </summary>
    [SugarTable("ReportworkQualityInspection", TableDescription = "报工质检表")]
    public class ReportworkQualityInspection : AuditableEntity
    {
        /// <summary>
        /// 检验单id
        /// </summary>
        [SugarColumn(ColumnDescription = "检验单id", IsNullable = false)]
        public int InspectionItemId { get; set; }
        /// <summary>
        /// 检测项目名称
        /// </summary>
        [SugarColumn(ColumnName = "ItemsName", ColumnDescription = "检测项目名称", IsNullable = false)]
        public string ItemsName { get; set; } = string.Empty;
        /// <summary>
        /// 检测项目编号
        /// </summary>
        [SugarColumn(ColumnDescription = "检测项目编号", IsNullable = false)]
        public string ItemCode { get; set; }
        /// <summary>
        /// 检测类型
        /// </summary>
        [SugarColumn(ColumnDescription = "检测类型", IsNullable = false)]
        public int InspectionItemTypeId { get; set; }
        /// <summary>
        /// 检测项目类型名称
        /// </summary>
        [SugarColumn(ColumnDescription = "检测项目类型名称", IsNullable = false)]
        public string InspectionItemTypeName { get; set; }

        /// <summary>
        /// 报工记录关联ID（关联 ReportworkRecord 表主键）
        /// 用于关联对应的报工记录
        /// </summary>
        [SugarColumn(ColumnDescription = "报工记录关联ID", IsNullable = false)]
        public int ReportworkRecordId { get; set; }
        /// <summary>
        /// 报工人员
        /// </summary>
        [SugarColumn(ColumnDescription = "报工人员", IsNullable = false)]
        public string ReportingPerson { get; set; }
        /// <summary>
        /// 报工时间
        /// </summary>
        [SugarColumn(ColumnDescription = "报工时间", IsNullable = false)]
        public DateTime ReportingDate { get; set; }
        /// <summary>
        /// 检验时间
        /// </summary>
        [SugarColumn(ColumnDescription = "检验时间", IsNullable = true)]
        public DateTime? QualityDate { get; set; }
        /// <summary>
        /// 报工数量
        /// </summary>
        [SugarColumn(ColumnDescription = "报工数量", IsNullable = false)]
        public int ReportingQuantity { get; set; }

        /// <summary>
        /// 生产工单id
        /// </summary>
        [SugarColumn(ColumnDescription = "生产工单id", IsNullable = false)]
        public int WorkOrderId { get; set; }
        /// <summary>
        /// 工单编号1
        /// </summary>
        [SugarColumn(ColumnDescription = "工单编号", IsNullable = false)]
        public string OrderNumber { get; set; }
        /// <summary>
        /// 工单名称
        /// </summary>
        [SugarColumn(ColumnDescription = "工单名称", IsNullable = false)]
        public string OrderName { get; set; }

        /// <summary>
        /// 工单任务id
        /// </summary>
        [SugarColumn(ColumnDescription = "工单任务id", IsNullable = false)]
        public int WorkOrderTasksId { get; set; }
        /// <summary>
        /// 任务编号1
        /// </summary>
        [SugarColumn(ColumnDescription = "任务编号", IsNullable = false)]
        public string TaskNumber { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        [SugarColumn(ColumnDescription = "任务名称", IsNullable = false)]
        public string TaskName { get; set; }

        /// <summary>
        /// 站点id
        /// </summary>
        [SugarColumn(ColumnDescription = "站点id", IsNullable = false)]
        public int SiteId { get; set; }
        /// <summary>
        /// 站点名称
        /// </summary>
        [SugarColumn(ColumnDescription = "站点名称", IsNullable = false)]
        public string SiteName { get; set; }

        /// <summary>
        /// 工序id
        /// </summary>
        [SugarColumn(ColumnDescription = "工序id", IsNullable = false)]
        public int ProcessId { get; set; }
        /// <summary>
        /// 工序编号1
        /// </summary>
        [SugarColumn(ColumnDescription = "工序编号", IsNullable = false)]
        public string ProcessCode { get; set; }
        /// <summary>
        /// 工序名称
        /// </summary>
        [SugarColumn(ColumnDescription = "工序名称", IsNullable = false)]
        public string ProcessName { get; set; }

        /// <summary>
        /// 班组id
        /// </summary>
        [SugarColumn(ColumnDescription = "班组id", IsNullable = false)]
        public int TeamId { get; set; }
        /// <summary>
        /// 班组名称
        /// </summary>
        [SugarColumn(ColumnDescription = "班组名称", IsNullable = false)]
        public string TeamName { get; set; }
        /// <summary>
        /// 任务负责人
        /// </summary>
        [SugarColumn(ColumnDescription = "任务负责人", IsNullable = false)]
        public string Director { get; set; }

        /// <summary>
        /// 产品id
        /// </summary>
        [SugarColumn(ColumnDescription = "产品id", IsNullable = false)]
        public int ProductId { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        [SugarColumn(ColumnDescription = "产品编号", IsNullable = false)]
        public string ProductCode { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        [SugarColumn(ColumnDescription = "产品名称", IsNullable = false)]
        public string ProductName { get; set; }
        /// <summary>
        /// 规格型号
        /// </summary>
        [SugarColumn(ColumnDescription = "规格型号", IsNullable = false)]
        public string Specification { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [SugarColumn(ColumnDescription = "单位", IsNullable = false)]
        public string Unit { get; set; }
        /// <summary>
        /// 产品类型(成品/半成品等)
        /// </summary>
        [SugarColumn(ColumnDescription = "产品类型", IsNullable = false)]
        public string ProductType { get; set; }

        /// <summary>
        /// 检验部门（如页面中的“质检部门”）
        /// </summary>
        [SugarColumn(ColumnDescription = "检验部门", IsNullable = false)]
        public string InspectionDept { get; set; }

        /// <summary>
        /// 检验人（如页面中的“李丽丽”）
        /// </summary>
        [SugarColumn(ColumnDescription = "检验人", IsNullable = false)]
        public string Inspector { get; set; }
        /// <summary>
        /// 合格数量
        /// </summary>
        [SugarColumn(ColumnDescription = "合格数量", IsNullable = true)]
        public int? Qualified { get; set; }

        /// <summary>
        /// 不合格数量
        /// </summary>
        [SugarColumn(ColumnDescription = "不合格数量", IsNullable = true)]
        public int? Unqualified { get; set; }

        /// <summary>
        /// 合格率（格式如页面中的“60%”）
        /// </summary>
        [SugarColumn(ColumnDescription = "合格率", IsNullable = true)]
        public string? QualifiedRate { get; set; }

        /// <summary>
        /// 检测结果（如页面中的“合格”）
        /// </summary>
        [SugarColumn(ColumnDescription = "检测结果", IsNullable = true)]
        public string? InspectionResult { get; set; }

        /// <summary>
        /// 质检状态（如页面中的“未质检/已质检”）
        /// </summary>
        [SugarColumn(ColumnDescription = "质检状态", IsNullable = false)]
        public int QualityStatus { get; set; }
    }

    /// <summary>
    /// 报工质检类型表
    /// </summary>
    [SugarTable("InspectionType", TableDescription = "报工质检类型表")]
    public class InspectionType : AuditableEntity
    {
        /// <summary>
        /// 检验类型名称（如页面中的“首检”
        /// </summary>
        [SugarColumn(ColumnDescription = "InspectionTypeName")]
        public string InspectionTypeName { get; set; }
    }

    /// <summary>
    /// 检验项目表
    /// </summary>
    [SugarTable("InspectionItem ", TableDescription = "检验项目表")]
    public class InspectionItem : AuditableEntity
    {
        /// <summary>
        /// 检测项目名称
        /// </summary>
        [SugarColumn(ColumnDescription = "ItemName")]
        public string ItemName { get; set; }

        /// <summary>
        /// 检测项目编号
        /// </summary>
        [SugarColumn(ColumnDescription = "ItemCode")]
        public string ItemCode { get; set; }

        /// <summary>
        /// 检测类型
        /// </summary>
        [SugarColumn(ColumnDescription = "InspectionItemTypeId")]
        public int InspectionItemTypeId { get; set; }

        /// <summary>
        /// 检测工具（如：卡尺、显微镜等）(工装夹具类型表 )
        /// </summary>
        [SugarColumn(ColumnDescription = "ToolingFixtureTypeId")]
        public int ToolingFixtureTypeId { get; set; }

        /// <summary>
        /// 检测要求（文字描述检测规范）
        /// </summary>
        [SugarColumn(ColumnDescription = "InspectionRequirement")]
        public string InspectionRequirement { get; set; }

        /// <summary>
        /// 标准值（检测的基准数值）
        /// </summary>
        [SugarColumn(ColumnDescription = "StandardValue")]
        public string StandardValue { get; set; }

        /// <summary>
        /// 单位（如：mm、kg、℃等）
        /// </summary>
        [SugarColumn(ColumnDescription = "Unit")]
        public int UnitId { get; set; }

        /// <summary>
        /// 误差上限
        /// </summary>
        [SugarColumn(ColumnDescription = "ToleranceUpper", IsNullable = true)]
        public decimal? ToleranceUpper { get; set; }

        /// <summary>
        /// 误差下限
        /// </summary>
        [SugarColumn(ColumnDescription = "ToleranceLower", IsNullable = true)]
        public decimal? ToleranceLower { get; set; }

        /// <summary>
        /// 致命缺陷数
        /// </summary>
        [SugarColumn(ColumnDescription = "CriticalDefectCount")]
        public int CriticalDefectCount { get; set; }

        /// <summary>
        /// 严重缺陷数
        /// </summary>
        [SugarColumn(ColumnDescription = "SeriousDefectCount")]
        public int SeriousDefectCount { get; set; }

        /// <summary>
        /// 轻微缺陷数
        /// </summary>
        [SugarColumn(ColumnDescription = "MinorDefectCount")]
        public int MinorDefectCount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnDescription = "Remarks")]
        public string Remarks { get; set; }

    }

    /// <summary>
    /// 检测项目类型表
    /// </summary>
    [SugarTable("InspectionItemType", TableDescription = "检测项目类型表")]
    public class InspectionItemType : AuditableEntity
    {
        /// <summary>
        /// 检测项目类型名称
        /// </summary>
        [SugarColumn(ColumnDescription = "InspectionItemTypeName")]
        public string InspectionItemTypeName { get; set; }
    }

    /// <summary>
    /// 检验结果表
    /// 记录单次检验的统计结果、缺陷率及结论
    /// </summary>
    [SugarTable("InspectionResult", TableDescription = "检验结果表")]
    public class InspectionResult : AuditableEntity
    {
        /// <summary>
        /// 关联的检验项目ID（外键，关联检验单主表）
        /// 用于串联“检验单 - 检验结果”数据
        /// </summary>
        [SugarColumn(ColumnDescription = "InspectionSheetId")]
        public int InspectionSheetId { get; set; }
        /// <summary>
        /// 检验时间
        /// </summary>
        public DateTime? QualityDate { get; set; }
        /// <summary>
        /// 检测数量
        /// 对应页面“检测数量”字段
        /// </summary>
        [SugarColumn(ColumnDescription = "InspectionQuantity")]
        public int InspectionQuantity { get; set; }

        /// <summary>
        /// 合格数量
        /// 对应页面“合格数量”字段
        /// </summary>
        [SugarColumn(ColumnDescription = "QualifiedQuantity")]
        public int QualifiedQuantity { get; set; }

        /// <summary>
        /// 不合格数量
        /// 对应页面“不合格数量”字段
        /// </summary>
        [SugarColumn(ColumnDescription = "UnqualifiedQuantity")]
        public int UnqualifiedQuantity { get; set; }
        /// <summary>
        /// 合格率
        /// </summary>
        [SugarColumn(ColumnDescription = "UnqualifiedQuantity")]
        public string QualifiedRate { get; set; }
        /// <summary>
        /// 致命缺陷率（格式如：0%）
        /// 对应页面“致命缺陷率”字段
        /// </summary>
        [SugarColumn(ColumnDescription = "CriticalDefectRate")]
        public string CriticalDefectRate { get; set; }

        /// <summary>
        /// 严重缺陷率（格式如：0%）
        /// 对应页面“严重缺陷率”字段
        /// </summary>
        [SugarColumn(ColumnDescription = "SeriousDefectRate")]
        public string SeriousDefectRate { get; set; }

        /// <summary>
        /// 轻微缺陷率（格式如：0%）
        /// 对应页面“轻微缺陷率”字段
        /// </summary>
        [SugarColumn(ColumnDescription = "MinorDefectRate")]
        public string MinorDefectRate { get; set; }

        /// <summary>
        /// 检测结果（如：合格、不合格）
        /// 对应页面“检测结果”字段，可通过枚举管理
        /// </summary>
        [SugarColumn(ColumnDescription = "InspectionConclusion")]
        public string InspectionConclusion { get; set; }

        /// <summary>
        /// 备注
        /// 对应页面“备注”输入框，用于扩展说明
        /// </summary>
        [SugarColumn(ColumnDescription = "Remarks")]
        public string Remarks { get; set; }
    }
}
