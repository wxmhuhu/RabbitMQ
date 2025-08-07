using MricoServices.Shared;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Domain.InStorage
{
	/// <summary>
	/// 物料库存表
	/// </summary>
	[SugarTable("MaterialInventory", TableDescription = "库存表")]
	public class MaterialInventory:AuditableEntity
	{
		/// <summary>
		/// 物料编号
		/// </summary>
		[SugarColumn(ColumnName = "MaterialCode", ColumnDescription = "物料编号")]
		public string MaterialCode { get; set; }
		/// <summary>
		/// 物料名称
		/// </summary>
		[SugarColumn(ColumnName = "MaterialName", ColumnDescription = "物料名称")]
        public string MaterialName { get; set; }
		/// <summary>
		/// 物料数量
		/// </summary>
		[SugarColumn(ColumnName = "MaterialSum", ColumnDescription = "物料数量")]
        public int MaterialSum { get; set; }
		/// <summary>
		/// 物料单位
		/// </summary>
		[SugarColumn(ColumnName = "MaterialUnit", ColumnDescription = "物料单位")]
        public string MaterialUnit { get; set; }
		/// <summary>
		/// 供货商
		/// </summary>
		[SugarColumn(ColumnName = "Supplier", ColumnDescription = "供货商")]
		public string Supplier { get; set; }
		/// <summary>
		/// 仓库编号
		/// </summary>
		[SugarColumn(ColumnName = "WarehouseId", ColumnDescription = "仓库编号")]
        public int WarehouseId { get; set; }

	}


	/// <summary>
	/// 产品库存表
	/// </summary>
	public class ProductInventory:AuditableEntity
	{
		/// <summary>
		/// 产品编号
		/// </summary>
		[SugarColumn(ColumnName = "ProductNum", ColumnDescription = "产品编号")]
		public string ProductNum { get; set; }

		/// <summary>
		/// 产品名称
		/// </summary>
		[SugarColumn(ColumnName = "ProductName", ColumnDescription = "产品名称")]
		public string ProductName { get; set; }

		/// <summary>
		/// 产品型号
		/// </summary>
        [SugarColumn(ColumnName = "ProductModel", ColumnDescription = "产品型号")]
        public string ProductModel { get; set; }

		/// <summary>
		/// 产品单位
		/// </summary>
		[SugarColumn(ColumnName = "ProductUnit", ColumnDescription = "产品单位")]
		public string ProductUnit { get; set; }

		/// <summary>
		/// BOM单
		/// </summary>
		[SugarColumn(ColumnName = "BOM", ColumnDescription = "BOM单")]
        public string BOM { get; set; }

		/// <summary>
		/// 产品类型
		/// </summary>
        [SugarColumn(ColumnName = "ProductType", ColumnDescription = "产品类型")]
        public string ProductType { get; set; }

		/// <summary>
		/// 当前库存
		/// </summary>
		[SugarColumn(ColumnName = "ProductSum", ColumnDescription = "产品数量")]
		public int ProductSum { get; set; }
		/// <summary>
		/// 仓库编号
		/// </summary>
		[SugarColumn(ColumnName = "WarehouseId", ColumnDescription = "仓库编号")]
		public int WarehouseId { get; set; }

		/// <summary>
		/// 库区编号
		/// </summary>
        [SugarColumn(ColumnName = "AreaId", ColumnDescription = "库区编号")]
        public int AreaId { get; set; }

		/// <summary>
		/// 库位编号
		/// </summary>
        [SugarColumn(ColumnName = "LocationId", IsNullable =true, ColumnDescription = "库位编号")]
        public int? LocationId { get; set; }
	}
}
