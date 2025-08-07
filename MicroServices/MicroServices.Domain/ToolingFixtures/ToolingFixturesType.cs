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
    /// 工装夹具类型
    /// </summary>
    [SugarTable("ToolingFixtureType", TableDescription = "工装夹具类型")]
    public class ToolingFixtureType: AuditableEntity
    {
        /// <summary>
        /// 类型编号
        /// </summary>
        [SugarColumn(ColumnName = "ToolingFixtureTypeNumber")]
        public string ToolingFixtureTypeNumber { get; set; }

        /// <summary>
        /// 是否系统自动生成ID
        /// </summary>
        [SugarColumn(ColumnName = "IsSystemGenerated")]
        public bool IsSystemGenerated { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        [SugarColumn(ColumnName = "ToolingFixtureTypeId")]
        public int ToolingFixtureTypeId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [SugarColumn(ColumnName = "State")]
        public int State { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "Remark")]
        public string Remark { get; set; }

        // 导航属性（一对多）
        [Navigate(NavigateType.OneToMany, nameof(ToolingFixture.TypeId))]
        public List<ToolingFixture> Tools { get; set; }
    }
}
