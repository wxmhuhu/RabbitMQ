using MicroServices.Repository.IRepository.I_RBAC_Repository;
using MricoServices.Domain.RBAC;
using MricoServices.Repository.Repository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Repository.Repository.RBAC_Repository
{
    public class MenuRepository : BaseRepository<Menu>, IMenuRepository
    {
        public MenuRepository(ISqlSugarClient db) : base(db)
        {
            // BaseRepository 的构造函数已经设置了 base.Context = db;
            // 所以这里不需要额外的操作
        }
    }
}
