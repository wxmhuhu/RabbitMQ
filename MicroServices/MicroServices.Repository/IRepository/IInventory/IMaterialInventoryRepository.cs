using MicroServices.Domain.InStorage;
using MricoServices.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Repository.IRepository.IInventory
{
	/// <summary>
	/// 物料库存仓储接口
	/// </summary>
	public interface IMaterialInventoryRepository:IBaseRepository<MaterialInventory>
	{
	}

	/// <summary>
	/// 产品库存仓储接口
	/// </summary>
	public interface IProductInventoryRepository : IBaseRepository<ProductInventory>
	{
	}
}
