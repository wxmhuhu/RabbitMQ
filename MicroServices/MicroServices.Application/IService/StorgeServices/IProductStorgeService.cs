using MicroServices.Application.Services.StorgeService;
using MicroServices.Models.Dtos.StorgeDTOS;
using MricoServices.Shared.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Application.IService.StorgeServices
{
	/// <summary>
	/// 产品入库服务接口
	/// </summary>
	public interface IProductStorgeService
	{
		/// <summary>
		/// 新增产品入库单与其对应的产品明细
		/// </summary>
		/// <param name="productStorgeDTo"></param>
		/// <returns></returns>
		Task<ApiResult> AddProductStorge(ProductStorgeDTo productStorgeDTo);



		/// <summary>
        /// 修改产品入库单
        /// </summary>
		Task<ApiResult> UpdateProductStorgeAsync(int ProductId, ProductStorgeDTo productStorgeDTo);

		/// <summary>
		/// 获取产品入库单
		/// </summary>
		/// <param name="productStorgeSearch"></param>
		/// <returns></returns>
        Task<ApiResult<ApiPaging<List<ProductStorgeDto>>>> GetProductStorgeAsync(ProductStorgeSearch productStorgeSearch);

		/// <summary>
		/// 删除产品入库单
		/// </summary>
		/// <param name="ProductId">入库单Id</param>
		/// <returns></returns>
		Task<ApiResult> DeleteProductStorgeAsync(int ProductId);

		/// <summary>
		/// 获取产品入库单列表
		/// </summary>
		/// <param name="productStorgeSearch"></param>
		/// <returns></returns>
		Task<ApiResult<ApiPaging<List<ProductStorgeDetailDto>>>> GetProductStorgeDetailAsync(ProductStorgeDetailSearch productStorgeSearch);


		/// <summary>
		/// 新增产品
		/// </summary>
		/// <param name="productInventoryDto"></param>
		/// <returns></returns>
		Task<ApiResult> AddProductInventory(ProductInventoryDto productInventoryDto);

		/// <summary>
		/// 产品入库
		/// </summary>
		/// <param name="ProductId"></param>
		/// <returns></returns>
		Task<ApiResult> ProductIn(int ProductId);

		/// <summary>
		/// 产品出库
		/// </summary>
		/// <param name="ProductId"></param>
		/// <returns></returns>
		Task<ApiResult> ProductOut(int ProductId);


		/// <summary>
		/// 获取产品库存列表
		/// </summary>
		Task<ApiResult<ApiPaging<List<ProductInventoryDto>>>> GetProductInventoryAsync(ProductStorgeSearch productStorgeSearch);

	}
}
