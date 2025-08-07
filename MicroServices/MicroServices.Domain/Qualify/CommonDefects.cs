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
    /// 常见缺陷表
    /// </summary>
    [SugarTable("CommonDefects", TableDescription = "常见缺陷表")]
    public class CommonDefects : AuditableEntity
    {
        /// <summary>
        /// 缺陷编码
        /// </summary>
        [SugarColumn(ColumnDescription = "缺陷编码", IsNullable = true)]
        public string? CommonDefectsCode { get; set; }
        /// <summary>
        /// 缺陷名称
        /// </summary>

        [SugarColumn(ColumnDescription = "缺陷名称", IsNullable = true)]
        public string? CommonDefectsName { get; set; }
        /// <summary>
        /// 缺陷类型id
        /// </summary>

        [SugarColumn(ColumnDescription = "缺陷类型id")]
        public int DefectionTypeId { get; set; }
        /// <summary>
        /// 缺陷位置id
        /// </summary>

        [SugarColumn(ColumnDescription = "缺陷位置id")]
        public int DefectLocationId { get; set; }
        /// <summary>
        /// 缺陷等级id
        /// </summary>

        [SugarColumn(ColumnDescription = "缺陷等级id")]
        public int DefectLevelId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>

        [SugarColumn(ColumnDescription = "状态", DefaultValue = "true")]
        public bool State { get; set; } = true;
        /// <summary>
        /// 缺陷描述
        /// </summary>

        [SugarColumn(ColumnDescription = "缺陷描述", IsNullable = true)]
        public string? DefectDesc { get; set; }
        /// <summary>
        /// 备注
        /// </summary>

        [SugarColumn(ColumnDescription = "备注", IsNullable = true)]
        public string? Remark { get; set; }
    }
}
