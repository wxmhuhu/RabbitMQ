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
    /// 缺陷类型表
    /// </summary>
    [SugarTable("DefectionType", TableDescription = "缺陷类型表")]
    public class DefectionType : AuditableEntity
    {
        /// <summary>
        /// 缺陷类型编码
        /// </summary>
        [SugarColumn(ColumnDescription = "缺陷类型编码", IsNullable = true)]
        public string? TypeCode { get; set; }
        /// <summary>
        /// 缺陷类型名称
        /// </summary>

        [SugarColumn(ColumnDescription = "缺陷类型名称", IsNullable = true)]
        public string? TypeName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>

        [SugarColumn(ColumnDescription = "状态", DefaultValue = "true")]
        public bool State { get; set; } = true;
        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnDescription = "备注", IsNullable = true)]
        public string? Remark { get; set; }
    }
}
