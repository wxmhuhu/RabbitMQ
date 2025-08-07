using MicroServices.Domain.Bom;
using MicroServices.Models.Dtos.Bom;
using MricoServices.Shared.ApiResult;

namespace MicroServices.Application.IService.Bom
{
    public interface IBomService
    {
        /// <summary>
        /// 获取Bom
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<ApiPaging<List<BomDto>>>> GetBomAsync(Search search);
        /// <summary>
        /// 获取BOM树形结构
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<BomTreeDto>>> GetBomTreeAsync(int? id);
        /// <summary>
        /// 新增Bom
        /// </summary>
        /// <param name="createBomDto"></param>
        /// <returns></returns>
        Task<ApiResult<BOM>> CreateBomAsync(CreateOrUpdateBomDto createBomDto);
        /// <summary>
        /// 删除Bom
        /// </summary>
        /// <param name="Bomid"></param>
        /// <returns></returns>
        Task<ApiResult<int>> DeleteBomAsync(int[] Bomid);
        /// <summary>
        /// 编辑Bom
        /// </summary>
        /// <param name="Bomid"></param>
        /// <param name="updateBomDto"></param>
        /// <returns></returns>
        Task<ApiResult<BOM>> UpdateBomAsync(int Bomid, CreateOrUpdateBomDto updateBomDto);
    }
}
