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
    /// 生产计划表 
    /// </summary>
    [SugarTable("ProductPlan", TableDescription = "生产计划表")]
    public class ProductPlan : AuditableEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        [SugarColumn(Length = 255, ColumnDescription = "编号")]
        public string Plan_Id { get; set; }

        /// <summary>
        /// 计划名称
        /// </summary>
        [SugarColumn(Length = 255, ColumnDescription = "计划名称")]
        public string Plan_Name { get; set; }

        /// <summary>
        /// 工单数量
        /// </summary>
        [SugarColumn(ColumnDescription = "工单数量")]
        public int? OrderNums { get; set; } // 注意：SQL 中是 INTEGER，C# 中默认 int 不可空，如果可能为 null，请使用 int?

        /// <summary>
        /// 来源类型
        /// </summary>
        [SugarColumn(ColumnDescription = "来源类型")]
        public int? FromType { get; set; } // 注意同上
        /// <summary>
        /// 成品Id(物料)
        /// </summary>
        [SugarColumn(ColumnDescription = "成品Id(物料)")]
        public int? ProductId { get; set; } // 注意同上

        /// <summary>
        /// 成品名称
        /// </summary>
        [SugarColumn(Length = 255, ColumnDescription = "成品名称")]
        public string? ProductName { get; set; }

        /// <summary>
        /// 成品编号
        /// </summary>
        [SugarColumn(Length = 255, ColumnDescription = "成品编号")]
        public string? Product_Id { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        [SugarColumn(Length = 255, ColumnDescription = "规格型号")]
        public string? SpecificationModel { get; set; }

        /// <summary>
        /// 成品类型
        /// </summary>
        [SugarColumn(ColumnDescription = "成品类型")]
        public string? PoductType { get; set; } // 注意同上

        /// <summary>
        /// 单位
        /// </summary>
        [SugarColumn(Length = 255, ColumnDescription = "单位")]
        public string? Unit { get; set; }

        /// <summary>
        /// 计划数量
        /// </summary>
        [SugarColumn(ColumnDescription = "计划数量")]
        public int? PlanNums { get; set; } // 注意同上

        /// <summary>
        /// 开工日期
        /// </summary>
        [SugarColumn(ColumnDataType = "DATE", ColumnDescription = "开工日期")] // 显式指定数据库列类型为 DATE
        public DateTime? StartTime { get; set; } // 日期类型建议使用可空 DateTime?

        /// <summary>
        /// 完工日期
        /// </summary>
        [SugarColumn(ColumnDataType = "DATE", ColumnDescription = "完工日期")]
        public DateTime? EndTime { get; set; } // 日期类型建议使用可空 DateTime?

        /// <summary>
        /// 需求日期
        /// </summary>
        [SugarColumn(ColumnDataType = "DATE", ColumnDescription = "需求日期")]
        public DateTime? NeedTime { get; set; } // 日期类型建议使用可空 DateTime?

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(Length = 255, ColumnDescription = "备注")]
        public string? Remark { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        [SugarColumn(Length = 255, ColumnDescription = "附件")]
        public string? Annex { get; set; }

        /// <summary>
        /// BOMId
        /// </summary>
        [SugarColumn(ColumnDescription = "BOMId")]
        public int? BomId { get; set; } // 注意同上

        /// <summary>
        /// 状态
        /// </summary>
        [SugarColumn(ColumnDescription = "状态")] //1、未分解 2、已分解 3、已撤回 4、进行中 5、已完成 6、已关闭
        public int? Status { get; set; }
    }

    public class SourceType : AuditableEntity
    {
        [SugarColumn(Length = 255, ColumnDescription = "来源类型名称")]
        public string? SourceTypeName { get; set;}
    }
}
