using MicroServices.Application.IService.Bom;
using MicroServices.Domain.Bom;
using MicroServices.Models.Dtos.Bom;
using MicroServices.Repository.IRepository.I_BOM_Repository;
using MicroServices.Repository.Repository.BOM_Repository;
using MicroServices.Repository.Repository.Product_Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MricoServices.Shared.ApiResult;

namespace MicroServices.API.Controllers.Bom
{
    /// <summary>
    /// Bom
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BomController : ControllerBase
    {
        private readonly IBomService bomService;

        public BomController(IBomService bomService)
        {
            this.bomService = bomService;
        }
        /// <summary>
        /// 获取Bom
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ApiPaging<List<BomDto>>>> GetBomAsync([FromQuery]Search search)
        {
            try
            {
                return await bomService.GetBomAsync(search);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 获取BOM树形结构
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<BomTreeDto>>> GetBomTreeAsync(int? id)
        {
            return await bomService.GetBomTreeAsync(id);
        }
        /// <summary>
        /// 新增Bom
        /// </summary>
        /// <param name="createBomDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<BOM>> CreateBomAsync(CreateOrUpdateBomDto createBomDto)
        {
            try
            {
                return await bomService.CreateBomAsync(createBomDto);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 删除Bom
        /// </summary>
        /// <param name="Bomid"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpDelete]
        public async Task<ApiResult<int>> DeleteBomAsync([FromQuery]int[] Bomid)
        {
            try
            {
                return await bomService.DeleteBomAsync(Bomid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 编辑Bom
        /// </summary>
        /// <param name="Bomid"></param>
        /// <param name="updateBomDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPut]
        public async Task<ApiResult<BOM>> UpdateBomAsync(int Bomid, CreateOrUpdateBomDto updateBomDto)
        {
            try
            {
                return await bomService.UpdateBomAsync(Bomid, updateBomDto);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
