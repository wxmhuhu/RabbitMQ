using MicroServices.Application.IService.StorgeServices;
using MicroServices.Application.Services.StorgeService;
using MicroServices.Models.Dtos.StorgeDTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MricoServices.Shared.ApiResult;

namespace MicroServices.API.Controllers.ProductStorge
{
	/// <summary>
	/// 出入库管理
	/// </summary>
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProductStorgeController : ControllerBase
	{
		private readonly IProductStorgeService _productStorgeService;

		public ProductStorgeController(IProductStorgeService productStorgeService)
		{
			_productStorgeService = productStorgeService;
		}

		/// <summary>
		/// 创建入库信息
		/// </summary>
		/// <param name="productStorgeDTo"></param>
		/// <returns></returns>
        [HttpPost]
		public async Task<ApiResult> AddProductStorge(ProductStorgeDTo productStorgeDTo)
		{
            return await _productStorgeService.AddProductStorge(productStorgeDTo);
		}

		/// <summary>
		/// 删除入库信息
		/// </summary>
		/// <param name="ProductId"></param>
		/// <returns></returns>
		[HttpDelete]
        public async Task<ApiResult> DeleteProductStorgeAsync(int ProductId)
		{
            return await _productStorgeService.DeleteProductStorgeAsync(ProductId);
		}

		/// <summary>
		/// 修改入库信息
		/// </summary>
		/// <param name="ProductId"></param>
		/// <param name="productStorgeDTo"></param>
		/// <returns></returns>
        [HttpPut]
        public async Task<ApiResult> UpdateProductStorgeAsync(int ProductId, ProductStorgeDTo productStorgeDTo)
		{
            return await _productStorgeService.UpdateProductStorgeAsync(ProductId, productStorgeDTo);
		}

		/// <summary>
		/// 获取入库单信息
		/// </summary>
		/// <param name="productStorgeSearch"></param>
		/// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ApiPaging<List<ProductStorgeDto>>>> GetProductStorgeListAsync([FromQuery]ProductStorgeSearch productStorgeSearch)
		{
            return await _productStorgeService.GetProductStorgeAsync(productStorgeSearch);
		}

		/// <summary>
		/// 获取入库明细信息
		/// </summary>
		/// <param name="productStorgeDetailSearch"></param>
		/// <returns></returns>
		[HttpGet]
		public async Task<ApiResult<ApiPaging<List<ProductStorgeDetailDto>>>> GetProductStorgeDetailAsync([FromQuery]ProductStorgeDetailSearch productStorgeDetailSearch)
		{
			return await _productStorgeService.GetProductStorgeDetailAsync(productStorgeDetailSearch);
		}

		/// <summary>
		/// 产品入库
		/// </summary>
		/// <param name="ProductId"></param>
		/// <returns></returns>
		[HttpPut]
        public async Task<ApiResult> ProductIn(int ProductId)
		{
			return await _productStorgeService.ProductIn(ProductId);
		}

		/// <summary>
		/// 新增产品
		/// </summary>
		/// <param name="productInventoryDto"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<ApiResult> AddProductInventory(ProductInventoryDto productInventoryDto)
		{
			return await _productStorgeService.AddProductInventory(productInventoryDto);
		}


		/// <summary>
		/// 获取产品库存列表
		/// </summary>
		/// <param name="productStorgeSearch"></param>
		/// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ApiPaging<List<ProductInventoryDto>>>> GetProductInventoryAsync([FromQuery]ProductStorgeSearch productStorgeSearch)
        {
            return await _productStorgeService.GetProductInventoryAsync(productStorgeSearch);
        }


	}
}
