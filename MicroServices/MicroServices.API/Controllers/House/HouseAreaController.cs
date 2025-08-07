using MicroServices.Application.IService.Houses;
using MicroServices.Models.Dtos.House;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MricoServices.Shared.ApiResult;

namespace MicroServices.API.Controllers.House
{
	/// <summary>
	/// 库区管理
	/// </summary>
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class HouseAreaController : ControllerBase
	{
        private readonly IHouseAreaService _houseAreaService;

		public HouseAreaController(IHouseAreaService houseAreaService)
		{
			_houseAreaService = houseAreaService;
		}

		/// <summary>
        /// 添加库区
        /// </summary>
        /// <param name="houseAreaDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> AddHouseAreaAsync(CreateUpdateWareHouseAreaDto houseAreaDto)
        {
            return await _houseAreaService.AddHouseAreaAsync(houseAreaDto);
        }

		/// <summary>
        /// 删除库区
        /// </summary>
        /// <param name="houseAreaId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ApiResult> DeleteHouseAreaAsync(int houseAreaId)
        {
            return await _houseAreaService.DeleteHouseAreaAsync(houseAreaId);
        }

		/// <summary>
        /// 获取库区列表
        /// </summary>
        /// <param name="houseAreaSearch"></param>
        /// <returns></returns>
		[HttpGet]
        public async Task<ApiResult<ApiPaging<List<WareHouseAreaDto>>>> GetHouseAreaListAsync([FromQuery]WareHouseAreaSearch houseAreaSearch)
        {
            return await _houseAreaService.GetHouseAreaListAsync(houseAreaSearch);
        }

		/// <summary>
        /// 更新库区
        /// </summary>
        /// <param name="houseAreaDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ApiResult<WareHouseAreaDto>> UpdateHouseAreaAsync(int houseAreaId, CreateUpdateWareHouseAreaDto houseAreaDto)
        {
            return await _houseAreaService.UpdateHouseAreaAsync(houseAreaId,houseAreaDto);
        }

        /// <summary>
        /// 根据仓库Id获取库区列表
        /// </summary>
        /// <param name="wareHouseId">仓库Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<WareHouseAreaDto>>> GetHouseAreaAllAsync(int wareHouseId)
        {
            return await _houseAreaService.GetHouseAreaAllAsync(wareHouseId);
        }

	}
}
