using MricoServices.Shared;
using SqlSugar;

namespace MicroServices.Domain.InStorage
{
	/// <summary>
	/// 产品入库单
	/// </summary>
	[SugarTable("ProductStorage", TableDescription = "产品入库单")] // 设置表名和描述
	public class ProductStorage:AuditableEntity
	{
		/// <summary>
		/// 入库单号
		/// </summary>
		[SugarColumn(ColumnName = "InHouseNum", ColumnDescription = "入库单号")]
		public string InHouseNum { get; set; }

		/// <summary>
		/// 入库时间
		/// </summary>
		[SugarColumn(ColumnName = "InDate", ColumnDescription = "入库时间")]
		public DateTime InDate { get; set; }

		/// <summary>
		/// 工单号（外键）
		/// </summary>
		[SugarColumn(ColumnName = "Supplier", ColumnDescription = "工单号")]
		public int Supplier { get; set; }

		/// <summary>
		/// 入库状态（true: 已入库，false: 未入库）
		/// </summary>
		[SugarColumn(ColumnName = "Status", ColumnDescription = "入库状态")]
		public bool Status { get; set; } = false;

		/// <summary>
		/// 备注
		/// </summary>
		[SugarColumn(ColumnName = "Remark", IsNullable = true, ColumnDescription = "备注")]
		public string? Remark { get; set; }


	}

	/// <summary>
	/// 产品入库明细表
	/// </summary>
	[SugarTable("ProductStorageDetail", TableDescription = "产品入库明细表")]
	public class ProductStorageDetail:AuditableEntity
	{
		/// <summary>
		/// 产品编号
		/// </summary>
		[SugarColumn(ColumnName = "ProductId", ColumnDescription = "产品编号")]
		public int ProductId { get; set; }

		/// <summary>
		/// 所属入库单编号
		/// </summary>
        [SugarColumn(ColumnName = "ProductStorgeId", ColumnDescription = "所属入库单编号")]
        public int ProductStorgeId { get; set; }

		/// <summary>
		/// 数量
		/// </summary>
		[SugarColumn(ColumnName = "ProductSum", ColumnDescription = "数量")]
		public int ProductSum { get; set; }

		/// <summary>
		/// 生产日期
		/// </summary>
		[SugarColumn(ColumnName = "ProductionDate", ColumnDescription = "生产日期")]
		public DateTime ProductionDate { get; set; }

		/// <summary>
		/// 到期日期
		/// </summary>
		[SugarColumn(ColumnName = "ExpirationDate", ColumnDescription = "到期日期")]
		public DateTime ExpirationDate { get; set; }

		/// <summary>
		/// 仓库编号
		/// </summary>
		[SugarColumn(ColumnName = "WarehouseId", ColumnDescription = "仓库编号")]
		public int WarehouseId { get; set; }

	}
}
