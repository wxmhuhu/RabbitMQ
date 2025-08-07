using MicroServices.Domain.InStorage;
using MricoServices.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Repository.IRepository.IInStoreRepository
{
	/// <summary>
	/// 产品入库仓储接口
	/// </summary>
	public interface IProductStorgeRepository:IBaseRepository<ProductStorage>
	{
	}

	/// <summary>
	/// 产品入库明细仓储接口
	/// </summary>
    public interface IProductStorageDetailRepository : IBaseRepository<ProductStorageDetail>
	{
	}

	/// <summary>
	/// 物料入库仓储接口
	/// </summary>
    public interface IMaterialStorgeRepository:IBaseRepository<MaterialInventory>
    {
    }


	/// <summary>
	/// 物料入库明细仓储接口
	/// </summary>
	public interface IMaterialStorgeDetailRepository : IBaseRepository<PurchaseInventoryMaterial>
	{

	}


}
