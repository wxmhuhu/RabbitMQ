using MicroServices.Domain.InStorage;
using MicroServices.Repository.IRepository.IInStoreRepository;
using MricoServices.Repository.IRepository;
using MricoServices.Repository.Repository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Repository.Repository.InStoreRepository
{
	/// <summary>
	/// 产品入库接口实现
	/// </summary>
	public class ProductStorgeRepository:BaseRepository<ProductStorage>, IProductStorgeRepository
	{
		public ProductStorgeRepository(ISqlSugarClient db) : base(db)
        {
        }
	}

	/// <summary>
	/// 产品入库明细接口实现
	/// </summary>
    public class ProductStorgeDetailRepository:BaseRepository<ProductStorageDetail>, IProductStorageDetailRepository
	{
        public ProductStorgeDetailRepository(ISqlSugarClient db) : base(db)
        {
        }
    }

	/// <summary>
	/// 物料入库接口实现
	/// </summary>
	public class MaterialStorgeRepository : BaseRepository<MaterialInventory>, IMaterialStorgeRepository
	{
		public MaterialStorgeRepository(ISqlSugarClient db) : base(db)
        {
        }
	}


	/// <summary>
	/// 物料入库明细接口实现
	/// </summary>
	public class MaterialStorgeDetailRepository : BaseRepository<PurchaseInventoryMaterial>
	{
		public MaterialStorgeDetailRepository(ISqlSugarClient db) : base(db)
        {
        }
	}


}
