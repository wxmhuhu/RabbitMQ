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
    /// 工装夹具 
    /// </summary>
    [SugarTable("ToolingFixture", TableDescription = "工装夹具")]
    public class ToolingFixture:AuditableEntity
    {
        // <summary>
        /// 编号
        /// </summary>
        [SugarColumn(ColumnName = "ToolingFixtureNumber")]
        public string ToolingFixtureNumber { get; set; }

        /// <summary>
        /// 是否系统自动生成ID
        /// </summary>
        [SugarColumn(ColumnName = "IsSystemGenerated")]
        public bool IsSystemGenerated { get; set; }

        /// <summary>
        /// 工装夹具名称
        /// </summary>
        [SugarColumn(ColumnName = "ToolingFixturesName")]
        public string Name { get; set; }

        /// <summary>
        /// 工装夹具类型ID（外键）
        /// </summary>
        [SugarColumn(ColumnName = "ToolingFixturesTypeId")]
        public int TypeId { get; set; }

        /// <summary>
        /// 工装夹具型号
        /// </summary>
        [SugarColumn(ColumnName = "ToolingFixturesModel")]
        public string Model { get; set; }

        /// <summary>
        /// 库位ID（外键）
        /// </summary>
        [SugarColumn(ColumnName = "WareHouseId")]
        public int WareHouseId { get; set; }

        /// <summary>
        /// 预警次数
        /// </summary>
        [SugarColumn(ColumnName = "WarningCount")]
        public int WarningCount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "Remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 状态（可用/领用中/维修中/报废）
        /// </summary>
        [SugarColumn(ColumnName = "State")]
        public int State { get; set; }

        /// <summary>
        /// 关联站点
        /// </summary>
        [SugarColumn(ColumnName = "Site")]
        public int Site { get; set; }

        /// <summary>
        /// 使用次数
        /// </summary>
        [SugarColumn(ColumnName = "UsingNumber")]
        public int UsingCount { get; set; }

        /// <summary>
        /// 报废时间
        /// </summary>
        [SugarColumn(ColumnName = "ScrapTime")]
        public DateTime? ScrapTime { get; set; }

        /// <summary>
        /// 报废原因
        /// </summary>
        [SugarColumn(ColumnName = "ScrapReason")]
        public string ScrapReason { get; set; }

        // 导航属性（SqlSugar需手动加载）
        [Navigate(NavigateType.OneToOne, nameof(TypeId))]
        public ToolingFixtureType Type { get; set; }

        //[Navigate(NavigateType.OneToOne, nameof(WareHouseId))]
        //public WareHouse WareHouse { get; set; }
    }
}
