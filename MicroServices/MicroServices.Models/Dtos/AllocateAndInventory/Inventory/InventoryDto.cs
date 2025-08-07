using MricoServices.Shared;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Models.Dtos.AllocateAndInventory.Inventory
{ 
    /// <summary>
    /// 盘点Dto
    /// </summary>
    public class InventoryDto  : AuditableEntity
    {
        [SugarColumn(ColumnName = "IsSystemGenerated")]
        public bool IsSystemGenerated { get; set; } = false;//是否系统生成

        [SugarColumn(ColumnName = "InventoryNumber")]
        public string? Number { get; set; }//盘点编号

        [SugarColumn(ColumnName = "InventoryDate")]
        public DateTime InventoryDate { get; set; } = DateTime.Now;//盘点日期

        [SugarColumn(ColumnName = "Reviewers")]
        public string? Reviewers { get; set; }//审核人

        [SugarColumn(ColumnName = "ReviewersDate")]
        public DateTime? ReviewersDate { get; set; } = DateTime.Now;//审核时间

        [SugarColumn(ColumnName = "Creator")]
        public string Creator { get; set; }//创建人

        [SugarColumn(ColumnName = "CreatDate")]
        public DateTime CreateDate { get; set; } = DateTime.Now;//创建时间

        [SugarColumn(ColumnName = "IsReviewed")]
        public bool IsReviewed { get; set; } = false;//是否审核

        [SugarColumn(ColumnName = "State")]
        public int State { get; set; } = 1;  //状态

        [SugarColumn(ColumnName = "Remark")]
        public string? Remark { get; set; }//备注  

    }

    public class InventoryDetailDto: AuditableEntity
    {
        [SugarColumn(ColumnName = "MaterialNumber")]
        public string MaterialNumber { get; set; }//物料编号

        [SugarColumn(ColumnName = "MaterialName")]
        public string MaterialName { get; set; }//物料名称

        [SugarColumn(ColumnName = "SpecificationModels")]
        public string SpecificationModels { get; set; }//规格型号

        [SugarColumn(ColumnName = "SinglePrice")]
        public decimal SinglePrice { get; set; }//单价

        [SugarColumn(ColumnName = "Uint")]
        public string Unit { get; set; }//单位

        [SugarColumn(ColumnName = "WarehouseId")]
        public int WarehouseId { get; set; }//仓库ID

        [SugarColumn(ColumnName = "BatchNumber")]
        public string BatchNumber { get; set; }//批次号

        [SugarColumn(ColumnName = "QuantityCount")]
        public int QuantityCount { get; set; }//账面数量

        [SugarColumn(ColumnName = "InventoryCount")]
        public int InventoryCount { get; set; }//盘点数量

        [SugarColumn(ColumnName = "InventoryPerson")]
        public string InventoryPerson { get; set; }//盘点人

        [SugarColumn(ColumnName = "Remark")]
        public string? Remark { get; set; }//备注

        [SugarColumn(ColumnName = "InventoryId")]
        public int InventoryId { get; set; }//盘点单ID 
    }
    /// <summary>
    /// 盘点创建或更新Dto
    /// </summary>
    public class CreateUpdateInventoryDto
    {
        [SugarColumn(ColumnName = "IsSystemGenerated")]
        public bool IsSystemGenerated { get; set; } = false;//是否系统生成

        [SugarColumn(ColumnName = "InventoryNumber")]
        public string? Number { get; set; }//盘点编号

        [SugarColumn(ColumnName = "InventoryDate")]
        public DateTime InventoryDate { get; set; } = DateTime.Now;//盘点日期

        [SugarColumn(ColumnName = "Reviewers")]
        public string Reviewers { get; set; }//审核人

        [SugarColumn(ColumnName = "ReviewersDate")]
        public DateTime? ReviewersDate { get; set; } = DateTime.Now;//审核时间

        [SugarColumn(ColumnName = "Creator")]
        public string Creator { get; set; }//创建人

        [SugarColumn(ColumnName = "CreatDate")]
        public DateTime CreateDate { get; set; } = DateTime.Now;//创建时间

        [SugarColumn(ColumnName = "IsReviewed")]
        public bool IsReviewed { get; set; } = false;//是否审核

        [SugarColumn(ColumnName = "State")]
        public int State { get; set; }  //状态

        [SugarColumn(ColumnName = "Remark")]
        public string? Remark { get; set; }//备注  
    }

    /// <summary>
    /// 盘点明细表创建或更新Dto
    /// </summary>
    public class CreateUpdateInventoryDetailDto
    {
        [SugarColumn(ColumnName = "MaterialNumber")]
        public string MaterialNumber { get; set; }//物料编号

        [SugarColumn(ColumnName = "MaterialName")]
        public string MaterialName { get; set; }//物料名称

        [SugarColumn(ColumnName = "SpecificationModels")]
        public string SpecificationModels { get; set; }//规格型号

        [SugarColumn(ColumnName = "SinglePrice")]
        public decimal SinglePrice { get; set; }//单价

        [SugarColumn(ColumnName = "Uint")]
        public string Unit { get; set; }//单位

        [SugarColumn(ColumnName = "WarehouseId")]
        public int WarehouseId { get; set; }//仓库ID

        [SugarColumn(ColumnName = "BatchNumber")]
        public string BatchNumber { get; set; }//批次号

        [SugarColumn(ColumnName = "QuantityCount")]
        public int QuantityCount { get; set; }//账面数量

        [SugarColumn(ColumnName = "InventoryCount")]
        public int InventoryCount { get; set; }//盘点数量

        [SugarColumn(ColumnName = "InventoryPerson")]
        public string InventoryPerson { get; set; }//盘点人

        [SugarColumn(ColumnName = "Remark")]
        public string? Remark { get; set; }//备注

        [SugarColumn(ColumnName = "InventoryId")]
        public int InventoryId { get; set; }//盘点单ID
    }
    /// <summary>
    /// 盘点查询 DTO
    /// </summary>
    public class Search : PageModel
    {
        /// <summary>
        /// 盘点编号
        /// </summary>
        public string? number { get; set; }
        /// <summary>
        /// /盘点日期
        /// </summary>
        public DateTime? date { get; set; }
        /// <summary>
        /// 盘点状态
        /// </summary>
        public int? state { get; set; }
    }
}
