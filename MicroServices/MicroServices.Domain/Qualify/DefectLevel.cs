using MricoServices.Shared;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Domain.Qualify
{
    /// <summary>
    /// 缺陷等级表
    /// </summary>
    [SugarTable("DefectLevel", TableDescription = "缺陷等级表")]
    public class DefectLevel : AuditableEntity
    {
        /// <summary>
        /// 缺陷等级编码
        /// </summary>
        [SugarColumn(ColumnDescription = "缺陷等级编码", Length = 20)]
        public string? LevelCode { get; set; }
        /// <summary>
        /// 缺陷等级名称
        /// </summary>

        [SugarColumn(ColumnDescription = "缺陷等级名称")]
        public string? LevelName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>

        [SugarColumn(ColumnDescription = "状态", DefaultValue = "true")]
        public bool State { get; set; } = true;
        /// <summary>
        /// 备注
        /// </summary>

        [SugarColumn(ColumnDescription = "备注")]
        public string? Remark { get; set; }
    }
}
