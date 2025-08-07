using MricoServices.Shared;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Domain.Inventory
{
    /// <summary>
    /// 盘点表
    /// </summary>
    [SugarTable("Inventory")]
    public class Inventory:AuditableEntity
    {

        [SugarColumn(ColumnName = "IsSystemGenerated")]
        public bool IsSystemGenerated { get; set; } = false;//是否系统生成

        [SugarColumn(ColumnName = "Number")]
        public string? Number { get; set; }//盘点编号

        [SugarColumn(ColumnName = "InventoryDate")]
        public DateTime InventoryDate { get; set; } = DateTime.Now;//盘点日期

        [SugarColumn(ColumnName = "Reviewers")]
        public string Reviewers { get; set; }//审核人

        [SugarColumn(ColumnName = "ReviewersDate")]
        public DateTime? ReviewersDate { get; set; } = DateTime.Now;//审核时间

        [SugarColumn(ColumnName = "Creator")]
        public string Creator { get; set; }//创建人

        [SugarColumn(ColumnName = "CreateDate")]
        public DateTime CreateDate { get; set; } = DateTime.Now;//创建时间

        [SugarColumn(ColumnName = "IsReviewed")]
        public bool IsReviewed { get; set; } = false;//是否审核

        [SugarColumn(ColumnName = "State")]
        public int State { get; set; } = 1; //状态

        [SugarColumn(ColumnName = "Remark")]
        public string? Remark { get; set; }//备注  
    }
}
