using MicroServices.Domain.Reportworks;
using MicroServices.Repository.IRepository.Reportworks;
using MricoServices.Repository.Repository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Repository.Repository.Reportworks
{
    public class ReportworkRecordRepository : BaseRepository<ReportworkRecord>, IReportworkRecordRepository
    {
        public ReportworkRecordRepository(ISqlSugarClient db) : base(db)
        {
        }
    }
}
