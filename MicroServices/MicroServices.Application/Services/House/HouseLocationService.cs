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
	/// 库位服务实现
	/// </summary>
	public class HouseLocationService : IHouseLocationService
	{

		private readonly IHouseLocationRepository _houseLocationRepository;
		private readonly IMapper mapper;

		public HouseLocationService(IHouseLocationRepository houseLocationRepository, IMapper mapper)
		{
			_houseLocationRepository = houseLocationRepository;
			this.mapper = mapper;
		}

		/// <summary>
		/// 添加库位
		/// </summary>
		/// <param name="houseLocationDto"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<ApiResult> AddHouseLocationAsync(CreateUpdateHouseLocationDto houseLocationDto)
		{
			try
			{
				var houseLocation =await _houseLocationRepository.GetAll().Where(d => d.CreatedByUserName == houseLocationDto.LocationName&&d.AreaId== houseLocationDto.AreaId).AnyAsync();
                if (houseLocation)
				{
					return ApiResult.Fail(ResultCode.Fail, "该库位已存在");
				}
				if (houseLocationDto.LocationNum == null || houseLocationDto.LocationNum == "string")
				{
					houseLocationDto.LocationNum = "KWBH" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
				}
				var result= await _houseLocationRepository.AddAsync(mapper.Map<WareHouseLocation>(houseLocationDto));
				return result > 0 ? ApiResult.Success(ResultCode.Ok) : ApiResult.Fail(ResultCode.Fail, "添加库位失败");
			}
			catch (Exception)
			{

				throw;
			}
		}

		/// <summary>
		/// 删除库位
		/// </summary>
		public async Task<ApiResult> DeleteHouseLocationAsync(int houseLocationId)
		{
			try
			{
				var houseLocation = _houseLocationRepository.GetByIdAsync(houseLocationId);
                if (houseLocation != null)
                {
                    _houseLocationRepository.SoftDeleteAsync(houseLocationId);
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
		/// 获取库位列表
		/// </summary>
		public async Task<ApiResult<ApiPaging<List<HouseLocationDto>>>> GetHouseLocationListAsync(HouseLocationSearch houseLocationSearch)
		{
			try
			{
				var houseLocationList = _houseLocationRepository.GetAll();
				if (houseLocationSearch.HouseLocationId != 0)
				{
                    houseLocationList = houseLocationList.Where(d => d.Id == houseLocationSearch.HouseLocationId);
				}
				var totalCount = houseLocationList.Count();
                var totalPage = (int)Math.Ceiling(houseLocationList.Count() * 1.0 / houseLocationSearch.PageSize);
                var page = houseLocationList.Skip((houseLocationSearch.PageIndex - 1) * houseLocationSearch.PageSize).Take(houseLocationSearch.PageSize).ToList();
                var data = mapper.Map<List<HouseLocationDto>>(page);
                var apiPagingData = new ApiPaging<List<HouseLocationDto>>
                {
                    TotalCount = totalCount,
                    TotalPage = totalPage,
                    Data = data
                };
                return ApiResult<ApiPaging<List<HouseLocationDto>>>.Success(ResultCode.Ok, apiPagingData);
			}
			catch (Exception)
			{

				throw;
			}
		}

		/// <summary>
		/// 修改库位信息
		/// </summary>
		public async Task<ApiResult<HouseLocationDto>> UpdateHouseLocationAsync(int HouseLocationId,CreateUpdateHouseLocationDto houseLocationDto)
		{
			try
			{
				var houselocation =await _houseLocationRepository.GetByIdAsync(HouseLocationId);
				if (houselocation == null)
				{
                    return ApiResult<HouseLocationDto>.Fail(ResultCode.Fail, "该库位不存在");
				}
				var result=mapper.Map(houseLocationDto, houselocation);
				var isWin= await _houseLocationRepository.UpdateAsync(result);
				return isWin > 0 ? ApiResult<HouseLocationDto>.Success(ResultCode.Ok, mapper.Map<HouseLocationDto>(result)) : ApiResult<HouseLocationDto>.Fail(ResultCode.Fail, "库位更新失败");

			}
			catch (Exception)
			{

				throw;
			}
		}

		/// <summary>
		/// 根据库位Id查询库位信息
		/// </summary>
		/// <param name="houseLocationId"></param>
		/// <returns></returns>
        public async Task<ApiResult<List<HouseLocationDto>>> GetHouseLocationByIdAsync(int AreaId)
		{ 
			try
			{
				var houseLocation = _houseLocationRepository.GetAll().Where(x => x.AreaId == AreaId).ToList();
				var	 result = mapper.Map<List<HouseLocationDto>>(houseLocation);
				return ApiResult<List<HouseLocationDto>>.Success(ResultCode.Ok,result);
			}
			catch (Exception)
			{

				throw;
			}	
		}
	}
}
