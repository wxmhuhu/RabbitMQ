using MricoServices.Shared;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Domain.ToolingFixtures
{
    /// <summary>
    /// 领取工具 表
    /// </summary>
    [SugarTable("ToolingFixturesIssue", TableDescription = "领取工具表")]
    public class ToolingFixturesIssue : AuditableEntity
    {
        public string IssuedTo { get; set; }      // 领取人
        public DateTime IssueDate { get; set; }  // 领取时间
        public int Quantity { get; set; }        // 领取数量

        // 关联工具
        public int ToolingFixtureId { get; set; }
        public ToolingFixture ToolingFixture { get; set; }

        // 关联库位（可选）
        public int? WareHouseId { get; set; }
        //public WareHouse WareHouse { get; set; }
    }
    /// </summary>
    /// 领取工具表
    /// </summary>
    [SugarTable("ToolingFixturesIssue")]
    public class ToolingFixtureIssue:AuditableEntity
    { 
        [SugarColumn(ColumnName = "ToolingFixturesIssueNumber")]
        [Display(Name = "领取编号")]
        public string IssueNumber { get; set; }

        [SugarColumn(ColumnName = "IsSystemGenerated")]
        public bool IsSystemGenerated { get; set; }

        [SugarColumn(ColumnName = "ToolingFixturesIssueName")]
        [Display(Name = "领取名称")]
        public string Name { get; set; }

        [SugarColumn(ColumnName = "IssuedTo")]
        [Display(Name = "领取人")]
        public string IssuedTo { get; set; }

        [SugarColumn(ColumnName = "IssueDate")]
        [Display(Name = "领取日期")]
        public DateTime IssueDate { get; set; }

        [SugarColumn(ColumnName = "Quantity")]
        [Display(Name = "数量")]
        public int Quantity { get; set; }

        [SugarColumn(ColumnName = "IssueState")]
        [Display(Name = "领取状态")]
        public string State { get; set; }

        [SugarColumn(ColumnName = "Remark")]
        public string Remark { get; set; }

        // 导航属性
        [Navigate(NavigateType.OneToMany, nameof(ToolingFixtureIssueDetail.IssueId))]
        public List<ToolingFixtureIssueDetail> Details { get; set; }
    }
}
