using MicroServices.Domain.Materials;
using MicroServices.Repository.IRepository.I_Material_Repository;
using MricoServices.Repository.Repository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Repository.Repository.Material_Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ISqlSugarClient db) : base(db)
        {
        }
    }
}
