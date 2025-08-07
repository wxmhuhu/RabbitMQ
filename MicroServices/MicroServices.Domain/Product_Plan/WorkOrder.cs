using MricoServices.Shared;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Domain.Product_Plan
{
    /// <summary>
    /// 生产工单表
    /// </summary>
    [SugarTable("WorkOrder", TableDescription = "生产工单表")]
    public class WorkOrder : AuditableEntity
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
        public int? PlanId { get; set; }

        /// <summary>
        /// 工单状态 1 待排产 2 已排产 3 未开始 4 进行中 5 已暂停 6 已完成 7 部分完成 8 已关闭 9 已取消
        /// </summary>
        [SugarColumn(ColumnName = "Status")]
        public int? Status { get; set; } 
    }
}
