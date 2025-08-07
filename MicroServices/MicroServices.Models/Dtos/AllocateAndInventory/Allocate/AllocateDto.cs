using MicroServices.Domain.Allocate;
using MicroServices.Models.Dtos.Product_PlanDtos;
using MricoServices.Shared;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Models.Dtos.AllocateAndInventory.Allocate
{
    public class AllocateDto
    {

    } 
    public class CreateUpdateAllocateDto
    {
        [SugarColumn(ColumnName = "IsSystemGenerated")]
        public bool IsSystemGenerated { get; set; } = false;//系统自动编号

        [SugarColumn(ColumnName = "AllocateNumber")]
        public string? AllocateNumber { get; set; }//挑拨单编号

        [SugarColumn(ColumnName = "AllocateDate")]
        public DateTime AllocateDate { get; set; } = DateTime.Now;//挑拨单日期 

        [SugarColumn(ColumnName = "Remark")]
        public string? Remark { get; set; }//备注 
    }
    public class AllocateDetailDto
    {
    }
    public class CreateUpdateAllocateDetailDto
    {
    }
    public class AllocateSearch: PageModel
    {
        /// <summary>
        /// 调拨编号
        /// </summary>
        public string? number { get; set; }
        /// <summary>
        /// 调拨日期
        /// </summary>
        public DateTime? date { get; set; }
        /// <summary>
        /// 调拨状态
        /// </summary>
        public int? state { get; set; }
    }
}
