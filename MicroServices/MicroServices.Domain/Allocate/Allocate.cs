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
    /// 调拨表
    /// </summary>
    [SugarTable("Allocate")]
    public class Allocate:AuditableEntity
    {

        [SugarColumn(ColumnName = "IsSystemGenerated")]
        public bool IsSystemGenerated { get; set; } = false;//系统自动编号

        [SugarColumn(ColumnName = "AllocateNumber")]
        public string? AllocateNumber { get; set; }//挑拨单编号

        [SugarColumn(ColumnName = "AllocateDate")]
        public DateTime AllocateDate { get; set; } = DateTime.Now;//挑拨单日期

        [SugarColumn(ColumnName = "Reviewer")]
        public string Reviewer { get; set; }//审核人

        [SugarColumn(ColumnName = "ReviewersDate")]
        public DateTime? ReviewDate { get; set; } = DateTime.Now;//审核时间

        [SugarColumn(ColumnName = "Creator")]
        public string Creator { get; set; }//创建人

        [SugarColumn(ColumnName = "CreatDate")]
        public DateTime CreateDate { get; set; } = DateTime.Now;//创建时间

        [SugarColumn(ColumnName = "IsReviewed")]
        public bool IsReviewed { get; set; } = false;//是否审核

        [SugarColumn(ColumnName = "State")]
        public int State { get; set; } = 1; //状态

        [SugarColumn(ColumnName = "Remark")]
        public string? Remark { get; set; }//备注 
    }
}
