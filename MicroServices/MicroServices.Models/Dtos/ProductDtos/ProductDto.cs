using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Models.Dtos.ProductDtos
{
    public class ProductDto
    {
        public int Id { get; set;}
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
        //public int Unit { get; set; }
        public string UnitName { get; set; }

        /// <summary>
        /// 产品类型(成品/半成品等)
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 50, ColumnDescription = "产品类型")]
        //public int ProductType { get; set; }
        public string ProductTypeName { get; set; }

        /// <summary>
        /// 产品属性(自制/外购/外协等)
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 50, ColumnDescription = "产品属性")]
        //public int ProductProperty { get; set; }
        public string ProductPropertyName { get; set; }
    }
    public class Search : PageModel
    {

    }
}
