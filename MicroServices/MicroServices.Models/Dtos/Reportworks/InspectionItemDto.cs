using MricoServices.Shared;
using System;

namespace MicroServices.Models.Dtos.Reportworks
{
    /// <summary>
    /// 检测项目显示DTO
    /// </summary>
    public class InspectionItemDto : AuditableEntity
    {
        /// <summary>
        /// 检测项目名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 检测项目编号
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// 检测类型ID
        /// </summary>
        public int InspectionItemTypeId { get; set; }

        /// <summary>
        /// 检测类型名称
        /// </summary>
        public string InspectionItemTypeName { get; set; }

        /// <summary>
        /// 检测工具ID（工装夹具类型表）
        /// </summary>
        public int ToolingFixtureTypeId { get; set; }

        /// <summary>
        /// 检测工具名称
        /// </summary>
        public string ToolingFixtureTypeName { get; set; }

        /// <summary>
        /// 检测要求（文字描述检测规范）
        /// </summary>
        public string InspectionRequirement { get; set; }

        /// <summary>
        /// 标准值（检测的基准数值）
        /// </summary>
        public string StandardValue { get; set; }

        /// <summary>
        /// 单位ID
        /// </summary>
        public int UnitId { get; set; }

        /// <summary>
        /// 单位名称（如：mm、kg、℃等）
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 误差上限
        /// </summary>
        public decimal? ToleranceUpper { get; set; }

        /// <summary>
        /// 误差下限
        /// </summary>
        public decimal? ToleranceLower { get; set; }

        /// <summary>
        /// 致命缺陷数
        /// </summary>
        public int CriticalDefectCount { get; set; }

        /// <summary>
        /// 严重缺陷数
        /// </summary>
        public int SeriousDefectCount { get; set; }

        /// <summary>
        /// 轻微缺陷数
        /// </summary>
        public int MinorDefectCount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
}