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
	/// 库区Dto
	/// </summary>
	public class WareHouseAreaDto:AuditableEntity
	{
		/// <summary>
		/// 库区编号
		/// </summary>
		public string AreaNum { get; set; }

		/// <summary>
		/// 库区名称
		/// </summary>
		public string AreaName { get; set; }

		/// <summary>
		/// 库区面积
		/// </summary>
		public decimal Area { get; set; }

		/// <summary>
		/// 所属仓库（外键）
		/// </summary>
		public int WareHouseId { get; set; }

		/// <summary>
		/// 所属仓库名称
		/// </summary>
        public string WareHouseName { get; set; }

		/// <summary>
		/// 负责人
		/// </summary>
		public int WareHouseManager { get; set; }
	}

	/// <summary>
	/// 库区创建或更新Dto
	/// </summary>
    public class CreateUpdateWareHouseAreaDto
    {
		/// <summary>
		/// 库区编号
		/// </summary>
		public string AreaNum { get; set; }

		/// <summary>
		/// 库区名称
		/// </summary>
		public string AreaName { get; set; }

		/// <summary>
		/// 库区面积
		/// </summary>
		public decimal Area { get; set; }

		/// <summary>
		/// 所属仓库（外键）
		/// </summary>
		public int WareHouseId { get; set; }

		/// <summary>
		/// 负责人
		/// </summary>
		public int WareHouseManager { get; set; }
	}
	//库区搜索Dto
    public class WareHouseAreaSearch : PageModel
    {
		public int WareHouseAreaId { get; set; }
	}
}
