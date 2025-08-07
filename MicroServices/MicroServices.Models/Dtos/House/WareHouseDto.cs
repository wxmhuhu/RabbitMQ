using MricoServices.Shared;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Models.Dtos.House
{
	/// <summary>
	/// 仓库Dto
	/// </summary>
	public class WareHouseDto:AuditableEntity
	{
		/// <summary>
		/// 仓库编号
		/// </summary>
		public string WareHouseNum { get; set; }

		/// <summary>
		/// 仓库名称
		/// </summary>
		public string WareHouseName { get; set; }

		/// <summary>
		/// 仓库地址
		/// </summary>
		public string Address { get; set; }

		/// <summary>
		/// 面积
		/// </summary>
		public decimal Area { get; set; }

		/// <summary>
		/// 仓库负责人
		/// </summary>
		public int WareHouseManager { get; set; }

		/// <summary>
		/// 备注
		/// </summary>
		public string? Remark { get; set; }
	}

	/// <summary>
	/// 仓库创建或更新Dto
	/// </summary>
    public class CreateUpdateWareHouseDto
    {
		/// <summary>
		/// 仓库编号
		/// </summary>
		public string WareHouseNum { get; set; }

		/// <summary>
		/// 仓库名称
		/// </summary>
		public string WareHouseName { get; set; }

		/// <summary>
		/// 仓库地址
		/// </summary>
		public string Address { get; set; }

		/// <summary>
		/// 面积
		/// </summary>
		public decimal Area { get; set; }

		/// <summary>
		/// 仓库负责人
		/// </summary>
		public int WareHouseManager { get; set; }

		/// <summary>
		/// 备注
		/// </summary>
		public string? Remark { get; set; }
	}

	/// <summary>
	/// 仓库搜索Dto
	/// </summary>
	public class WareHouseSearch : PageModel
	{
		public int WareHouseId { get; set; }

	}
}
