using MicroServices.Domain.ProcessInfo;
using MicroServices.Models.Dtos;
using MricoServices.Shared.ApiResult;

namespace MicroServices.Application.IService.ProcessInfo
{
    public interface IProcessCompositionService
    {
        /// <summary>
        /// 获取工序组合
        /// </summary>
        /// <param name="processrouteId"></param>
        /// <returns></returns>
        Task<ApiResult<List<ProcessCompositionDto>>> GetProcessCompositionAsync(int? processrouteId);
        /// <summary>
        /// 新建工序组合
        /// </summary>
        /// <param name="createOrUpdateProcessCompositionDto"></param>
        /// <returns></returns>
        Task<ApiResult<ProcessComposition>> CreateProcessCompositionAsync(CreateOrUpdateProcessCompositionDto createProcessCompositionDto);
        /// <summary>
        /// 批量删除工序组合
        /// </summary>
        /// <param name="processCompositionId"></param>
        /// <returns></returns>
        Task<ApiResult<int>> DeleteProcessCompositionAsync(int[] processCompositionId);
        /// <summary>
        /// 编辑工序组合
        /// </summary>
        /// <param name="processCompositionId"></param>
        /// <param name="createOrUpdateProcessCompositionDto"></param>
        /// <returns></returns>
        Task<ApiResult<ProcessComposition>> UpdateProcessCompositionAsync(int processCompositionId, CreateOrUpdateProcessCompositionDto UpdateProcessCompositionDto);
    }
}
