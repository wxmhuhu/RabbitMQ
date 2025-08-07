using MricoServices.Shared;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Domain.Materials
{
    /// <summary>
    /// 物料表
    /// </summary>
    [SugarTable("Material", TableDescription = "物料表")]
    public class Material : AuditableEntity
    {
        /// <summary>
        /// 物料编号
        /// </summary>
        [SugarColumn(ColumnName = "MaterialCode", IsNullable = false)]
        public string MaterialCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        [SugarColumn(ColumnName = "MaterialName", IsNullable = false)]
        public string MaterialName { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        [SugarColumn(ColumnName = "Specification")]
        public string Specification { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [SugarColumn(ColumnName = "Unit", IsNullable = false)]
        public int Unit { get; set; }

        /// <summary>
        /// 物料类型(原材料/半成品/成品等)
        /// </summary>
        [SugarColumn(ColumnName = "MaterialTypeId", IsNullable = false)]
        public int MaterialTypeId { get; set; }

        /// <summary>
        /// 物料属性(自制/外购/外协等)
        /// </summary>
        [SugarColumn(ColumnName = "MaterialPropertyId", IsNullable = false)]
        public int MaterialPropertyId { get; set; }

        /// <summary>
        /// 物料分类ID
        /// </summary>
        [SugarColumn(ColumnName = "CategoryId", IsNullable = false)]
        public int CategoryId { get; set; }

        /// <summary>
        /// 状态(启用/禁用)
        /// </summary>
        [SugarColumn(ColumnName = "Status", IsNullable = false)]
        public string Status { get; set; }

        /// <summary>
        /// 有效开始日期
        /// </summary>
        [SugarColumn(ColumnName = "ValidFrom", IsNullable = true)]
        public DateTime? ValidFrom { get; set; }

        /// <summary>
        /// 有效结束日期
        /// </summary>
        [SugarColumn(ColumnName = "ValidTo", IsNullable = true)]
        public DateTime? ValidTo { get; set; }

        /// <summary>
        /// 预警天数
        /// </summary>
        [SugarColumn(ColumnName = "WarningDays", IsNullable = true)]
        public int? WarningDays { get; set; }

        /// <summary>
        /// 库存上限
        /// </summary>
        [SugarColumn(ColumnName = "InventoryUpperLimit", IsNullable = true)]
        public decimal? InventoryUpperLimit { get; set; }

        /// <summary>
        /// 库存下限
        /// </summary>
        [SugarColumn(ColumnName = "InventoryLowerLimit", IsNullable = true)]
        public decimal? InventoryLowerLimit { get; set; }

        /// <summary>
        /// 采购价格
        /// </summary>
        [SugarColumn(ColumnName = "PurchasePrice", IsNullable = true)]
        public decimal? PurchasePrice { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        [SugarColumn(ColumnName = "ImagePath")]
        public string ImagePath { get; set; }

        /// <summary>
        /// 附件路径
        /// </summary>
        [SugarColumn(ColumnName = "AttachmentPath")]
        public string AttachmentPath { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "Remark")]
        public string Remark { get; set; }
    }
    /// <summary>
    /// 单位表
    /// </summary>
    [SugarTable("Unite", TableDescription = "单位表")]
    public class Unite : AuditableEntity
    {
        [SugarColumn(ColumnName = "UniteName")]
        public string UniteName { get; set; }
    }
    /// <summary>
    /// 类型
    /// </summary>
    [SugarTable("TypeInfos", TableDescription = "类型表")]
    public class TypeInfos : AuditableEntity
    {
        [SugarColumn(ColumnName = "TypeName")]
        public string TypeName { get; set; }
    }
    /// <summary>
    /// 分类
    /// </summary>
    [SugarTable("Category", TableDescription = "分类表")]
    public class Category : AuditableEntity
    {
        [SugarColumn(ColumnName = "CategoryName")]
        public string CategoryName { get; set; }
        [SugarColumn(ColumnName = "ParentId", ColumnDataType = "int")]
        public int ParentId { get; set; }
    }
    /// <summary>
    /// 属性
    /// </summary>
    [SugarTable("PropertyInfos", TableDescription = "属性表")]
    public class PropertyInfos : AuditableEntity
    {
        [SugarColumn(ColumnName = "MaterialPropertyName")]
        public string MaterialPropertyName { get; set; }
    }
}
