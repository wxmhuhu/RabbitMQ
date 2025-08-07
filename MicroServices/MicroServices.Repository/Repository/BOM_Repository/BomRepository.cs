using MicroServices.Domain.Bom;
using MicroServices.Repository.IRepository.I_BOM_Repository;
using MricoServices.Repository.Repository;
using SqlSugar;

namespace MicroServices.Repository.Repository.BOM_Repository
{
    public class BomRepository : BaseRepository<BOM>, IBomRepository
    {
        public BomRepository(ISqlSugarClient db) : base(db)
        {
        }
    }
}
