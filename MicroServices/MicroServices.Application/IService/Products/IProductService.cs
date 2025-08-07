using MicroServices.Models.Dtos.ProductDtos;
using MricoServices.Shared.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Application.IService.Products
{
    public interface IProductService
    {
        /// <summary>
        /// 获取产品列表(新增选择)
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<ApiPaging<List<ProductDto>>>> GetProductsAsync(Search search);
    }
}
