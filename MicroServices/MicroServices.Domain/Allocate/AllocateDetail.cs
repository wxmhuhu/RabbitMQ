using MricoServices.Shared;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Domain.Allocate
{
    /// <summary>
    /// 调拨明细表
    /// </summary>
    [SugarTable("AllocateDetail", TableDescription = "调拨明细表")]
    public class AllocateDetail : AuditableEntity
    {
        [SugarColumn(ColumnName = "MaterialNumber")]
        public string MaterialNumber { get; set; }//物料编号

        [SugarColumn(ColumnName = "MaterialName")]
        public string MaterialName { get; set; }//物料名称

        [SugarColumn(ColumnName = "Specification")]
        public string Specification { get; set; }//规格型号

        [SugarColumn(ColumnName = "SinglePrice")]
        public decimal SinglePrice { get; set; }//单价

        [SugarColumn(ColumnName = "Uint")]
        public string Unit { get; set; }//单位

        [SugarColumn(ColumnName = "Quantity")]
        public int Quantity { get; set; }//调拨数量

        [SugarColumn(ColumnName = "Price")]
        public decimal Price { get; set; }//金额

        [SugarColumn(ColumnName = "OutWarehouseId")]
        public int OutWarehouseId { get; set; }//拨出仓库

        [SugarColumn(ColumnName = "OutBatchNumber")]
        public string OutBatchNumber { get; set; }//拨出批次

        [SugarColumn(ColumnName = "InWarehouseId")]
        public int InWarehouseId { get; set; }//拨入仓库

        [SugarColumn(ColumnName = "InBatchNumber")]
        public string InBatchNumber { get; set; }//拨入批次

        [SugarColumn(ColumnName = "Reason")]
        public string? Reason { get; set; }//理由

        [SugarColumn(ColumnName = "Remark")]
        public string? Remark { get; set; }//备注

        [SugarColumn(ColumnName = "AllocateId")]
        public int AllocateId { get; set; }//调拨单主键
        //导航属性

        [Navigate(NavigateType.OneToOne, nameof(AllocateId))]
        public Allocate?  Allocate { get; set; }
    }
}
