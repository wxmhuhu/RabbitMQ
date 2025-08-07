using MicroServices.Domain.Equipments;
using MricoServices.Domain.RBAC;
using MricoServices.Shared;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Domain.FacatoryFloors
{
	/// <summary>
	/// 车间表
	/// </summary>
	[SugarTable("FactoryFloor", TableDescription = "车间表")]
	public class FactoryFloor: AuditableEntity
	{
		/// <summary>
		/// 车间名称
		/// </summary>
		[SugarColumn(ColumnName = "FactoryName", Length = 255, ColumnDescription = "车间名称")]
		public string FactoryName { get; set; }
		/// <summary>
		/// 车间主任
		/// </summary>
		[SugarColumn(ColumnName = "FactoryManager", ColumnDescription = "车间主任")]
		public int FactoryManager { get; set; }

	}



	/// <summary>
	/// 班组表
	/// </summary>
	[SugarTable("Team", TableDescription = "班组表")]
	public class Team: AuditableEntity
	{
		/// <summary>
		/// 班组编号
		/// </summary>
		[SugarColumn(ColumnName = "TeamNum", Length = 255, ColumnDescription = "班组编号")]
		public string TeamNum { get; set; }
		/// <summary>
		/// 班组名称
		/// </summary>
		[SugarColumn(ColumnName = "TeamName", Length = 255, ColumnDescription = "班组名称")]
		public string TeamName { get; set; }
		/// <summary>
		/// 所属车间
		/// </summary>
		[SugarColumn(ColumnName = "FactoryId", ColumnDescription = "所属车间")]
		public int FactoryId { get; set; }
		/// <summary>
		/// 班组类型
		/// </summary>
		[SugarColumn(ColumnName = "TeamTypeId", ColumnDescription = "班组类型")]
		public int TeamTypeId { get; set; }
		/// <summary>
		/// 负责人
		/// </summary>
		[SugarColumn(ColumnName = "Director", ColumnDescription = "负责人")]
		public int Director { get; set; }
		/// <summary>
		/// 班组状态
		/// </summary>
		[SugarColumn(ColumnName = "TeamStatus", IsNullable = true, ColumnDescription = "班组状态")]
		public int? TeamStatus { get; set; }

	}
	/// <summary>
	/// 班组类型表
	/// </summary>
	[SugarTable("TeamType", TableDescription = "班组类型表")]
	public class TeamType: AuditableEntity
	{
        /// <summary>
        /// 班组类型名称
        /// </summary>
        [SugarColumn(ColumnName = "TeamTypeName", Length = 255, ColumnDescription = "班组类型名称")]
		public string TeamTypeName { get; set; }
        /// <summary>
        /// 班组类型编号
        /// </summary>

        [SugarColumn(ColumnName = "TeamTypeNum", Length = 255, ColumnDescription = "班组类型编号")]
		public string TeamTypeNum { get; set; }
        /// <summary>
        /// 状态
        /// </summary>

        [SugarColumn(ColumnName = "TeamStatus", ColumnDescription = "状态")]
		public int TeamStatus { get; set; }
        /// <summary>
        /// 备注
        /// </summary>

        [SugarColumn(ColumnName = "Remark", Length = 255, IsNullable = true, ColumnDescription = "备注")]
		public string Remark { get; set; }
	}


	/// <summary>
	/// 班组成员表
	/// </summary>
	[SugarTable("TeamUser", TableDescription = "班组成员表")]
	public class TeamUser:AuditableEntity
	{    
		[SugarColumn(ColumnName = "TeamId", ColumnDescription = "班组编号")]
		public int TeamId { get; set; }
		[SugarColumn(ColumnName = "UserId", ColumnDescription = "用户编号")]
		public int UserId { get; set; }

	}
    /// <summary>
    /// 部门名称
    /// </summary>
    [SugarTable("DeptInfo", TableDescription = "部门表")]
    public class DeptInfo: AuditableEntity
    {
        [SugarColumn(ColumnName = "DeptName", ColumnDescription = "部门名称")]
        public string DeptName {  get; set; }
        [SugarColumn(ColumnName = "ParentId", ColumnDescription = "父级id")]
        public int ParentId { get; set; }

    }
}
