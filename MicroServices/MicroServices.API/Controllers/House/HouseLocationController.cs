using MicroServices.Application.IService.Houses;
using MicroServices.Models.Dtos.House;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MricoServices.Shared.ApiResult;

namespace MicroServices.API.Controllers.House
{
	/// <summary>
	/// 库位管理
	/// </summary>
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class HouseLocationController : ControllerBase
	{
		private readonly IHouseLocationService _houseLocationService;

		public HouseLocationController(IHouseLocationService houseLocationService)
		{
			_houseLocationService = houseLocationService;
		}

		/// <summary>
		/// 添加库位
		/// </summary>
		/// <param name="houseLocationDto"></param>
		/// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> AddHouseLocationAsync(CreateUpdateHouseLocationDto houseLocationDto)
        {
            return await _houseLocationService.AddHouseLocationAsync(houseLocationDto);
        }

		/// <summary>
		/// 删除库位
		/// </summary>
		/// <param name="houseLocationId"></param>
		/// <returns></returns>
        [HttpDelete]
        public async Task<ApiResult> DeleteHouseLocationAsync(int houseLocationId)
        {
            return await _houseLocationService.DeleteHouseLocationAsync(houseLocationId);
        }

		/// <summary>
		/// 获取库位列表
		/// </summary>
		/// <param name="houseLocationSearch"></param>
		/// <returns></returns>
        [HttpGet]
		public async Task<ApiResult<ApiPaging<List<HouseLocationDto>>>> GetHouseLocationListAsync([FromQuery]HouseLocationSearch houseLocationSearch)
		{
			return await _houseLocationService.GetHouseLocationListAsync(houseLocationSearch);
		}

		/// <summary>
		/// 修改库位
		/// </summary>
		/// <param name="houseLocationId"></param>
		/// <param name="houseLocationDto"></param>
		/// <returns></returns>
        [HttpPut]
        public async Task<ApiResult<HouseLocationDto>> UpdateHouseLocationAsync(int houseLocationId, CreateUpdateHouseLocationDto houseLocationDto)
		{
            return await _houseLocationService.UpdateHouseLocationAsync(houseLocationId, houseLocationDto);
		}

		/// <summary>
		/// 根据库区Id获取库位列表
		/// </summary>
		/// <param name="AreaId"></param>
		/// <returns></returns>
        [HttpGet]
		public async Task<ApiResult<List<HouseLocationDto>>> GetHouseLocationByIdAsync(int AreaId)
		{
            return await _houseLocationService.GetHouseLocationByIdAsync(AreaId);
		}
	}
}
