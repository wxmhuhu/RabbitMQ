using MricoServices.Shared;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Domain.ProcessInfo
{
    // 工序管理表
    [SugarTable("Processes")]
    public class Processes : AuditableEntity
    {
        [SugarColumn(ColumnName = "ProcessName", IsNullable = false, ColumnDescription = "工序名称")]
        public string ProcessName { get; set; }

        [SugarColumn(ColumnName = "ProcessCode", IsNullable = true, ColumnDescription = "工序编号")]
        public string ProcessCode { get; set; }

        [SugarColumn(ColumnName = "States", IsNullable = true, ColumnDescription = "状态", ColumnDataType = "int")]
        public int? States { get; set; }

        [SugarColumn(ColumnName = "specification", IsNullable = true, ColumnDescription = "工序说明")]
        public string Specification { get; set; }

        [SugarColumn(ColumnName = "Remark", IsNullable = true, ColumnDescription = "备注")]
        public string Remark { get; set; }
    }

    // 工序组成表
    [SugarTable("ProcessComposition")]
    public class ProcessComposition: AuditableEntity
    {
        [SugarColumn(ColumnName = "SerialNumber", IsNullable = false, ColumnDescription = "序号", ColumnDataType = "int")]
        public int SerialNumber { get; set; }
        [SugarColumn(ColumnName = "ProcessId", IsNullable = false, ColumnDescription = "工序id", ColumnDataType = "int")]
        public int ProcessId { get; set; }
        [SugarColumn(ColumnName = "ProcessRouteId", IsNullable = false, ColumnDescription = "工艺路线id", ColumnDataType = "int")]
        public int ProcessRouteId { get; set; }
        [SugarColumn(ColumnName = "NexiProcessId", IsNullable = false, ColumnDescription = "下一道工序id", ColumnDataType = "int")]
        public int NexiProcessId { get; set; }
        [SugarColumn(ColumnName = "NextProcessRelation", IsNullable = true, ColumnDescription = "与下一道工序关系")]
        public string NextProcessRelation { get; set; }

        [SugarColumn(ColumnName = "KeyProcess", IsNullable = true, ColumnDescription = "关键工序")]
        public bool? KeyProcess { get; set; }

        [SugarColumn(ColumnName = "ReadinessTime", IsNullable = true, ColumnDescription = "准备时间", ColumnDataType = "int")]
        public int? ReadinessTime { get; set; }

        [SugarColumn(ColumnName = "WaitingTime", IsNullable = true, ColumnDescription = "等待时间", ColumnDataType = "int")]
        public int? WaitingTime { get; set; }

        [SugarColumn(ColumnName = "Colors", IsNullable = true, ColumnDescription = "颜色")]
        public string Colors { get; set; }

        [SugarColumn(ColumnName = "Remark", IsNullable = true)]
        public string Remark { get; set; }

        [Navigate(NavigateType.OneToOne, nameof(ProcessId))]
        public Processes ProcessInfo { get; set; }
    }

    // 工艺路线表
    [SugarTable("ProcessRoute")]
    public class ProcessRoute : AuditableEntity
    {
        [SugarColumn(ColumnName = "ProcessRouteCode", IsNullable = true, ColumnDescription = "工艺路线编号")]
        public string ProcessRouteCode { get; set; }

        [SugarColumn(ColumnName = "ProcessRouteName", IsNullable = true, ColumnDescription = "工艺路线名称")]
        public string ProcessRouteName { get; set; }

        [SugarColumn(ColumnName = "States", IsNullable = true, ColumnDescription = "状态", ColumnDataType = "int")]
        public int? States { get; set; }

        [SugarColumn(ColumnName = "Explain", IsNullable = true, ColumnDescription = "说明")]
        public string Explain { get; set; }

        [SugarColumn(ColumnName = "Remark", IsNullable = true, ColumnDescription = "备注")]
        public string Remark { get; set; }

        [Navigate(NavigateType.OneToOne, nameof(Id))]
        public ProcessComposition ProcessCompositionInfo { get; set; }
    }
}
