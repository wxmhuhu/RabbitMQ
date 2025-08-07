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
	/// 库位Dto
	/// </summary>
	public class HouseLocationDto:AuditableEntity
	{
		/// <summary>
		/// 库位编号
		/// </summary>
		public string LocationNum { get; set; }

		/// <summary>
		/// 库位名称
		/// </summary>
		public string LocationName { get; set; }

		/// <summary>
		/// 库位所属库区
		/// </summary>
		public int AreaId { get; set; }

		/// <summary>
		/// 所属库区名称
		/// </summary>
		public string AreaName { get; set; }

		/// <summary>
		/// 库位最大载重
		/// </summary>
		public int MaxLoad { get; set; }

		/// <summary>
		/// 库位位置X
		/// </summary>
		public int LocationAddrX { get; set; }

		/// <summary>
		/// 库位位置Y
		/// </summary>
		public int LocationAddrY { get; set; }

		/// <summary>
		/// 库位位置Z（字符串类型）
		/// </summary>
		public int LocationAddrZ { get; set; }

		/// <summary>
		/// 库位描述
		/// </summary>
		public string Remark { get; set; }
	}

	/// <summary>
	/// 库位创建或更新Dto
	/// </summary>
    public class CreateUpdateHouseLocationDto
    {

		/// <summary>
		/// 库位编号
		/// </summary>
		public string LocationNum { get; set; }

		/// <summary>
		/// 库位名称
		/// </summary>
		public string LocationName { get; set; }

		/// <summary>
		/// 库位所属库区
		/// </summary>
		public int AreaId { get; set; }

		/// <summary>
		/// 库位最大载重
		/// </summary>
		public int MaxLoad { get; set; }

		/// <summary>
		/// 库位位置X
		/// </summary>
		public int LocationAddrX { get; set; }

		/// <summary>
		/// 库位位置Y
		/// </summary>
		public int LocationAddrY { get; set; }

		/// <summary>
		/// 库位位置Z（字符串类型）
		/// </summary>
		public int LocationAddrZ { get; set; }

		/// <summary>
		/// 库位描述
		/// </summary>
		public string Remark { get; set; }
	}

	/// <summary>
	/// 查询库位Dto
	/// </summary>
    public class HouseLocationSearch : PageModel
    {
        public int HouseLocationId { get; set; }
    }
}
