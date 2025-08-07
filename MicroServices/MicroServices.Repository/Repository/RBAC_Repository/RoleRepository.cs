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
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(ISqlSugarClient db) : base(db)
        {
            // BaseRepository 的构造函数已经设置了 base.Context = db;
            // 所以这里不需要额外的操作
        }
        // 这里可以添加角色相关的特定方法，例如获取角色列表、检查角色是否存在等
    }
}
