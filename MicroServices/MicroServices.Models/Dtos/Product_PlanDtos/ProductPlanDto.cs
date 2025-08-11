using SqlSugar;
using System.ComponentModel;

namespace MicroServices.Models.Dtos.Product_PlanDtos
{
    /// <summary>
    /// 生产计划列表DTO
    /// </summary>
    public class ProductPlanDto
    {
        public int Id { get; set; }
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
        public int? OrderNums { get; set; } 
        public int FromType { get; set; }
        public string? FromTypeName { get; set; }

        /// <summary>
        /// 成品Id(物料)
        /// </summary>
        [SugarColumn(ColumnDescription = "成品Id(物料)")]
        public int? ProductId { get; set; } // 注意同上

        /// <summary>
        /// 成品名称
        /// </summary>
        [SugarColumn(Length = 255, ColumnDescription = "成品名称")]
        public string ProductName { get; set; }

        /// <summary>
        /// 成品编号
        /// </summary>
        [SugarColumn(Length = 255, ColumnDescription = "成品编号")]
        public string Product_Id { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        [SugarColumn(Length = 255, ColumnDescription = "规格型号")]
        public string SpecificationModel { get; set; }

        /// <summary>
        /// 成品类型
        /// </summary>
        [SugarColumn(ColumnDescription = "成品类型")]
        public string? PoductType { get; set; } // 注意同上

        /// <summary>
        /// 单位
        /// </summary>
        [SugarColumn(Length = 255, ColumnDescription = "单位")]
        public string Unit { get; set; }

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
        public string Remark { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        [SugarColumn(Length = 255, ColumnDescription = "附件")]
        public string Annex { get; set; }

        /// <summary>
        /// BOMId
        /// </summary>
        [SugarColumn(ColumnDescription = "BOMId")]
        public int? BomId { get; set; } // 注意同上

        /// <summary>
        /// 状态
        /// </summary>
        [SugarColumn(ColumnDescription = "状态")]
        public int? Status { get; set; }
    }

    public class CreateUpdateProductionPlanDto
    {
        /// <summary>
        /// 计划编号
        /// </summary>
        [SugarColumn(Length = 255, ColumnDescription = "编号")]
        public string Plan_Id { get; set; }

        /// <summary>
        /// 计划名称
        /// </summary>
        [SugarColumn(Length = 255, ColumnDescription = "计划名称")]
        public string? Plan_Name { get; set; }

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
        public string Product_Id { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        [SugarColumn(Length = 255, ColumnDescription = "规格型号")]
        public string SpecificationModel { get; set; }

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
        public int? BomId { get; set; } 

        /// <summary>
        /// 状态
        /// </summary>
        [SugarColumn(ColumnDescription = "状态")]
        public int? Status { get; set; }
    }


    public class  Search : PageModel
    {
        /// <summary>
        /// 计划编号
        /// </summary>
        public string? Plan_Id { get; set; }
        /// <summary>
        /// 计划名称
        /// </summary>
        public string? Plan_Name { get; set ; }
        /// <summary>
        /// 订单来源
        /// </summary>
        [DefaultValue(0)]
        public int? FromType { get; set; } 
        /// <summary>
        /// 产品名称
        /// </summary>
        public string? ProductName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [DefaultValue(0)]
        public int? Status { get; set; } 
    }

    /// <summary>
    /// RabbitMQ消息模型 - 生产计划创建完成
    /// </summary>
    public class ProductPlanCreatedMessage
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public string MessageId { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 消息类型
        /// </summary>
        public string MessageType { get; set; } = "ProductPlanCreated";

        /// <summary>
        /// 消息时间戳
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.Now;

        /// <summary>
        /// 生产计划ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 生产计划编号
        /// </summary>
        public string PlanId { get; set; }

        /// <summary>
        /// 生产计划名称
        /// </summary>
        public string PlanName { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 计划数量
        /// </summary>
        public int? PlanNums { get; set; }

        /// <summary>
        /// 开工日期
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 完工日期
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
