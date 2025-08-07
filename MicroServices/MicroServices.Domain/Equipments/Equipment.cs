using MricoServices.Shared;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Domain.Equipments
{
	/// <summary>
	/// 设备表
	/// </summary>
	[SugarTable("Equipment", TableDescription = "设备表")]
	public class Equipment:AuditableEntity
	{
		/// <summary>
		/// 设备名称
		/// </summary>
		[SugarColumn(ColumnName = "EquipmentName", Length = 255, ColumnDescription = "设备名称")]
		public string EquipmentName { get; set; }
		/// <summary>
		/// 设备类型
		/// </summary>
		[SugarColumn(ColumnName = "EquipmentTypeId", ColumnDescription = "设备类型")]
		public int EquipmentTypeId { get; set; }
		/// <summary>
		/// 设备编号
		/// </summary>
		[SugarColumn(ColumnName = "EquipmentNum", Length = 255, ColumnDescription = "设备编号")]
		public string EquipmentNum { get; set; }
		/// <summary>
		/// 设备型号
		/// </summary>
		[SugarColumn(ColumnName = "Specification", Length = 255, ColumnDescription = "设备型号")]
		public string Specification { get; set; }
		/// <summary>
		/// 设备品牌
		/// </summary>
		[SugarColumn(ColumnName = "Brank", Length = 255, ColumnDescription = "品牌")]
		public string Brank { get; set; }
		/// <summary>
		/// 所属车间
		/// </summary>
		[SugarColumn(ColumnName = "FactoryFloorId", ColumnDescription = "所属车间")]
		public int FactoryFloorId { get; set; }
		/// <summary>
		/// 设备状态
		/// </summary>
		[SugarColumn(ColumnName = "EquipmentStatus", ColumnDescription = "设备状态")]
		public int EquipmentStatus { get; set; }
		/// <summary>
		/// 设备备注
		/// </summary>
		[SugarColumn(ColumnName = "Remark", Length = 255, IsNullable = true, ColumnDescription = "备注")]
		public string Remark { get; set; }

	}



	/// <summary>
	/// 设备类型表
	/// </summary>
	[SugarTable("EquipmentType", TableDescription = "设备类型表")]
	public class EquipmentType:AuditableEntity
	{
		/// <summary>
		/// 设备类型名称
		/// </summary>
		[SugarColumn(ColumnName = "EquipmentTypeName", Length = 255, ColumnDescription = "类型名称")]
		public string EquipmentTypeName { get; set; }
		/// <summary>
		/// 设备类型父级编号
		/// </summary>
		[SugarColumn(ColumnName = "ParentId", IsNullable = true, ColumnDescription = "上级设备类型编号")]
		public int? ParentId { get; set; }
		/// <summary>
		/// 设备类型备注
		/// </summary>
		[SugarColumn(ColumnName = "Remark", Length = 255, IsNullable = true, ColumnDescription = "备注")]
		public string Remark { get; set; }
	}

}
