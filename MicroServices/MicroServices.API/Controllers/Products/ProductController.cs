using MicroServices.Application.IService.Products;
using MicroServices.Domain.Materials;
using MicroServices.Models.Dtos.ProductDtos;
using MicroServices.Repository.IRepository.I_Product_Repository;
using MicroServices.Repository.Repository.Product_Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MricoServices.Shared.ApiResult;

namespace MicroServices.API.Controllers.Product
{
    /// <summary>
    /// 产品管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        /// <summary>
        /// 获取所有产品（新增选择)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ApiPaging<List<ProductDto>>>> GetProductsAsync([FromQuery]Search search)
        {
            try
            {
                return await productService.GetProductsAsync(search);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
