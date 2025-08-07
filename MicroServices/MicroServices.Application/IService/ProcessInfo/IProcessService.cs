using MicroServices.Domain.ProcessInfo;
using MicroServices.Models.Dtos;
using MricoServices.Shared.ApiResult;

namespace MicroServices.Application.IService.ProcessInfo
{
    public interface IProcessService
    {
        /// <summary>
        /// 获取工序
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<ProcessDto>>> GetProcessAsync();
        /// <summary>
        /// 新增工序
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<Processes>> CreateProcessAsync(CreateOrUpdateProcessDto createOrUpdateProcessDto);
        /// <summary>
        /// 批量删除工序
        /// </summary>
        /// <param name="processid"></param>
        /// <returns></returns>
        Task<ApiResult<int>> DeleteProcessAsync(int[] processid);
        /// <summary>
        /// 编辑工序
        /// </summary>
        /// <param name="updateOrUpdateProcessDto"></param>
        /// <returns></returns>
        Task<ApiResult<Processes>> UpdateProcessAsync(int processid, CreateOrUpdateProcessDto updateOrUpdateProcessDto);
    }
}
