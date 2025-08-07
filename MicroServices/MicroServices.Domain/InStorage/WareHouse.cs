using MricoServices.Shared;
using SqlSugar;

namespace MicroServices.Domain.InStorage
{
	/// <summary>
	/// 仓库
	/// </summary>
	[SugarTable(TableName = "WareHouse", TableDescription = "仓库")]
	public class WareHouse:AuditableEntity
	{
		/// <summary>
		/// 仓库编号
		/// </summary>
		[SugarColumn(ColumnName = "WareHouseNum",ColumnDescription = "仓库编号")]
		public string WareHouseNum { get; set; }

		/// <summary>
		/// 仓库名称
		/// </summary>
		[SugarColumn(ColumnName = "WareHouseName", ColumnDescription = "仓库名称")]
		public string WareHouseName { get; set; }

		/// <summary>
		/// 仓库地址
		/// </summary>
		[SugarColumn(ColumnName = "Address", ColumnDescription = "仓库地址")]
		public string Address { get; set; }

		/// <summary>
		/// 面积
		/// </summary>
		[SugarColumn(ColumnName = "Area", ColumnDescription = "面积")]
		public decimal Area { get; set; }

		/// <summary>
		/// 仓库负责人
		/// </summary>
		[SugarColumn(ColumnName = "WareHouseManager", ColumnDescription = "仓库负责人")]
		public int WareHouseManager { get; set; }

		/// <summary>
		/// 备注
		/// </summary>
		[SugarColumn(ColumnName = "Remark", IsNullable = true, ColumnDescription = "备注")]
		public string? Remark { get; set; }
	}

	/// <summary>
	/// 库区信息表
	/// </summary>
	[SugarTable(TableName = "WareHouseArea", TableDescription = "库区信息表")]
    public class WareHouseArea:AuditableEntity
    {
		/// <summary>
		/// 库区编号
		/// </summary>
		[SugarColumn(ColumnName = "AreaNum",ColumnDescription = "库区编号")]
		public string AreaNum { get; set; }

		/// <summary>
		/// 库区名称
		/// </summary>
		[SugarColumn(ColumnName = "AreaName", ColumnDescription = "库区名称")]
		public string AreaName { get; set; }

		/// <summary>
		/// 库区面积
		/// </summary>
		[SugarColumn(ColumnName = "Area", ColumnDescription = "库区面积")]
		public decimal Area { get; set; }

		/// <summary>
		/// 所属仓库（外键）
		/// </summary>
		[SugarColumn(ColumnName = "WareHouseId", ColumnDescription = "所属仓库")]
		public int WareHouseId { get; set; }

		/// <summary>
		/// 负责人
		/// </summary>
		[SugarColumn(ColumnName = "WareHouseManager", ColumnDescription = "负责人")]
		public int WareHouseManager { get; set; }
	}

	/// <summary>
	/// 库位信息表
	/// </summary>
	[SugarTable(TableName = "WareHouseLocation",TableDescription = "库位信息表")]
    public class WareHouseLocation:AuditableEntity
    {
		/// <summary>
		/// 库位编号
		/// </summary>
		[SugarColumn(ColumnName = "LocationNum",ColumnDescription = "库位编号")]
		public string LocationNum { get; set; }

		/// <summary>
		/// 库位名称
		/// </summary>
		[SugarColumn(ColumnName = "LocationName", ColumnDescription = "库位名称")]
		public string LocationName { get; set; }

		/// <summary>
		/// 库位所属库区
		/// </summary>
		[SugarColumn(ColumnName = "AreaId", ColumnDescription = "库位所属库区")]
		public int AreaId { get; set; }

		/// <summary>
		/// 库位最大载重
		/// </summary>
		[SugarColumn(ColumnName = "MaxLoad", ColumnDescription = "库位最大载重")]
		public int MaxLoad { get; set; }

		/// <summary>
		/// 库位位置X
		/// </summary>
		[SugarColumn(ColumnName = "LocationAddrX", ColumnDescription = "库位位置X")]
		public int LocationAddrX { get; set; }

		/// <summary>
		/// 库位位置Y
		/// </summary>
		[SugarColumn(ColumnName = "LocationAddrY", ColumnDescription = "库位位置Y")]
		public int LocationAddrY { get; set; }

		/// <summary>
		/// 库位位置Z（字符串类型）
		/// </summary>
		[SugarColumn(ColumnName = "LocationAddrZ", ColumnDescription = "库位位置Z")]
		public int LocationAddrZ { get; set; }

		/// <summary>
		/// 库位描述
		/// </summary>
		[SugarColumn(ColumnName = "Remark", ColumnDescription = "库位描述")]
		public string Remark { get; set; }
	}
}
