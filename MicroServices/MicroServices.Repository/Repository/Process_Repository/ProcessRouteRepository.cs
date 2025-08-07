using MicroServices.Domain.ProcessInfo;
using MicroServices.Repository.IRepository.I_Process_Repository;
using MricoServices.Repository.Repository;
using SqlSugar;

namespace MicroServices.Repository.Repository.Process_Repository
{
    public class ProcessRouteRepository : BaseRepository<ProcessRoute>, IProcessRouteRepository
    {
        public ProcessRouteRepository(ISqlSugarClient db) : base(db)
        {
        }
    }
}
