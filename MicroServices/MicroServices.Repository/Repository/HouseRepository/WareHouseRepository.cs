using MicroServices.Domain.InStorage;
using MicroServices.Repository.IRepository.IHouseRepository;
using MricoServices.Repository.Repository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Repository.Repository.HouseRepository
{
	/// <summary>
	/// 仓库仓储实现类
	/// </summary>
	public class WareHouseRepository : BaseRepository<WareHouse>, IWareHouseRepository
	{
		public WareHouseRepository(ISqlSugarClient db) : base(db)
        {
        }
	}
}
