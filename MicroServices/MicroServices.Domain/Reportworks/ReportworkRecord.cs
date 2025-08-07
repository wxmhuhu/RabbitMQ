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
    /// 报工记录表
    /// </summary>
    [SugarTable("ReportworkRecord", TableDescription = "报工记录表")]
    public class ReportworkRecord : AuditableEntity
    {
        /// <summary>
        /// 生产工单id
        /// </summary>
        [SugarColumn(ColumnDescription = "生产工单id", IsNullable = false)]
        public int WorkOrderId { get; set; }
        /// <summary>
        /// 工单编号
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
        /// 任务编号
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
        /// 工艺路线id
        /// </summary>
        [SugarColumn(ColumnDescription = "工艺路线id", IsNullable = false)]
        public int ProcessRouteId { get; set; }
        /// <summary>
        /// 工艺流程
        /// </summary>
        [SugarColumn(ColumnDescription = "工艺流程", IsNullable = false)]
        public string ProcessRouteName { get; set; }
        /// <summary>
        /// 工序id
        /// </summary>
        [SugarColumn(ColumnDescription = "工序id", IsNullable = false)]
        public int ProcessId { get; set; }
        /// <summary>
        /// 工序编号
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
        /// 报工人员
        /// </summary>
        [SugarColumn(ColumnDescription = "报工人员", IsNullable = false)]
        public string ReportingPerson { get; set; }

        /// <summary>
        /// 报工数量
        /// </summary>
        [SugarColumn(ColumnDescription = "报工数量", IsNullable = false)]
        public int ReportingQuantity { get; set; }

        /// <summary>
        /// 报工时间
        /// </summary>
        [SugarColumn(ColumnDescription = "报工时间", IsNullable = false)]
        public DateTime ReportingDate { get; set; }

        /// <summary>
        /// 质检时间
        /// </summary>
        [SugarColumn(ColumnDescription = "质检时间", IsNullable = true)]
        public DateTime? QualityDate { get; set; }

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
        /// 合格率
        /// </summary>
        [SugarColumn(ColumnDescription = "合格率", IsNullable = true)]
        public string? QualifiedRate { get; set; }

        /// <summary>
        /// 检测结果
        /// </summary>
        [SugarColumn(ColumnDescription = "检测结果", IsNullable = true)]
        public string? InspectionResult { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [SugarColumn(ColumnDescription = "状态", IsNullable = false)]
        public int Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
       [SugarColumn(ColumnDescription = "备注", IsNullable = true)]
        public string Desc {  get; set; }

    }
}
