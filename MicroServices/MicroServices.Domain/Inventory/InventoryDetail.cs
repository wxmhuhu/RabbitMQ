using MricoServices.Shared;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Domain.Inventory
{
    /// <summary>
    /// 盘点明细表
    /// </summary>
    [SugarTable("InventoryDetail")]
    public class InventoryDetail: AuditableEntity
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
        public  int  InventoryId { get; set; }//盘点单ID

        [Navigate(NavigateType.OneToOne, nameof(InventoryId))]
        public Inventory Inventory { get; set; }//盘点单
    }

}
