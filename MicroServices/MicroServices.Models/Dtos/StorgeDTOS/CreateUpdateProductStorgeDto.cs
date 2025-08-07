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
	/// 产品入库单Dto
	/// </summary>
	public class ProductStorgeDto:AuditableEntity
	{
		/// <summary>
		/// 入库单号
		/// </summary>
		public string InHouseNum { get; set; }

		/// <summary>
		/// 入库时间
		/// </summary>
		public DateTime InDate { get; set; }

		/// <summary>
		/// 工单号（外键）
		/// </summary>
		public int Supplier { get; set; }

		/// <summary>
		/// 入库状态（true: 已入库，false: 未入库）
		/// </summary>
		public bool Status { get; set; } = false;

		/// <summary>
		/// 备注
		/// </summary>
		public string? Remark { get; set; }
	}


	/// <summary>
	/// 创建/修改产品入库单Dto
	/// </summary>
	public class CreateUpdateProductStorgeDto
	{
		/// <summary>
		/// 主键
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// 入库单号
		/// </summary>
		public string InHouseNum { get; set; }

		/// <summary>
		/// 入库时间
		/// </summary>
		public DateTime InDate { get; set; }

		/// <summary>
		/// 工单号（外键）
		/// </summary>
		public int Supplier { get; set; }

		/// <summary>
		/// 入库状态（true: 已入库，false: 未入库）
		/// </summary>
		public bool Status { get; set; } = false;

		/// <summary>
		/// 备注
		/// </summary>
		public string? Remark { get; set; }
	}

	/// <summary>
	/// 查询产品入库单Dto
	/// </summary>
    public class ProductStorgeSearch: PageModel
    {
		/// <summary>
		/// 入库单编号
		/// </summary>
        public int ProductStorgeId { get; set;}
    }


	/// <summary>
	/// 产品入库明细Dto
	/// </summary>
    public class ProductStorgeDetailDto:AuditableEntity
    {
		/// <summary>
		/// 产品编号
		/// </summary>
		public int ProductId { get; set; }

		/// <summary>
		/// 数量
		/// </summary>
		public int ProductSum { get; set; }

		/// <summary>
		/// 生产日期
		/// </summary>
		public DateTime ProductionDate { get; set; }

		/// <summary>
		/// 到期日期
		/// </summary>
		public DateTime ExpirationDate { get; set; }

		/// <summary>
		/// 仓库编号
		/// </summary>
		public int WarehouseId { get; set; }
	}

	/// <summary>
	/// 创建/修改产品入库明细Dto
	/// </summary>
	public class CreateUpdateProductStorgeDetailDto
	{
		/// <summary>
		/// 入库单编号
		/// </summary>
		public int ProductId { get; set; }
		/// <summary>
		/// 入库单编号
		/// </summary>
		public int ProductStorgeId { get; set; }

		/// <summary>
		/// 数量
		/// </summary>
		public int ProductSum { get; set; }

		/// <summary>
		/// 生产日期
		/// </summary>
		public DateTime ProductionDate { get; set; }

		/// <summary>
		/// 到期日期
		/// </summary>
		public DateTime ExpirationDate { get; set; }

		/// <summary>
		/// 仓库编号
		/// </summary>
		public int WarehouseId { get; set; }
	}

	/// <summary>
	/// 查询产品入库明细Dto
	/// </summary>
    public class ProductStorgeDetailSearch: PageModel
    {
		/// <summary>
		/// 入库明细编号
		/// </summary>
        public int ProductStorgeDetailId { get; set;}
    }



}
