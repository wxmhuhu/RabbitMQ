using Dm.filter;
using MicroServices.Application.IService.Products;
using MicroServices.Domain.Materials;
using MicroServices.Models.Dtos.Bom;
using MicroServices.Models.Dtos.ProductDtos;
using MicroServices.Repository.IRepository.I_Product_Repository;
using MricoServices.Shared.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Application.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        /// <summary>
        /// 获取所有产品（新增选择)
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<ApiPaging<List<ProductDto>>>> GetProductsAsync(Models.Dtos.ProductDtos.Search search)
        {
            try
            {
                var products = productRepository.GetAll()
                    .LeftJoin<Unite>((product, unite) => product.Unit == unite.Id)
                    .LeftJoin<TypeInfos>((product, unite, type) => product.ProductType == type.Id)
                    .LeftJoin<PropertyInfos>((product, unite, type, property) => product.ProductProperty == property.Id)
                    .Select((product, unite, type, property) => new ProductDto
                    {
                        Id = product.Id,
                        ProductName = product.ProductName,
                        ProductCode = product.ProductCode,
                        Specification = product.Specification,
                        UnitName = unite.UniteName,
                        ProductTypeName = type.TypeName,
                        ProductPropertyName = property.MaterialPropertyName
                    });
                var totalCount = await products.CountAsync();
                var totalPage = (int)Math.Ceiling(totalCount * 1.0 / search.PageSize); // 使用 totalCount 而非再次 count

                var pagedData = await products.OrderByDescending(product => product.Id) // 假设 Production_Planning 有 CreatedAt 属性
                                           .Skip((search.PageIndex - 1) * search.PageSize)
                                           .Take(search.PageSize)
                                           .ToListAsync();
                var apiPagingData = new ApiPaging<List<ProductDto>>
                {
                    TotalCount = totalCount,
                    TotalPage = totalPage,
                    Data = pagedData
                };
                return ApiResult<ApiPaging<List<ProductDto>>>.Success(ResultCode.Ok, apiPagingData);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
