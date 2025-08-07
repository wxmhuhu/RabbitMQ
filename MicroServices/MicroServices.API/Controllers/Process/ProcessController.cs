using MicroServices.Application.IService.ProcessInfo;
using MicroServices.Domain.ProcessInfo;
using MicroServices.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using MricoServices.Shared.ApiResult;

namespace MicroServices.API.Controllers
{
    /// <summary>
    /// 工序，工序组成，工艺路线
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        private readonly IProcessService processService;
        private readonly IProcessCompositionService compositionService;
        private readonly IProcessRouteService processRouteService;

        public ProcessController(IProcessService processService,IProcessCompositionService compositionService,IProcessRouteService processRouteService)
        {
            this.processService = processService;
            this.compositionService = compositionService;
            this.processRouteService = processRouteService;
        }
        #region 工序
        /// <summary>
        /// 获取工序
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ProcessDto>>> GetProcessAsync()
        {
            try
            {
                return await processService.GetProcessAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 创建工序
        /// </summary>
        /// <param name="createOrUpdateProcessDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<Processes>> CreateProcessAsync(CreateOrUpdateProcessDto createOrUpdateProcessDto)
        {
            try
            {
                return await processService.CreateProcessAsync(createOrUpdateProcessDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 批量删除工序
        /// </summary>
        /// <param name="processid"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpDelete]
        public async Task<ApiResult<int>> DeleteProcessAsync([FromQuery]int[] processid)
        {
            try
            {
                return await processService.DeleteProcessAsync(processid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 编辑工序
        /// </summary>
        /// <param name="updateOrUpdateProcessDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ApiResult<Processes>> UpdateProcessAsync(int processid, CreateOrUpdateProcessDto updateOrUpdateProcessDto)
        {
            try
            {
                return await processService.UpdateProcessAsync(processid, updateOrUpdateProcessDto);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region 工序组合
        /// <summary>
        /// 获取所有工序组合
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ProcessCompositionDto>>> GetProcessCompositionAsync(int? processrouteId)
        {
            try
            {
                return await compositionService.GetProcessCompositionAsync(processrouteId);
            }
            catch (Exception ex)
            {
                return ApiResult<List<ProcessCompositionDto>>.Fail(ResultCode.Fail, ex.Message);
            }
        }
        /// <summary>
        /// 创建工序组合
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<ProcessComposition>> CreateProcessCompositionAsync(CreateOrUpdateProcessCompositionDto createProcessCompositionDto)
        {
            try
            {
                return await compositionService.CreateProcessCompositionAsync(createProcessCompositionDto);
            }
            catch (Exception ex)
            {
                return ApiResult<ProcessComposition>.Fail(ResultCode.Fail, ex.Message);
            }
        }
        /// <summary>
        /// 批量删除工序组合
        /// </summary>
        /// <param name="processCompositionId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ApiResult<int>> DeleteProcessCompositionAsync([FromQuery] int[] processCompositionId)
        {
            try
            {
                return await compositionService.DeleteProcessCompositionAsync(processCompositionId);
            }
            catch (Exception ex)
            {
                return ApiResult<int>.Fail(ResultCode.Fail, ex.Message);
            }
        }
        /// <summary>
        /// 编辑工序组合
        /// </summary>
        /// <param name="processCompositionId"></param>
        /// <param name="createOrUpdateProcessCompositionDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ApiResult<ProcessComposition>> UpdateProcessCompositionAsync(int processCompositionId, CreateOrUpdateProcessCompositionDto UpdateProcessCompositionDto)
        {
            try
            {
                return await compositionService.UpdateProcessCompositionAsync(processCompositionId, UpdateProcessCompositionDto);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region 工艺路线
        /// <summary>
        /// 获取工艺路线
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ApiPaging<List<ProcessRouteDto>>>> GetProcessRouteAsync([FromQuery]Search search)
        {
            try
            {
                return await processRouteService.GetProcessRouteAsync(search);
            }
            catch (Exception ex)
            {
                return ApiResult<ApiPaging<List<ProcessRouteDto>>>.Fail(ResultCode.Fail, ex.Message);
            }
        }
        /// <summary>
        /// 新增工艺路线
        /// </summary>
        /// <param name="createProcessRouteDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<ProcessRoute>> CreateProcessRouteAsync(CreateOrUpdateProcessRouteDto createProcessRouteDto)
        {
            try
            {
                return await processRouteService.CreateProcessRouteAsync(createProcessRouteDto);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 批量删除工艺路线
        /// </summary>
        /// <param name="processRouteId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpDelete]
        public async Task<ApiResult<int>> DeleteProcessRouteAsync([FromQuery]int[] processRouteId)
        {
            try
            {
                return await processRouteService.DeleteProcessRouteAsync(processRouteId);
            }
            catch (Exception ex)
            {
                return ApiResult<int>.Fail(ResultCode.Fail, ex.Message);
            }
        }
        /// <summary>
        /// 编辑工艺路线
        /// </summary>
        /// <param name="processRouteId"></param>
        /// <param name="UpdateProcessRouteDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPut]
        public async Task<ApiResult<ProcessRoute>> UpdateProcessRouteAsync(int processRouteId, CreateOrUpdateProcessRouteDto UpdateProcessRouteDto)
        {
            try
            {
                return await processRouteService.UpdateProcessRouteAsync(processRouteId, UpdateProcessRouteDto);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
