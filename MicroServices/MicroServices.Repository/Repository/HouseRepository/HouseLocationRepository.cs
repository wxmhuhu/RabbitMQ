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
	public class HouseLocationRepository : BaseRepository<WareHouseLocation>, IHouseLocationRepository
	{
		public HouseLocationRepository(ISqlSugarClient db) : base(db)
		{
		}
	}
}
