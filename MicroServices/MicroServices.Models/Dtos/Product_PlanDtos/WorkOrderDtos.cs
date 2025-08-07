using MricoServices.Shared;
using SqlSugar;

namespace MicroServices.Models.Dtos.Product_PlanDtos
{
    /// <summary>
    /// 工单任务表
    /// </summary>
    public class WorkOrderDtos : AuditableEntity
    {
        public int Id { get; set; }
        /// <summary>
        /// 工单编号
        /// </summary>
        [SugarColumn(ColumnName = "OrderNumber", IsNullable = false)]
        public string OrderNumber { get; set; }

        /// <summary>
        /// 工单名称
        /// </summary>
        [SugarColumn(ColumnName = "OrderName")]
        public string OrderName { get; set; }

        /// <summary>
        /// 计划Id
        /// </summary>
        [SugarColumn(ColumnName = "PlanId")]
        public int PlanId { get; set; } // INTEGER类型，如果可为NULL，使用int?

        /// <summary>
        /// 工单进度
        /// </summary>
        [SugarColumn(ColumnName = "OrderProgress")]
        public string OrderProgress { get; set; }

        /// <summary>
        /// 关联计划
        /// </summary>
        public string PlannName { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductNumber { get; set; }
        /// <summary>
        /// 规格型号
        /// </summary>
        public string SpecificationModel { get; set; }
        /// <summary>
        /// 产品类型
        /// </summary>
        public string FinishedProduceType { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 需求日期
        /// </summary>
        public DateTime NeedTime { get; set; }
        /// <summary>
        /// 计划数量
        /// </summary>
        public int PlanNums { get; set; }
        /// <summary>
        /// // 实际生产数量
        /// </summary>
        public int FactProduceNums { get; set; }

        /// <summary>
        /// 计划开工时间
        /// </summary>
        [SugarColumn(ColumnName = "Start_Time")]
        public DateTime? PlanStartTime { get; set; }

        /// <summary>
        /// 计划完工时间
        /// </summary>
        [SugarColumn(ColumnName = "End_Time")]
        public DateTime? PlanEndTime { get; set; }
        /// <summary>
        /// 实际开工时间
        /// </summary>
        public DateTime FactStartTime { get; set; }
        /// <summary>
        /// 实际完工时间
        /// </summary>
        public DateTime FactFinishTime { get; set; }

        /// <summary>
        /// 工单状态
        /// </summary>
        [SugarColumn(ColumnName = "Status")]
        public int? Status { get; set; } // INTEGER类型，如果可为NULL，使用int?
    }

    public class CreateUpdateWorkOrderDtos
    {
        /// <summary>
        /// 工单编号
        /// </summary>
        [SugarColumn(ColumnName = "OrderNumber", IsNullable = false)]
        public string OrderNumber { get; set; }

        /// <summary>
        /// 工单名称
        /// </summary>
        [SugarColumn(ColumnName = "OrderName")]
        public string OrderName { get; set; }

        /// <summary>
        /// 工单进度
        /// </summary>
        [SugarColumn(ColumnName = "OrderProgress")]
        public string OrderProgress { get; set; }

        /// <summary>
        /// 计划Id
        /// </summary>
        [SugarColumn(ColumnName = "PlanId")]
        public int? PlanId { get; set; } // INTEGER类型，如果可为NULL，使用int?

        /// <summary>
        /// 工单状态
        /// </summary>
        [SugarColumn(ColumnName = "Status")]
        public int? Status { get; set; }
    }

    public class SearchWorkOrderDtos : PageModel
    {
        /// <summary>
        /// 工单编号
        /// </summary>
        public string? OrderNumber { get; set; }
        /// <summary>
        /// 工单名称
        /// </summary>
        public string? OrderName { get; set; }
        /// <summary>
        /// 计划Id
        /// </summary>
        public int? PlanId { get; set; } // INTEGER类型，如果可为NULL，使用int?
        /// <summary>
        /// 计划名称
        /// </summary>
        public string? PlanName { get; set; }

        /// <summary>
        /// 工单状态
        /// </summary>
        public int? Status { get; set; }
        /// <summary>
        /// 排产信息
        /// </summary>
        public class ProductionSchedulingDto
        {
            public int Id { get; set; }
            /// <summary>
            /// 工单编号
            /// </summary>
            [SugarColumn(ColumnName = "OrderNumber", IsNullable = false)]
            public string OrderNumber { get; set; }

            /// <summary>
            /// 工单名称
            /// </summary>
            [SugarColumn(ColumnName = "OrderName")]
            public string OrderName { get; set; }

            /// <summary>
            /// 计划Id
            /// </summary>
            [SugarColumn(ColumnName = "PlanId")]
            public int PlanId { get; set; } // INTEGER类型，如果可为NULL，使用int?
            public string? PlanName { get; set; }
            /// <summary>
            /// 产品名称
            /// </summary>
            public string ProductName { get; set; }
            /// <summary>
            /// 产品编号
            /// </summary>
            public string ProductNumber { get; set; }
            /// <summary>
            /// 规格型号
            /// </summary>
            public string SpecificationModel { get; set; }
            /// <summary>
            /// 产品类型
            /// </summary>
            public string FinishedProduceType { get; set; }
            /// <summary>
            /// 单位
            /// </summary>
            public string Unit { get; set; }
            public int BomId { get; set; }
            /// <summary>
            /// BOM编号
            /// </summary>
            [SugarColumn(ColumnName = "BomCode", IsNullable = false)]
            public string BomCode { get; set; }
            /// <summary>
            /// BOM版本
            /// </summary>
            [SugarColumn(ColumnName = "BomVersion", IsNullable = false)]
            public string BomVersion { get; set; }
            /// <summary>
            /// 工艺路线Id
            /// </summary>
            [SugarColumn(ColumnName = "ProcessRouteId", IsNullable = false)]
            public int ProcessRouteId { get; set; }
        }
    }

    /// <summary>
    /// 工单搜索条件
    /// </summary>
    public class SearchWorkOrderDto : PageModel
    {
        /// <summary>
        /// 工单编号
        /// </summary>
        public string? OrderNumber { get; set; }

        /// <summary>
        /// 工单名称
        /// </summary>
        public string? OrderName { get; set; }

        /// <summary>
        /// 计划名称
        /// </summary>
        public string? PlanName { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string? ProductName { get; set; }

        /// <summary>
        /// 工单状态
        /// </summary>
        public int? Status { get; set; }
    }
}
