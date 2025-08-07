using MricoServices.Domain.RBAC;
using MricoServices.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Repository.IRepository.I_RBAC_Repository
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
    }
}
