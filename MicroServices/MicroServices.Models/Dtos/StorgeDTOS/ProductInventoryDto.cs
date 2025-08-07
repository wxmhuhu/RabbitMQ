using MricoServices.Shared;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Models.Dtos.StorgeDTOS
{
	/// <summary>
	/// 产品库存Dto
	/// </summary>
	public class ProductInventoryDto:AuditableEntity
	{
		/// <summary>
		/// 产品编号
		/// </summary>
		public string ProductNum { get; set; }

		/// <summary>
		/// 产品名称
		/// </summary>
		public string ProductName { get; set; }

		/// <summary>
		/// 产品型号
		/// </summary>
		public string ProductModel { get; set; }

		/// <summary>
		/// 产品单位
		/// </summary>
		public string ProductUnit { get; set; }

		/// <summary>
		/// BOM单
		/// </summary>
		public string BOM { get; set; }

		/// <summary>
		/// 产品类型
		/// </summary>
		public string ProductType { get; set; }

		/// <summary>
		/// 当前库存
		/// </summary>
		public int ProductSum { get; set; }
		/// <summary>
		/// 仓库编号
		/// </summary>
		public int WarehouseId { get; set; }

		/// <summary>
		/// 库区编号
		/// </summary>
		public int AreaId { get; set; }

		/// <summary>
		/// 库位编号
		/// </summary>
		public int? LocationId { get; set; }
	}

	/// <summary>
	/// 创建/修改产品库存Dto
	/// </summary>
    public class CreateUpdateProductInventoryDto
    {
		/// <summary>
		/// 产品编号
		/// </summary>
		public string ProductNum { get; set; }

		/// <summary>
		/// 产品名称
		/// </summary>
		public string ProductName { get; set; }

		/// <summary>
		/// 产品型号
		/// </summary>
		public string ProductModel { get; set; }

		/// <summary>
		/// 产品单位
		/// </summary>
		public string ProductUnit { get; set; }

		/// <summary>
		/// BOM单
		/// </summary>
		public string BOM { get; set; }

		/// <summary>
		/// 产品类型
		/// </summary>
		public string ProductType { get; set; }

		/// <summary>
		/// 当前库存
		/// </summary>
		public int ProductSum { get; set; }
		/// <summary>
		/// 仓库编号
		/// </summary>
		public int WarehouseId { get; set; }

		/// <summary>
		/// 库区编号
		/// </summary>
		public int AreaId { get; set; }

		/// <summary>
		/// 库位编号
		/// </summary>
		public int? LocationId { get; set; }
	}
	/// <summary>
	/// 查询产品库存Dto
	/// </summary>
    public class ProductInventorySearch: PageModel
    {
		public int ProductInventId { get; set; }
	}
}
