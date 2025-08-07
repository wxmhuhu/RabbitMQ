using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Domain.ToolingFixtures
{
    /// <summary>
    /// 工装夹具+类型中间表
    /// </summary>
    [SugarTable("ToolingFixtureTypeRelation", TableDescription = "工装夹具+类型中间表")]
    public class ToolingFixtureTypeRelation
    { 

        [SugarColumn(ColumnName = "ToolingFixturesId")]
        public int ToolingFixturesId { get; set; }

        [SugarColumn(ColumnName = "ToolingFixturesTypeId")]
        public int ToolingFixturesTypeId { get; set; }
    }
}
