using AutoMapper;
using MicroServices.Application.IService.StorgeServices;
using MicroServices.Domain.InStorage;
using MicroServices.Models.Dtos.StorgeDTOS;
using MicroServices.Repository.IRepository.IInStoreRepository;
using MicroServices.Repository.IRepository.IInventory;
using MicroServices.Repository.Repository.InStoreRepository;
using Microsoft.Extensions.Configuration;
using MricoServices.Shared.ApiResult;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Application.Services.StorgeService
{
	public class ProductStorgeDTo
	{
		/// <summary>
		/// 创建/修改产品入库单Dto
		/// </summary>
		public CreateUpdateProductStorgeDto createUpdateProductStorgeDto { get; set;}
		/// <summary>
		/// 创建/修改产品入库明细Dto
		/// </summary>
		public CreateUpdateProductStorgeDetailDto createUpdateProductStorgeDetailDto { get; set;}
	}
	/// <summary>
	/// 产品入库服务实现
	/// </summary>
	public class ProductStorgeService : IProductStorgeService
	{
		private readonly IConfiguration _configuration;
		private readonly IProductStorgeRepository _productStorgeRepository;
		private readonly IProductStorageDetailRepository _productStorageDetailRepository;
		private readonly IProductInventoryRepository _productInventoryRepository;
		private readonly IMapper mapper;
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="productStorgeRepository"></param>
		public ProductStorgeService(IProductStorgeRepository productStorgeRepository, IMapper mapper, IConfiguration configuration, IProductStorageDetailRepository productStorageDetailRepository, IProductInventoryRepository productInventoryRepository)
		{
			_productStorgeRepository = productStorgeRepository;
			this.mapper = mapper;
			_configuration = configuration;
			_productStorageDetailRepository = productStorageDetailRepository;
			_productInventoryRepository = productInventoryRepository;
		}

		/// <summary>
		/// 新增产品入库单与其对应的产品明细
		/// </summary>
		/// <returns></returns>
		public async Task<ApiResult> AddProductStorge(ProductStorgeDTo productStorgeDTo)
		{

			using (var db = new SqlSugarScope(new ConnectionConfig()
			{
				ConnectionString = _configuration.GetConnectionString("DefaultConnection"), // 从配置文件中获取连接字符串
				DbType = DbType.PostgreSQL, // 根据实际数据库类型设置
				IsAutoCloseConnection = true
			}))
			try
			{
				db.Ado.BeginTran();
					//新增入库单
				if (productStorgeDTo.createUpdateProductStorgeDto.InHouseNum == null || productStorgeDTo.createUpdateProductStorgeDto.InHouseNum == "string")
				{
					productStorgeDTo.createUpdateProductStorgeDto.InHouseNum = "RKBH" + DateTime.Now.ToString("yyyyMMddHHmmsshhhh");
					productStorgeDTo.createUpdateProductStorgeDto.Status = false;
				}
				var NewId = await _productStorgeRepository.AddAsync(mapper.Map<ProductStorage>(productStorgeDTo.createUpdateProductStorgeDto));
				var insertedRecord = await _productStorgeRepository.GetAll().Where(x => x.InHouseNum == productStorgeDTo.createUpdateProductStorgeDto.InHouseNum).FirstAsync();

				//新增产品入库明细
				if (NewId > 0)
				{
					productStorgeDTo.createUpdateProductStorgeDetailDto.ProductId = NewId;
					var result = await _productStorageDetailRepository.AddAsync(mapper.Map<ProductStorageDetail>(productStorgeDTo.createUpdateProductStorgeDetailDto));
				}
					return ApiResult.Success(ResultCode.Ok);
			}
			catch (Exception)
			{
				db.RollbackTran();
				throw;
			}
		}



		/// <summary>
		/// 删除产品入库单
		/// </summary>
		/// <param name="ProductId"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<ApiResult> DeleteProductStorgeAsync(int ProductId)
		{
			using (var db=new SqlSugarScope(new ConnectionConfig()
			{
				ConnectionString = _configuration.GetConnectionString("DefaultConnection"), // 从配置文件中获取连接字符串
				DbType = DbType.PostgreSQL, // 根据实际数据库类型设置
				IsAutoCloseConnection = true
			}))
			{
				try
				{
					var productOrder = await _productStorgeRepository.GetByIdAsync(ProductId);
					var result1=await _productStorgeRepository.DeleteAsync(productOrder.Id);
					var productDetial=await _productStorageDetailRepository.GetAll().Where(x => x.ProductId == ProductId).ToListAsync();
					var result2 = 0;
					foreach(var item in productDetial)
					{
						result2=await _productStorageDetailRepository.DeleteAsync(item.Id);
					}
					return result1 > 0 && result2 > 0 ? ApiResult.Success(ResultCode.Ok) : ApiResult.Fail(ResultCode.Fail, "删除产品入库单失败");
				}
				catch (Exception)
				{

					throw;
				}
			}
		}

		/// <summary>
		/// 获取产品入库单列表
		/// </summary>
		public async Task<ApiResult<ApiPaging<List<ProductStorgeDto>>>> GetProductStorgeAsync(ProductStorgeSearch productStorgeSearch)
		{
			try
			{
				var productStorgeList = _productStorgeRepository.GetAll();
				var tatolCount = productStorgeList.Count();
                var totalPage = (int)Math.Ceiling(productStorgeList.Count() * 1.0 / productStorgeSearch.PageSize);
				var page = productStorgeList.Skip((productStorgeSearch.PageIndex - 1) * productStorgeSearch.PageSize).Take(productStorgeSearch.PageSize).ToList();
				var data = mapper.Map<List<ProductStorgeDto>>(page);
				return ApiResult<ApiPaging<List<ProductStorgeDto>>>.Success(ResultCode.Ok, new ApiPaging<List<ProductStorgeDto>>
				{
					TotalCount = tatolCount,
					TotalPage = totalPage,
					Data = data
				});
			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// 获取产品入库明细列表
		/// </summary>
		/// <param name="productStorgeSearch"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public Task<ApiPaging<List<ProductStorgeDetailDto>>> GetProductStorgeDetailListAsync(ProductStorgeSearch productStorgeSearch)
		{
			throw new NotImplementedException();
		}


		/// <summary>
		/// 获取产品入库单明细列表
		/// </summary>
		/// <param name="productStorgeSearch"></param>
		/// <returns></returns>
		public async Task<ApiResult<ApiPaging<List<ProductStorgeDetailDto>>>> GetProductStorgeDetailAsync(ProductStorgeDetailSearch productStorgeSearch)
		{
			try
			{
				// 直接在数据库层面进行筛选和分页
				var page = await _productStorageDetailRepository.GetAll()
					.Where(x => x.ProductStorgeId == productStorgeSearch.ProductStorgeDetailId)
					.Skip((productStorgeSearch.PageIndex - 1) * productStorgeSearch.PageSize)
					.Take(productStorgeSearch.PageSize)
					.ToListAsync();

				var totalCount = page.Count();
				var totalPage = (int)Math.Ceiling(totalCount * 1.0 / productStorgeSearch.PageSize);

				var data = mapper.Map<List<ProductStorgeDetailDto>>(page);
				return ApiResult<ApiPaging<List<ProductStorgeDetailDto>>>.Success(ResultCode.Ok, new ApiPaging<List<ProductStorgeDetailDto>>
				{
					TotalCount = totalCount,
					TotalPage = totalPage,
					Data = data
				});
			}
			catch (Exception)
			{

				throw;
			}
		}

		/// <summary>
		/// 修改产品入库单
		/// </summary>
		/// <param name="ProductId"></param>
		/// <param name="productStorgeDto"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<ApiResult> UpdateProductStorgeAsync(int ProductId, ProductStorgeDTo productStorgeDTo)
		{
			using (var db = new SqlSugarScope(new ConnectionConfig()
			{
				ConnectionString = _configuration.GetConnectionString("DefaultConnection"), // 从配置文件中获取连接字符串
				DbType = DbType.PostgreSQL, // 根据实际数据库类型设置
				IsAutoCloseConnection = true
			}))
			try
			{
				var productOrder = await _productStorgeRepository.GetByIdAsync(ProductId);
				var result1 = await _productStorgeRepository.UpdateAsync(mapper.Map<ProductStorage>(productStorgeDTo.createUpdateProductStorgeDto));
				var result2 = await _productStorageDetailRepository.UpdateAsync(mapper.Map<ProductStorageDetail>(productStorgeDTo.createUpdateProductStorgeDetailDto));
				return result1 > 0 && result2 > 0 ? ApiResult.Success(ResultCode.Ok) : ApiResult.Fail(ResultCode.Fail, "修改产品入库单失败");
			}
			catch (Exception)
			{
				db.RollbackTran();
				throw;
			}

		}

		/// <summary>
		/// 产品入库
		/// </summary>
		/// <param name="ProductStorgeId"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<ApiResult> ProductIn(int ProductStorgeId)
		{
			try
			{
				var productStorge = await _productStorgeRepository.GetByIdAsync(ProductStorgeId);
				var productStorgeDetail =await _productStorageDetailRepository.GetAll().ToListAsync();
				foreach (var item in productStorgeDetail)
				{ 
					var productInventory=await _productInventoryRepository.GetAll().Where(d=>d.Id==item.ProductId).FirstAsync();
					productInventory.ProductSum+=item.ProductSum;
                    await _productInventoryRepository.UpdateAsync(productInventory);
				}
				return ApiResult.Success(ResultCode.Ok);
			}
			catch (Exception)
			{

				throw;
			}
		}
		/// <summary>
		/// 新增产品
		/// </summary>
		/// <param name="productInventoryDto"></param>
		/// <returns></returns>
		public async Task<ApiResult> AddProductInventory(ProductInventoryDto productInventoryDto)
		{
			try
			{
				productInventoryDto.ProductSum = 0;
				var productInventory = mapper.Map<ProductInventory>(productInventoryDto);
				var result= await _productInventoryRepository.AddAsync(productInventory);
                return result > 0 ? ApiResult.Success(ResultCode.Ok) : ApiResult.Fail(ResultCode.Fail, "添加产品失败");
			}
			catch (Exception)
			{

				throw;
			}
		}


		/// <summary>
		/// 产品出库
		/// </summary>
		/// <param name="ProductId"></param>
		/// <returns></returns>
		public async Task<ApiResult> ProductOut(int ProductId)
		{
			try
			{
				var productStorge = await _productStorgeRepository.GetByIdAsync(ProductId);
				var productStorgeDetail = await _productStorageDetailRepository.GetAll().ToListAsync();
				foreach (var item in productStorgeDetail)
				{
					var productInventory = await _productInventoryRepository.GetAll().Where(d => d.Id == item.ProductId).FirstAsync();
					productInventory.ProductSum -= item.ProductSum;
					await _productInventoryRepository.UpdateAsync(productInventory);
				}
				return ApiResult.Success(ResultCode.Ok);
			}
			catch (Exception)
			{

				throw;
			}
		}

		/// <summary>
		/// 获取产品库存列表
		/// </summary>
		/// <param name="productStorgeSearch"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<ApiResult<ApiPaging<List<ProductInventoryDto>>>> GetProductInventoryAsync(ProductStorgeSearch productStorgeSearch)
		{
			try
			{
				var	product= await _productInventoryRepository.GetAll().ToListAsync();
				var totalCount = product.Count();
                var totalPage = (int)Math.Ceiling(totalCount*1.0/ productStorgeSearch.PageSize);
                var pagedata = product.Skip((productStorgeSearch.PageIndex - 1) * productStorgeSearch.PageSize).Take(productStorgeSearch.PageSize).ToList();
                var data = mapper.Map<List<ProductInventoryDto>>(pagedata);
                var apiPagingData = new ApiPaging<List<ProductInventoryDto>>
                {
                    TotalCount = totalCount,
                    TotalPage = totalPage,
                    Data = data
				};
                return ApiResult<ApiPaging<List<ProductInventoryDto>>>.Success(ResultCode.Ok, apiPagingData);
			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}
