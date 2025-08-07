using MricoServices.Shared;
using SqlSugar;

namespace MicroServices.Domain.InStorage
{
	/// <summary>
	/// 领/退料单
	/// </summary>
	public class ReceiveOrReturn : AuditableEntity
	{
		/// <summary>
        /// 退料单编号
        /// </summary>
        [SugarColumn(ColumnName = "Number", ColumnDescription = "领/退料单编号")]
        public string Number { get; set; } 
		/// <summary>
        /// 领/退料单名称
        /// </summary>
        [SugarColumn(ColumnName = "Name", ColumnDescription = "领/退料单名称")]
        public string Name { get; set; }
		/// <summary>
        /// 退料日期
        /// </summary>
        [SugarColumn(ColumnName = "Time", ColumnDescription = "日期")]
        public DateTime Time { get; set; }
		/// <summary>
        /// 领/退料人
        /// </summary>
        [SugarColumn(ColumnName = "Member", ColumnDescription = "领/退料人")]
        public string Member { get; set; }
		/// <summary>
        /// 工单名称
        /// </summary>
        [SugarColumn(ColumnName = "WorkOrderName", ColumnDescription = "工单名称")]
        public int WorkOrderId { get; set; }
		/// <summary>
        /// 任务名称
        /// </summary>
        [SugarColumn(ColumnName = "TaskName", ColumnDescription = "任务名称")]
        public string TaskName { get; set; }
		/// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "Remark", ColumnDescription = "备注")]
        public string Remark { get; set; }
	}

}
