using AutoMapper;
using MicroServices.Application.IService.ProcessInfo;
using MicroServices.Domain.ProcessInfo;
using MicroServices.Models.Dtos;
using MicroServices.Repository.IRepository.I_Process_Repository;
using MicroServices.Repository.Repository.Process_Repository;
using MricoServices.Shared.ApiResult;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Application.Services.ProcessInfo
{
    public class ProcessCompositionService : IProcessCompositionService
    {
        private readonly IProcessRouteRepository processRouteRepository;
        private readonly IProcessCompositionRepository processCompositionRepository;
        private readonly IMapper mapper;

        public ProcessCompositionService(IProcessRouteRepository processRouteRepository, IProcessCompositionRepository processCompositionRepository, IMapper mapper)
        {
            this.processRouteRepository = processRouteRepository;
            this.processCompositionRepository = processCompositionRepository;
            this.mapper = mapper;
        }
        /// <summary>
        /// 获取所有工序组合
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<List<ProcessCompositionDto>>> GetProcessCompositionAsync(int? processrouteId)
        {
            try
            {
                // 查询并包含导航属性
                var query = processCompositionRepository.GetAll().Includes(x => x.ProcessInfo);
                var compositions = await query.ToListAsync();

                // 映射为 DTO
                var dtos = compositions.Select(x => new ProcessCompositionDto
                {
                    Id = x.Id,
                    SerialNumber = x.SerialNumber,
                    ProcessId = x.ProcessId,
                    ProcessRouteId = x.ProcessRouteId,
                    NexiProcessId = x.NexiProcessId,
                    NextProcessRelation = x.NextProcessRelation,
                    KeyProcess = x.KeyProcess,
                    ReadinessTime = x.ReadinessTime,
                    WaitingTime = x.WaitingTime,
                    Colors = x.Colors,
                    Remark = x.Remark,
                    // 重点：从导航属性获取工序编号和名称
                    ProcessCode = x.ProcessInfo?.ProcessCode,
                    ProcessName = x.ProcessInfo?.ProcessName
                }).ToList();

                if (processrouteId != null)
                {
                    dtos = dtos.Where(x => x.ProcessRouteId == processrouteId).ToList();
                }

                return ApiResult<List<ProcessCompositionDto>>.Success(ResultCode.Ok, dtos);
            }
            catch (Exception ex)
            {
                return ApiResult<List<ProcessCompositionDto>>.Fail(ResultCode.Fail, ex.Message);
            }
        }
        /// <summary>
        /// 新建工序组合
        /// </summary>
        /// <param name="createProcessCompositionDto"></param>
        /// <returns></returns>
        public async Task<ApiResult<ProcessComposition>> CreateProcessCompositionAsync(CreateOrUpdateProcessCompositionDto createProcessCompositionDto)
        {
            try
            {
                // 1. 映射 DTO 到实体
                var processComposition = mapper.Map<ProcessComposition>(createProcessCompositionDto);

                // 2. 保存到数据库
                await processCompositionRepository.AddAsync(processComposition);

                return ApiResult<ProcessComposition>.Success(ResultCode.Ok, processComposition);
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
        public async Task<ApiResult<int>> DeleteProcessCompositionAsync(int[] processCompositionId)
        {
            try
            {
                int i = 0;
                foreach (var item in processCompositionId)
                {
                    var processComposition = await processCompositionRepository.SoftDeleteAsync(item);
                    if (processComposition == 0)
                    {
                        return ApiResult<int>.Fail(ResultCode.Fail, "删除失败");
                    }
                    i++;
                }
                return ApiResult<int>.Success(ResultCode.Ok, i);
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
        /// <param name="UpdateProcessCompositionDto"></param>
        /// <returns></returns>
        public async Task<ApiResult<ProcessComposition>> UpdateProcessCompositionAsync(int processCompositionId, CreateOrUpdateProcessCompositionDto UpdateProcessCompositionDto)
        {
            try
            {
                var processComposition = await processCompositionRepository.GetByIdAsync(processCompositionId);
                if (processComposition == null)
                {
                    return ApiResult<ProcessComposition>.Fail(ResultCode.Fail, "工序组合不存在");
                }
                var returninfo = mapper.Map(UpdateProcessCompositionDto, processComposition);
                await processCompositionRepository.UpdateAsync(returninfo);
                return ApiResult<ProcessComposition>.Success(ResultCode.Ok, returninfo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
