using MicroServices.Models.Dtos.House;
using MricoServices.Shared.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Application.IService.Houses
{
	/// <summary>
	/// 库区服务接口
	/// </summary>
	public interface IHouseAreaService
	{
		//新增库区
        Task<ApiResult> AddHouseAreaAsync(CreateUpdateWareHouseAreaDto houseAreaDto);

		/// <summary>
		/// 获取库区分页列表
		/// </summary>
		/// <param name="houseAreaSearch"></param>
		/// <returns></returns>
		Task<ApiResult<ApiPaging<List<WareHouseAreaDto>>>> GetHouseAreaListAsync(WareHouseAreaSearch houseAreaSearch);

		/// <summary>
		/// 更新库区
		/// </summary>
		/// <param name="houseAreaId"></param>
		/// <param name="houseAreaDto"></param>
		/// <returns></returns>
		Task<ApiResult<WareHouseAreaDto>> UpdateHouseAreaAsync(int houseAreaId,CreateUpdateWareHouseAreaDto houseAreaDto);

		/// <summary>
		/// 删除库区
		/// </summary>
		/// <param name="houseAreaId"></param>
		/// <returns></returns>
        Task<ApiResult> DeleteHouseAreaAsync(int houseAreaId);

		/// <summary>
		/// 根据仓库ID获取库区列表
		/// </summary>
		/// <param name="WareHouseId"></param>
		/// <returns></returns>
        Task<ApiResult<List<WareHouseAreaDto>>> GetHouseAreaAllAsync(int WareHouseId);
	}
}
