using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Models.Dtos.Bom
{
    public class BomDto
    {
        public int Id { get; set; }
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
        /// 产品Id
        /// </summary>
        [SugarColumn(ColumnName = "ProductId", IsNullable = false)]
        public int ProductId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 100, ColumnDescription = "产品名称")]
        public string ProductName { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 50, ColumnDescription = "产品编号")]
        public string ProductCode { get; set; }
        /// <summary>
        /// 规格型号
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 100, ColumnDescription = "规格型号")]
        public string Specification { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 20, ColumnDescription = "单位")]
        public string UniteName { get; set; }
        /// <summary>
        /// 默认Bom
        /// </summary>
        [SugarColumn(ColumnName = "DefaultBom", IsNullable = false)]
        public bool DefaultBom { get; set; }
        /// <summary>
        /// 日产量
        /// </summary>
        [SugarColumn(ColumnName = "DailyOutput", IsNullable = false)]
        public string DailyOutput { get; set; }
    }

    public class CreateOrUpdateBomDto
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
        [SugarColumn(ColumnName = "ProductId", IsNullable = false)]
        public int ProductId { get; set; }
        /// <summary>
        /// 工艺路线Id
        /// </summary>
        [SugarColumn(ColumnName = "ProcessRouteId", IsNullable = false)]
        public int ProcessRouteId { get; set; }
        /// <summary>
        /// 日产量
        /// </summary>
        [SugarColumn(ColumnName = "DailyOutput", IsNullable = false)]
        public string DailyOutput { get; set; }
        /// <summary>
        /// 父级Bom Id
        /// </summary>
        [SugarColumn(ColumnName = "BomParentId", IsNullable = false)]
        public int BomParentId { get; set; }
    }
    public class BomTreeDto : BomDto
    {
        /// <summary>
        /// 父级Bom Id
        /// </summary>
        [SugarColumn(ColumnName = "BomParentId", IsNullable = false)]
        public int BomParentId { get; set; }
        public List<BomTreeDto> Children { get; set; } = new();
    }
    public class Search: PageModel
    {
        /// <summary>
        /// BOM编号
        /// </summary>
        public string? BomCode { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string? ProductName { get; set; }
        /// <summary>
        /// BOM版本
        /// </summary>
        public string? BomVersion { get; set; }


    }
}
