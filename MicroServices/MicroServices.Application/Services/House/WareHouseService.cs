using AutoMapper;
using MicroServices.Application.IService.Houses;
using MicroServices.Domain.InStorage;
using MicroServices.Models.Dtos.House;
using MicroServices.Repository.IRepository.IHouseRepository;
using MricoServices.Shared.ApiResult;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Application.Services.House
{
	/// <summary>
	/// 仓库服务实现
	/// </summary>
	public class WareHouseService : IWareHouseService
	{
		private readonly IWareHouseRepository _wareHouseRepository;
		private readonly IMapper mapper;

		public WareHouseService(IWareHouseRepository wareHouseRepository, IMapper mapper)
		{
			_wareHouseRepository = wareHouseRepository;
			this.mapper = mapper;
		}

		/// <summary>
		/// 添加仓库
		/// </summary>
		/// <param name="wareHouseDto"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<ApiResult> AddWareHouseAsync(CreateUpdateWareHouseDto wareHouseDto)
		{
			try
			{
				var house = await _wareHouseRepository.GetAll().Where(x=>x.WareHouseName== wareHouseDto.WareHouseName).AnyAsync();
				if (house)
				{
                    return ApiResult.Fail(ResultCode.Fail, "该仓库已存在");
				}
				if(wareHouseDto.WareHouseNum == null|| wareHouseDto.WareHouseNum == "string")
				{
					wareHouseDto.WareHouseNum="CKBH"+DateTime.Now.ToString("yyyyMMddHHmmssffff");
				}
                var result=await _wareHouseRepository.AddAsync(mapper.Map<WareHouse>(wareHouseDto));
				return result > 0 ? ApiResult.Success(ResultCode.Ok) : ApiResult.Fail(ResultCode.Fail, "添加仓库失败");
			}
			catch (Exception)
			{

				throw;
			}
		}

		/// <summary>
		/// 删除仓库
		/// </summary>
		/// <param name="wareHouseId"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<ApiResult> DeleteWareHouseAsync(int wareHouseId)
		{
			try
			{
				var warehouse = _wareHouseRepository.GetByIdAsync(wareHouseId);
				if (warehouse != null)
				{
					await _wareHouseRepository.SoftDeleteAsync(wareHouseId);
                    return ApiResult.Success(ResultCode.Ok);
				}
				else
				{
                    return ApiResult.Fail(ResultCode.Fail, "删除失败");
				}
				
			}
			catch (Exception)
			{

				throw;
			}
		}

		/// <summary>
		/// 获取仓库列表
		/// </summary>
		/// <param name="wareHouseSearch"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<ApiResult<ApiPaging<List<WareHouseDto>>>> GetWareHouseListAsync(WareHouseSearch wareHouseSearch)
		{
			try
			{
				var warehouse =  _wareHouseRepository.GetAll();
				if (wareHouseSearch.WareHouseId != 0)
				{
                    warehouse = warehouse.Where(d => d.Id == wareHouseSearch.WareHouseId);
				}
				var totalCount = await warehouse.CountAsync();
				var totalPage = (int)Math.Ceiling(await warehouse.CountAsync() * 1.0 / wareHouseSearch.PageSize);
				var page = warehouse.Skip((wareHouseSearch.PageIndex - 1) * wareHouseSearch.PageSize).Take(wareHouseSearch.PageSize).ToList();
				var data = mapper.Map<List<WareHouseDto>>(page);

				var apiPagingData = new ApiPaging<List<WareHouseDto>>
				{
					TotalCount = totalCount,
					TotalPage = totalPage,
					Data = data
				};
				return ApiResult<ApiPaging<List<WareHouseDto>>>.Success(ResultCode.Ok, apiPagingData);

			}
			catch (Exception)
			{

				throw;
			}
		}

		/// <summary>
		/// 更新仓库信息
		/// </summary>
		/// <param name="wareHouseDto"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<ApiResult<WareHouseDto>> UpdateWareHouseAsync(int wareHouseId, CreateUpdateWareHouseDto wareHouseDto)
		{
			try
			{
				var house=await _wareHouseRepository.GetByIdAsync(wareHouseId);
				if (house ==null)
				{
					return ApiResult<WareHouseDto>.Fail(ResultCode.Fail, "该仓库不存在");
				}
				var result=mapper.Map(wareHouseDto, house);

				var isWin=await _wareHouseRepository.UpdateAsync(result);

				return isWin > 0 ? ApiResult<WareHouseDto>.Success(ResultCode.Ok, mapper.Map<WareHouseDto>(result)) : ApiResult<WareHouseDto>.Fail(ResultCode.Fail, "更新仓库失败");
			}
			catch (Exception)
			{

				throw;
			}
		}


		/// <summary>
		/// 获取仓库信息
		/// </summary>
		/// <param name="wareHouseId"></param>
		/// <returns></returns>
		public async Task<ApiResult<List<WareHouseDto>>> GetAllWareHouseAsync()
		{
			try
			{
				var house = _wareHouseRepository.GetAll().ToList();
				var	 result= mapper.Map<List<WareHouseDto>>(house);
				return ApiResult<List<WareHouseDto>>.Success(ResultCode.Ok, result);
			}
			catch (Exception)
			{

				throw;
			}
		}

	}
}
