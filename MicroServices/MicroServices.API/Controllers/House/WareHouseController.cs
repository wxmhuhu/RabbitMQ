using MicroServices.Application.IService.Houses;
using MicroServices.Models.Dtos.House;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MricoServices.Shared.ApiResult;

namespace MicroServices.API.Controllers.House
{
	/// <summary>
	/// 仓库管理
	/// </summary>
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class WareHouseController : ControllerBase
	{
		private readonly IWareHouseService _wareHouseService;

		public WareHouseController(IWareHouseService wareHouseService)
		{
			_wareHouseService = wareHouseService;
		}

		/// <summary>
		/// 添加仓库
		/// </summary>
		/// <param name="wareHouseDto"></param>
		/// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> AddWareHouseAsync(CreateUpdateWareHouseDto wareHouseDto)
        {
            return await _wareHouseService.AddWareHouseAsync(wareHouseDto);
        }

		/// <summary>
		/// 删除仓库
		/// </summary>
		/// <param name="wareHouseId"></param>
		/// <returns></returns>
        [HttpDelete]
        public async Task<ApiResult> DeleteWareHouseAsync(int wareHouseId)
        {
            return await _wareHouseService.DeleteWareHouseAsync(wareHouseId);
        }

		/// <summary>
		/// 获取仓库分页列表
		/// </summary>
		/// <param name="wareHouseSearch"></param>
		/// <returns></returns>
        [HttpGet]
		public async Task<ApiResult<ApiPaging<List<WareHouseDto>>>> GetWareHouseListAsync([FromQuery]WareHouseSearch wareHouseSearch)
		{
            return await _wareHouseService.GetWareHouseListAsync(wareHouseSearch);
        }

		/// <summary>
		/// 更新仓库信息
		/// </summary>
		/// <param name="wareHouseId"></param>
		/// <param name="wareHouseDto"></param>
		/// <returns></returns>
		[HttpPut]
        public async Task<ApiResult<WareHouseDto>> UpdateWareHouseAsync(int wareHouseId, CreateUpdateWareHouseDto wareHouseDto)
		{
            return await _wareHouseService.UpdateWareHouseAsync(wareHouseId, wareHouseDto);
        }

		/// <summary>
		/// 获取仓库列表
		/// </summary>
		/// <param name="wareHouseSearch"></param>
		/// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<WareHouseDto>>> GetAllWareHouseListAsync()
		{
            return await _wareHouseService.GetAllWareHouseAsync();
        }
	}
}
