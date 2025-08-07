using MricoServices.Shared;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Domain.ToolingFixtures
{
    /// <summary>
    /// 工装夹具归还表
    /// </summary>
    [SugarTable("ToolingFixtureReturn", TableDescription = "工装夹具归还表")]
    public class ToolingFixtureReturn: AuditableEntity
    { 

        /// <summary>
        /// 类型编号
        /// </summary>
        [SugarColumn(ColumnName = "ToolingFixtureReturnNumber")]
        public string ToolingFixtureReturnNumber { get; set; }

        /// <summary>
        /// 是否系统自动生成ID
        /// </summary>
        [SugarColumn(ColumnName = "IsSystemGenerated")]
        public bool IsSystemGenerated { get; set; }

        
        /// <summary>
        /// 归还名称
        /// </summary>
        [SugarColumn(ColumnName = "ToolingFixturesReturnName")]
        public string ToolingFixturesReturnName { get; set; }

        public string ReturnPerson { get; set; }      // 归还人
        public DateTime ReturnDate { get; set; }  // 归还时间
        public int Quantity { get; set; }        // 归还数量
        public int ReturnState { get; set; }        // 归还类型
        public string Remark { get; set; }        // 备注

        // 关联工具
        public List<int> ToolingFixturesReturnDetailId { get; set; } 
        //归还明细表
        public List<ToolingFixturesReturnDetail> ToolingFixturesReturnDetail { get; set; }
    }
}
