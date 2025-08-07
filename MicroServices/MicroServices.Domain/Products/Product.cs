using MricoServices.Shared;
using SqlSugar;

namespace MicroServices.Domain.Materials
{
    /// <summary>
    /// 产品表
    /// </summary>
    [SugarTable("Product", TableDescription = "产品表")]
    public class Product : AuditableEntity
    {
        /// <summary>
        /// 产品编号
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 50, ColumnDescription = "产品编号")]
        public string ProductCode { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 100, ColumnDescription = "产品名称")]
        public string ProductName { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 100, ColumnDescription = "规格型号")]
        public string Specification { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 20, ColumnDescription = "单位")]
        public int Unit { get; set; }

        /// <summary>
        /// 产品类型(成品/半成品等)
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 50, ColumnDescription = "产品类型")]
        public int ProductType { get; set; }

        /// <summary>
        /// 产品属性(自制/外购/外协等)
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 50, ColumnDescription = "产品属性")]
        public int ProductProperty { get; set; }

        /// <summary>
        /// 产品分类ID
        /// </summary>
        [SugarColumn(IsNullable = false, ColumnDescription = "产品分类ID")]
        public int CategoryId { get; set; }

        /// <summary>
        /// 状态(启用/禁用)
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 20, ColumnDescription = "状态")]
        public int Status { get; set; }

        /// <summary>
        /// 有效期
        /// 可空类型，对应数据库可空列
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "有效期")]
        public int? ValidFrom { get; set; }

        /// <summary>
        /// 预警天数
        /// 可空类型，对应数据库可空列
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "预警天数")]
        public int? WarningDays { get; set; }

        /// <summary>
        /// 库存上限
        /// 可空类型，对应数据库可空列
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "库存上限")]
        public decimal? InventoryUpperLimit { get; set; }

        /// <summary>
        /// 库存下限
        /// 可空类型，对应数据库可空列
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "库存下限")]
        public decimal? InventoryLowerLimit { get; set; }

        /// <summary>
        /// 采购价格
        /// 可空类型，对应数据库可空列
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "采购价格")]
        public decimal? PurchasePrice { get; set; }

        /// <summary>
        /// 销售价格
        /// 可空类型，对应数据库可空列
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDescription = "销售价格")]
        public decimal? SalesPrice { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 200, ColumnDescription = "图片路径")]
        public string ImagePath { get; set; }

        /// <summary>
        /// 附件路径
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 200, ColumnDescription = "附件路径")]
        public string AttachmentPath { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 500, ColumnDescription = "备注")]
        public string Remark { get; set; }
    }
}
