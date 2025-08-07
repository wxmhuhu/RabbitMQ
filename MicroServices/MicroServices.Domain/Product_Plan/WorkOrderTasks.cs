using MricoServices.Shared;
using SqlSugar;

namespace MicroServices.Domain.Product_Plan
{
    /// <summary>
    /// 工单任务表
    /// </summary>
    [SugarTable("WorkOrderTasks", TableDescription = "工单任务表")]
    public class WorkOrderTasks : AuditableEntity
    {
        /// <summary>
        /// 任务编号
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string? TaskNumber { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string? TaskName { get; set; }

        /// <summary>
        /// 站点名称
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string? SiteName { get; set; }
        /// <summary>
        /// 生产工单Id
        /// </summary>
        [SugarColumn(ColumnName = "WorkOrderId")]
        public int? WorkOrderId { get; set; }
        /// <summary>
        /// 计划数量
        /// </summary>
        public int? PlanNums { get; set; }
        /// <summary>
        /// 实际生产数量
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int? FactNums { get; set; }
        /// <summary>
        /// 计划开工时间
        /// </summary>
        public DateTime? PlanStartTime { get; set; }
        /// <summary>
        /// 计划完工时间
        /// </summary>
        public DateTime? PlanFinishTime { get; set; }
        /// <summary>
        /// 计划生产时长
        /// </summary>
        public string? PlanProductLong { get; set; }
        /// <summary>
        /// 实际开工时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? FactStartTime { get; set; }
        /// <summary>
        /// 实际完工时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? FactFinishTime { get; set; }
        /// <summary>
        /// 实际生产时长
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string? FactProductLong { get; set; }
        /// <summary>
        /// 需求日期
        /// </summary>
        public DateTime? NeedTime { get; set; } 
        /// <summary>
        /// 状态 1、未派工 2、已下达 3、进行中 4、已暂停 5、已完成 6、已关闭
        /// </summary>
        [SugarColumn(ColumnName = "Status")]
        public int? Status { get; set; } // INTEGER类型，如果可为NULL，使用int?

        public string Remark { get; set; } // 备注

        /// <summary>
        /// 班组Id
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int ClassGroupId { get; set; }
        /// <summary>
        /// 负责人Id
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int MainPeople { get; set; }
        /// <summary>
        /// 派工备注
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string DispatchRemark { get; set; }
        /// <summary>
        /// 质检部门Id
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int DepartmentId { get; set; }
        /// <summary>
        /// 质检人员Id
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int PeopleId { get; set; }
        /// <summary>
        /// 质检备注
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string QualityRemark { get; set; } 
    }
}
