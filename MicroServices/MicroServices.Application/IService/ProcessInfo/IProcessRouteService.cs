using MicroServices.Domain.ProcessInfo;
using MicroServices.Models.Dtos;
using MricoServices.Shared.ApiResult;

namespace MicroServices.Application.IService.ProcessInfo
{
    public interface IProcessRouteService
    {
        /// <summary>
        /// 获取工艺路线
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<ApiPaging<List<ProcessRouteDto>>>> GetProcessRouteAsync(Search search);
        /// <summary>
        /// 创建工艺路线
        /// </summary>
        /// <param name="createOrUpdateProcessRouteDto"></param>
        /// <returns></returns>
        Task<ApiResult<ProcessRoute>> CreateProcessRouteAsync(CreateOrUpdateProcessRouteDto createProcessRouteDto);
        /// <summary>
        /// 批量删除工艺路线
        /// </summary>
        /// <param name="processRouteId"></param>
        /// <returns></returns>
        Task<ApiResult<int>> DeleteProcessRouteAsync(int[] processRouteId);
        /// <summary>
        /// 编辑工艺路线
        /// </summary>
        /// <param name="processRouteId"></param>
        /// <param name="UpdateProcessRouteDto"></param>
        /// <returns></returns>
        Task<ApiResult<ProcessRoute>> UpdateProcessRouteAsync(int processRouteId, CreateOrUpdateProcessRouteDto UpdateProcessRouteDto);
    }
}
