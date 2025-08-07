using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Models.Dtos
{
    /// <summary>
    /// 工序
    /// </summary>
    public class ProcessDto
    {
        public int Id { get; set; }
        public string ProcessName { get; set; }
        public string ProcessCode { get; set; }
        public int? States { get; set; }
        public string Specification { get; set; }
        public string Remark { get; set; }
    }

    /// <summary>
    /// 创建或编辑工序
    /// </summary>
    public class CreateOrUpdateProcessDto
    {
        public string ProcessName { get; set; }
        public string ProcessCode { get; set; }
        public int? States { get; set; }
        public string Specification { get; set; }
        public string Remark { get; set; }
    }

    /// <summary>
    /// 工序组成
    /// </summary>
    public class ProcessCompositionDto
    {
        public int Id { get; set; }
        [SugarColumn(ColumnName = "SerialNumber", IsNullable = false, ColumnDescription = "序号")]
        public int SerialNumber { get; set; }
        [SugarColumn(ColumnName = "ProcessId", IsNullable = false, ColumnDescription = "工序id")]
        public int ProcessId { get; set; }
        [SugarColumn(ColumnName = "ProcessRouteId", IsNullable = false, ColumnDescription = "工艺路线id")]
        public string ProcessCode { get; set; }
        public string ProcessName { get; set; }
        public int ProcessRouteId { get; set; }
        [SugarColumn(ColumnName = "NexiProcessId", IsNullable = false, ColumnDescription = "下一道工序id")]
        public int NexiProcessId { get; set; }
        [SugarColumn(ColumnName = "NextProcessRelation", IsNullable = true, ColumnDescription = "与下一道工序关系")]
        public string NextProcessRelation { get; set; }

        [SugarColumn(ColumnName = "KeyProcess", IsNullable = true, ColumnDescription = "关键工序")]
        public bool? KeyProcess { get; set; }

        [SugarColumn(ColumnName = "ReadinessTime", IsNullable = true, ColumnDescription = "准备时间")]
        public int? ReadinessTime { get; set; }

        [SugarColumn(ColumnName = "WaitingTime", IsNullable = true, ColumnDescription = "等待时间")]
        public int? WaitingTime { get; set; }

        [SugarColumn(ColumnName = "Colors", IsNullable = true, ColumnDescription = "颜色")]
        public string Colors { get; set; }

        [SugarColumn(ColumnName = "Remark", IsNullable = true)]
        public string Remark { get; set; }
    }

    /// <summary>
    /// 创建或编辑工序组合
    /// </summary>
    public class CreateOrUpdateProcessCompositionDto
    {
        
        [SugarColumn(ColumnName = "SerialNumber", IsNullable = false, ColumnDescription = "序号")]
        public int SerialNumber { get; set; }
        [SugarColumn(ColumnName = "ProcessId", IsNullable = false, ColumnDescription = "工序id")]
        public int ProcessId { get; set; }
        [SugarColumn(ColumnName = "ProcessRouteId", IsNullable = false, ColumnDescription = "工艺路线id")]
        public int ProcessRouteId { get; set; }
        [SugarColumn(ColumnName = "NexiProcessId", IsNullable = false, ColumnDescription = "下一道工序id")]
        public int NexiProcessId { get; set; }
        [SugarColumn(ColumnName = "NextProcessRelation", IsNullable = true, ColumnDescription = "与下一道工序关系")]
        public string NextProcessRelation { get; set; }

        [SugarColumn(ColumnName = "KeyProcess", IsNullable = true, ColumnDescription = "关键工序")]
        public bool? KeyProcess { get; set; }

        [SugarColumn(ColumnName = "ReadinessTime", IsNullable = true, ColumnDescription = "准备时间")]
        public int? ReadinessTime { get; set; }

        [SugarColumn(ColumnName = "WaitingTime", IsNullable = true, ColumnDescription = "等待时间")]
        public int? WaitingTime { get; set; }

        [SugarColumn(ColumnName = "Colors", IsNullable = true, ColumnDescription = "颜色")]
        public string Colors { get; set; }

        [SugarColumn(ColumnName = "Remark", IsNullable = true)]
        public string Remark { get; set; }
    }

    /// <summary>
    /// 工艺路线
    /// </summary>
    public class ProcessRouteDto
    {
        public int Id { get; set; }
        [SugarColumn(ColumnName = "ProcessRouteCode", IsNullable = true, ColumnDescription = "工艺路线编号")]
        public string ProcessRouteCode { get; set; }

        [SugarColumn(ColumnName = "ProcessRouteName", IsNullable = true, ColumnDescription = "工艺路线名称")]
        public string ProcessRouteName { get; set; }

        [SugarColumn(ColumnName = "States", IsNullable = true, ColumnDescription = "状态")]
        public int? States { get; set; }

        [SugarColumn(ColumnName = "Explain", IsNullable = true, ColumnDescription = "说明")]
        public string Explain { get; set; }

        [SugarColumn(ColumnName = "Remark", IsNullable = true, ColumnDescription = "备注")]
        public string Remark { get; set; }
    }

    /// <summary>
    /// 创建或编辑工艺路线
    /// </summary>
    public class CreateOrUpdateProcessRouteDto
    {
        [SugarColumn(ColumnName = "ProcessRouteCode", IsNullable = true, ColumnDescription = "工艺路线编号")]
        public string ProcessRouteCode { get; set; }

        [SugarColumn(ColumnName = "ProcessRouteName", IsNullable = true, ColumnDescription = "工艺路线名称")]
        public string ProcessRouteName { get; set; }

        [SugarColumn(ColumnName = "States", IsNullable = true, ColumnDescription = "状态")]
        public int? States { get; set; }

        [SugarColumn(ColumnName = "Explain", IsNullable = true, ColumnDescription = "说明")]
        public string Explain { get; set; }

        [SugarColumn(ColumnName = "Remark", IsNullable = true, ColumnDescription = "备注")]
        public string Remark { get; set; }
    }
    public class Search : PageModel
    {

    }
}