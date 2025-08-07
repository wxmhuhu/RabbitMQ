using MricoServices.Shared;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Domain.Bom
{
    [SugarTable("BOM", TableDescription = "Bom表")]
    public class BOM:AuditableEntity
    {
        /// <summary>
        /// BOM编号
        /// </summary>
        [SugarColumn(ColumnName = "BomCode", IsNullable = false)]
        public string BomCode { get; set; }
        /// <summary>
        /// 默认Bom
        /// </summary>
        [SugarColumn(ColumnName = "DefaultBom", IsNullable = false)]
        public bool DefaultBom { get; set; }
        /// <summary>
        /// BOM版本
        /// </summary>
        [SugarColumn(ColumnName = "BomVersion", IsNullable = false)]
        public string BomVersion { get; set; }
        /// <summary>
        /// 产品Id
        /// </summary>
        [SugarColumn(ColumnName = "ProductId", IsNullable = false, ColumnDataType = "int")]
        public int ProductId { get; set; }
        /// <summary>
        /// 工艺路线Id
        /// </summary>
        [SugarColumn(ColumnName = "ProcessRouteId", IsNullable = false, ColumnDataType = "int")]
        public int ProcessRouteId { get; set; }
        /// <summary>
        /// 日产量
        /// </summary>
        [SugarColumn(ColumnName = "DailyOutput", IsNullable = false)]
        public string DailyOutput { get; set; }
        /// <summary>
        /// 父级Bom Id
        /// </summary>
        [SugarColumn(ColumnName = "BomParentId", IsNullable = false, ColumnDataType = "int")]
        public int BomParentId { get; set; }
    }

    [SugarTable("BomProductMaterial", TableDescription = "Bom产品物料表")]
    public class BomProductMaterial : AuditableEntity
    {
        /// <summary>
        /// BOM Id
        /// </summary>
        [SugarColumn(ColumnName = "BomId", IsNullable = false)]
        public string BomId { get; set; }
        /// <summary>
        /// 产品Id
        /// </summary>
        [SugarColumn(ColumnName = "ProductId", IsNullable = false, ColumnDataType = "int")]
        public int ProductId { get; set; }
        /// <summary>
        /// 物料Id
        /// </summary>
        [SugarColumn(ColumnName = "MaterialId", IsNullable = false, ColumnDataType = "int")]
        public int MaterialId { get; set; }
        /// <summary>
        /// 物料类型
        /// </summary>
        [SugarColumn(ColumnName = "MaterialTypeId", IsNullable = false)]
        public string MaterialTypeId { get; set; }
    }

    [SugarTable("ProcessBomMaterialProduct", TableDescription = "工序Bom物料产品表")]
    public class ProcessBomMaterialProduct : AuditableEntity
    {
        /// <summary>
        /// 工序Id
        /// </summary>
        [SugarColumn(ColumnName = "ProcessId", IsNullable = false, ColumnDataType = "int")]
        public int ProcessId { get; set; }
        /// <summary>
        /// 产品Id
        /// </summary>
        [SugarColumn(ColumnName = "ProductId", IsNullable = false, ColumnDataType = "int")]
        public int ProductId { get; set; }
        /// <summary>
        /// 物料Id
        /// </summary>
        [SugarColumn(ColumnName = "MaterialId", IsNullable = false, ColumnDataType = "int")]
        public int MaterialId { get; set; }
    }
}
