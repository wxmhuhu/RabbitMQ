using MicroServices.Domain.Materials;
using MicroServices.Repository.IRepository.I_Product_Repository;
using MricoServices.Repository.Repository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Repository.Repository.Product_Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ISqlSugarClient db) : base(db)
        {
        }
    }
}
