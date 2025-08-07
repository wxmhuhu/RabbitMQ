using MicroServices.Domain.InStorage;
using MicroServices.Repository.IRepository.IInventory;
using MricoServices.Repository.Repository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Repository.Repository.Inventorys
{
	/// <summary>
	/// 物料库存仓储实现类
	/// </summary>
	public class MaterialInventoryRepository:BaseRepository<MaterialInventory>,IMaterialInventoryRepository
	{
		public MaterialInventoryRepository(ISqlSugarClient db) : base(db)
		{
		}
	}

	/// <summary>
	/// 产品库存仓储实现类
	/// </summary>
	public class ProductInventoryRepository:BaseRepository<ProductInventory>,IProductInventoryRepository
	{
		public ProductInventoryRepository(ISqlSugarClient db) : base(db)
		{
		}
	}
}
