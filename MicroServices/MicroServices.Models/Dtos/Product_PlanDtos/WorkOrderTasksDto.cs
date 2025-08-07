using MricoServices.Shared;
using SqlSugar;

namespace MicroServices.Models.Dtos.Product_PlanDtos
{
    /// <summary>
    /// 工单任务表
    /// </summary>
    public class WorkOrderTasksDto:AuditableEntity
    {
        /// <summary>
        /// 任务编号
        /// </summary>
        public string TaskNumber { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }
        /// <summary>
        /// 站点名称
        /// </summary>
        /// </summary>
        public string SiteName { get; set; }
        /// <summary>
        /// 生产工单Id
        /// </summary>
        public int? WorkOrderId { get; set; } // INTEGER类型，如果可为NULL，使用int?
        /// <summary>
        /// 工单编号
        /// </summary>
        public string WorkOrderNumber { get; set; }
        /// <summary>
        /// 工单名称
        /// </summary>
        public string WorkOrderName { get; set; }

        /// <summary>
        /// 工艺流程
        /// </summary>
        public string ProcessRoute { get; set; }
        /// <summary>
        /// 工序名称
        /// </summary>
        public string ProcessName { get; set; }
        /// <summary>
        /// 工序编号
        /// </summary>
        public string ProcessNumber { get; set; }
        /// <summary>
        /// 任务颜色
        /// </summary>
        public string TaskColor { get; set; }
        /// <summary>
        /// 计划数量
        /// </summary>
        public int PlanNums { get; set; }
        /// <summary>
        /// 实际生产数量
        /// </summary>
        public int FactNums { get; set; }
        /// <summary>
        /// 计划开工时间
        /// </summary>
        public DateTime PlanStartTime { get; set; }
        /// <summary>
        /// 计划完工时间
        /// </summary>
        public DateTime PlanFinishTime { get; set; }
        /// <summary>
        /// 计划生产时长
        /// </summary>
        public string PlanProductLong { get; set; }
        /// <summary>
        /// 实际开工时间
        /// </summary>
        public DateTime FactStartTime { get; set; }
        /// <summary>
        /// 实际完工时间
        /// </summary>
        public DateTime FactFinishTime { get; set; }
        /// <summary>
        /// 实际生产时长
        /// </summary>
        public string FactProductLong { get; set; }
        /// <summary>
        /// 任务状态
        /// </summary>
        public int Status { get; set; } 
    }

    public class CreateUpdateWorkOrderTasksDtos
    {
        public int Id { get; set; } // 主键Id
        /// <summary>
        /// 计划数量
        /// </summary>
        public int? PlanNums { get; set; }
        /// <summary>
        /// 计划开工时间
        /// </summary>
        public DateTime? PlanStartTime { get; set; }
        /// <summary>
        /// 计划完工时间
        /// </summary>
        public DateTime? PlanFinishTime { get; set; }
        /// <summary>
        /// 需求日期
        /// </summary>
        public DateTime? NeedTime { get; set; }

        /// <summary>
        /// 生产工单Id
        /// </summary>
        public int? WorkOrderId { get; set; } 

        /// <summary>
        /// 任务状态 1、未派工 2、已下达 3、进行中 4、已暂停 5、已完成 6、已关闭
        /// </summary>
        public int? Status { get; set; } = 1; // 任务状态，默认为1（未派工）

        public string Remark { get; set; } // 备注
        /// <summary>
        /// 班组Id（派工时必填）
        /// </summary>
        public int? ClassGroupId { get; set; }
        /// <summary>
        /// 负责人Id（派工时必填）
        /// </summary>
        public int? MainPeople { get; set; }
        /// <summary>
        /// 派工备注
        /// </summary>
        public string DispatchRemark { get; set; }
        /// <summary>
        /// 质检部门Id
        /// </summary>
        public int? DepartmentId { get; set; }
        /// <summary>
        /// 质检人员Id
        /// </summary>
        public int? PeopleId { get; set; }
        /// <summary>
        /// 质检备注
        /// </summary>
        public string QualityRemark { get; set; }
    }
    /// <summary>
    /// 工单任务查询
    /// </summary>
    public class SearchWorkOrderTasksDto : PageModel
    {

    }
}
