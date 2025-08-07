using AutoMapper;
using MicroServices.Application.IService.Houses;
using MicroServices.Domain.InStorage;
using MicroServices.Models.Dtos.House;
using MicroServices.Repository.IRepository.IHouseRepository;
using MricoServices.Shared.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Application.Services.House
{
	/// <summary>
	/// 库区服务实现
	/// </summary>
	public class HouseAreaService : IHouseAreaService
	{
		private readonly IHouseAreaRepository houseAreaRepository;
		private readonly IMapper mapper;


		public HouseAreaService(IHouseAreaRepository wareHouseRepository, IMapper mapper)
		{
			houseAreaRepository = wareHouseRepository;
			this.mapper = mapper;
		}

		/// <summary>
		/// 添加库区
		/// </summary>
		/// <param name="houseAreaDto"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<ApiResult> AddHouseAreaAsync(CreateUpdateWareHouseAreaDto houseAreaDto)
		{
			try
			{
				var area =await houseAreaRepository.GetAll().Where(d=>d.AreaName==houseAreaDto.AreaName&& d.WareHouseId== houseAreaDto.WareHouseId).AnyAsync();
				if (area)
				{
					return ApiResult.Fail(ResultCode.Fail, "该库区已存在");
				}
				if (houseAreaDto.AreaNum == null||houseAreaDto.AreaNum=="string")
				{
					houseAreaDto.AreaNum="KQBH"+DateTime.Now.ToString("yyyyMMddHHmmssffff");
				}
				var result=await houseAreaRepository.AddAsync(mapper.Map<WareHouseArea>(houseAreaDto));
				return result > 0 ? ApiResult.Success(ResultCode.Ok) : ApiResult.Fail(ResultCode.Fail, "添加库区失败");
			}
			catch (Exception)
			{

				throw;
			}
		}

		/// <summary>
		/// 删除库区
		/// </summary>
		/// <param name="houseAreaId"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<ApiResult> DeleteHouseAreaAsync(int houseAreaId)
		{
			try
			{
				var houseArea = await houseAreaRepository.GetByIdAsync(houseAreaId);
				if (houseArea != null)
				{
					await houseAreaRepository.SoftDeleteAsync(houseAreaId);
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
		/// 获取库区列表
		/// </summary>
		/// <param name="houseAreaSearch"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<ApiResult<ApiPaging<List<WareHouseAreaDto>>>> GetHouseAreaListAsync(WareHouseAreaSearch houseAreaSearch)
		{
			try
			{
				var housearea = houseAreaRepository.GetAll();
				if(houseAreaSearch.WareHouseAreaId != 0)
				{
                    housearea = housearea.Where(d => d.Id == houseAreaSearch.WareHouseAreaId);
				}
				var totalCount = housearea.Count();
                var totalPage = (int)Math.Ceiling(housearea.Count() * 1.0 / houseAreaSearch.PageSize);
                var page = housearea.Skip((houseAreaSearch.PageIndex - 1) * houseAreaSearch.PageSize).Take(houseAreaSearch.PageSize).ToList();
                var data = mapper.Map<List<WareHouseAreaDto>>(page);
                var apiPagingData = new ApiPaging<List<WareHouseAreaDto>>
                {
                    TotalCount = totalCount,
                    TotalPage = totalPage,
                    Data = data
                };
                return ApiResult<ApiPaging<List<WareHouseAreaDto>>>.Success(ResultCode.Ok, apiPagingData);
			}
			catch (Exception)
			{

				throw;
			}
		}

		/// <summary>
		/// 更新库区信息
		/// </summary>
		/// <param name="houseAreaDto"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<ApiResult<WareHouseAreaDto>> UpdateHouseAreaAsync(int houseAreaId,CreateUpdateWareHouseAreaDto houseAreaDto)
		{
			try
			{
				var area =await houseAreaRepository.GetByIdAsync(houseAreaId);
				if (area == null)
				{
					return ApiResult<WareHouseAreaDto>.Fail(ResultCode.Fail, "该库区不存在");
				}
				var result=mapper.Map(houseAreaDto, area);
				var isWin=await houseAreaRepository.UpdateAsync(result);
				return isWin > 0 ? ApiResult<WareHouseAreaDto>.Success(ResultCode.Ok, mapper.Map<WareHouseAreaDto>(result)) : ApiResult<WareHouseAreaDto>.Fail(ResultCode.Fail, "更新库区失败");
			}
			catch (Exception)
			{

				throw;
			}
		}

		/// <summary>
		/// 根据仓库ID获取库区列表
		/// </summary>
		/// <param name="wareHouseId"></param>
		/// <returns></returns>
		public async Task<ApiResult<List<WareHouseAreaDto>>> GetHouseAreaAllAsync(int wareHouseId)
		{ 
			var houseAreaList = await houseAreaRepository.GetAll().Where(x => x.WareHouseId == wareHouseId).ToListAsync();
			var result= mapper.Map<List<WareHouseAreaDto>>(houseAreaList);
			return ApiResult<List<WareHouseAreaDto>>.Success(ResultCode.Ok,result);
		}



	}
}
