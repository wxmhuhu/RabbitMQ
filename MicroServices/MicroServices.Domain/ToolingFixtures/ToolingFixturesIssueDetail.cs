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
    /// 工具领取明细表
    /// </summary>
    [SugarTable("ToolingFixturesIssueDetail")]
    public class ToolingFixtureIssueDetail : AuditableEntity
    {

        [SugarColumn(ColumnName = "ToolingNumber")]
        public string ToolingNumber { get; set; }//工装夹具编号

        [SugarColumn(ColumnName = "ToolingName")]
        public string ToolingName { get; set; }//工装夹具名称

        [SugarColumn(ColumnName = "ToolingType")]
        public string ToolingType { get; set; }//工装夹具类型

        [SugarColumn(ColumnName = "ToolingModel")]
        public string ToolingModel { get; set; }//工装夹具型号

        [SugarColumn(ColumnName = "Location")]
        public string Location { get; set; }//所在库位

        [SugarColumn(ColumnName = "备注")]
        public string Remark { get; set; }//Remark

        [SugarColumn(ColumnName = "IssueId")]
        public int IssueId { get; set; }//工具领取表主键

        // 导航属性
        [Navigate(NavigateType.OneToOne, nameof(IssueId))]
        public ToolingFixtureIssue Issue { get; set; }
    }
}
