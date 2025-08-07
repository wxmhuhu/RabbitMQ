using MicroServices.Domain.ProcessInfo;
using MicroServices.Repository.IRepository.I_Process_Repository;
using MricoServices.Repository.Repository;
using SqlSugar;

namespace MicroServices.Repository.Repository.Process_Repository
{
    public class ProcessCompositionRepository : BaseRepository<ProcessComposition>, IProcessCompositionRepository
    {
        public ProcessCompositionRepository(ISqlSugarClient db) : base(db)
        {
        }
    }
}
