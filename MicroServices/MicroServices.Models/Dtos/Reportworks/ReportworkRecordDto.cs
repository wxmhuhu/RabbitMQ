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
    /// 报工记录列表
    /// </summary>
    public class ReportworkRecordDto: AuditableEntity
    {



        /// <summary>
        /// 生产工单id
        /// </summary>
        public int WorkOrderId { get; set; }
        /// <summary>
        /// 工单编号
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
        /// 任务编号
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
        /// 工艺路线id
        /// </summary>
        public int ProcessRouteId { get; set; }
        /// <summary>
        /// 工艺流程
        /// </summary>
        public string ProcessRouteName {  get; set; }
        /// <summary>
        /// 工序id
        /// </summary>
        public int ProcessId { get; set; }
        /// <summary>
        /// 工序编号
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
        public int ProductId {  get; set; }
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
        public string  ProductType { get; set; }


        /// <summary>
        /// 报工人员
        /// </summary>
        public string ReportingPerson { get; set; }

        /// <summary>
        /// 报工数量
        /// </summary>
        public int ReportingQuantity { get; set; }

        /// <summary>
        /// 报工时间
        /// </summary>
        public DateTime ReportingDate { get; set; }

        /// <summary>
        /// 质检时间
        /// </summary>
        public DateTime? QualityDate { get; set; }

        /// <summary>
        /// 合格数量
        /// </summary>
        public int? Qualified { get; set; }

        /// <summary>
        /// 不合格数量
        /// </summary>
        public int? Unqualified { get; set; }

        /// <summary>
        /// 合格率
        /// </summary>
        public string? QualifiedRate { get; set; }

        /// <summary>
        /// 检测结果
        /// </summary>
        public string? InspectionResult { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Desc { get; set; }


    }
    /// <summary>
    /// 报工记录查询
    /// </summary>
    public class SearchReportworkRecordDto: PageModel
    {
        /// <summary>
        /// 任务编号
        /// </summary>
        public string? TaskNumber { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string? TaskName { get; set; }
        /// <summary>
        /// 工单编号
        /// </summary>
        public string? OrderNumber { get; set; }
        /// <summary>
        /// 工单名称
        /// </summary>
        public string? OrderName { get; set; }
        /// <summary>
        /// 报工人员
        /// </summary>
        public string? ReportingPerson { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }
        /// <summary>
        /// 班组id
        /// </summary>
        public int? TeamId { get; set; }
        /// <summary>
        /// 报工时间
        /// </summary>
        public string? ReportingDate { get; set; }

        /// <summary>
        /// 质检开始时间
        /// </summary>
        public string? QualitystartDate { get; set; }
         /// <summary>
        /// 质检结束时间
        /// </summary>
        public string? QualityendDate { get; set; }
        /// <summary>
        /// 工艺路线id
        /// </summary>
        public int? ProcessRouteId { get; set; }
        /// <summary>
        /// 工序id
        /// </summary>
        public int? ProcessId { get; set; }
        /// <summary>
        /// 任务负责人
        /// </summary>
        public string? Director { get; set; }
        /// <summary>
        /// 检测结果
        /// </summary>
        public string? InspectionResult { get; set; }
        /// <summary>
        /// 站点id
        /// </summary>
        public int? SiteId { get; set; }

    }
    /// <summary>
    /// 报工记录添加
    /// </summary>
    public class CreateReportworkRecordDto 
    {
        public int Id {  get; set; }
        /// <summary>
        /// 生产工单id
        /// </summary>
        public int WorkOrderId { get; set; }
        /// <summary>
        /// 工单编号
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
        /// 任务编号
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
        /// 工艺路线id
        /// </summary>
        public int ProcessRouteId { get; set; }
        /// <summary>
        /// 工艺流程
        /// </summary>
        public string ProcessRouteName { get; set; }
        /// <summary>
        /// 工序id
        /// </summary>
        public int ProcessId { get; set; }
        /// <summary>
        /// 工序编号
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
        /// 报工人员
        /// </summary>
        public string ReportingPerson { get; set; }

        /// <summary>
        /// 报工数量
        /// </summary>
        public int ReportingQuantity { get; set; }

        /// <summary>
        /// 报工时间
        /// </summary>
        public DateTime ReportingDate { get; set; }

        /// <summary>
        /// 质检时间
        /// </summary>
        public DateTime? QualityDate { get; set; }

        /// <summary>
        /// 合格数量
        /// </summary>
        public int? Qualified { get; set; }

        /// <summary>
        /// 不合格数量
        /// </summary>
        public int? Unqualified { get; set; }

        /// <summary>
        /// 合格率
        /// </summary>
        public string? QualifiedRate { get; set; }

        /// <summary>
        /// 检测结果
        /// </summary>
        public string? InspectionResult { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Desc { get; set; }

    }
}
