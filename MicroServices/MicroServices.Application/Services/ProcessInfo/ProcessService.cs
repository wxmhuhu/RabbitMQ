using AutoMapper;
using MicroServices.Application.IService.ProcessInfo;
using MicroServices.Domain.ProcessInfo;
using MicroServices.Models.Dtos;
using MicroServices.Repository.IRepository.I_Process_Repository;
using MricoServices.Shared.ApiResult;

namespace MicroServices.Application.Services.ProcessInfo
{
    public class ProcessService : IProcessService
    {
        private readonly IProcessRepository processRepository;
        private readonly IMapper mapper;

        public ProcessService(IProcessRepository processRepository,IMapper mapper)
        {
            this.processRepository = processRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// 获取工序信息
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<List<ProcessDto>>> GetProcessAsync()
        {
            try
            {
                // 查询所有工序数据
                var processes = await processRepository.GetAll().ToListAsync();

                var dtoinfo=mapper.Map<List<ProcessDto>>(processes);

                if (dtoinfo == null|| dtoinfo.Count <= 0)
                {
                    return ApiResult<List<ProcessDto>>.Fail(ResultCode.Fail, "获取工序数据失败");
                }

                return ApiResult<List<ProcessDto>>.Success(ResultCode.Ok, dtoinfo);
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
        public async Task<ApiResult<Processes>> CreateProcessAsync(CreateOrUpdateProcessDto createOrUpdateProcessDto)
        {
            try
            {
                // 自动生成唯一工序编号
                string routeCode;
                bool exists;
                var datePart = DateTime.Now.ToString("yyyyMMdd");
                var prefix = "GXBH" + datePart;
                var random = new Random();
                int tryCount = 0;
                do
                {
                    var randomNumber = random.Next(0, 10000).ToString("D4");
                    routeCode = prefix + randomNumber;
                    // 判断编号是否已存在
                    exists = await processRepository.GetAll()
                        .AnyAsync(r => r.ProcessCode == routeCode);
                    tryCount++;
                    if (tryCount > 20)
                        throw new Exception("生成唯一工序编号失败，请重试。");
                } while (exists);

                // 赋值到DTO
                createOrUpdateProcessDto.ProcessCode = routeCode;

                var process = mapper.Map<Processes>(createOrUpdateProcessDto);
                await processRepository.AddAsync(process);

                return ApiResult<Processes>.Success(ResultCode.Ok, process);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 批量删除工序信息
        /// </summary>
        /// <param name="processid"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApiResult<int>> DeleteProcessAsync(int[] processid)
        {
            try
            {
                int i = 0;
                foreach (var item in processid)
                {
                    var process = await processRepository.SoftDeleteAsync(item);
                    if (process == 0)
                    {
                        return ApiResult<int>.Fail(ResultCode.Fail, "删除失败");
                    }
                    i++;
                }
                return ApiResult<int>.Success(ResultCode.Ok, i);
            }
            catch (Exception)
            {
                return ApiResult<int>.Fail(ResultCode.Fail, "删除工序信息失败，请稍后重试");
            }
        }
        /// <summary>
        /// 编辑工序信息
        /// </summary>
        /// <param name="updateOrUpdateProcessDto"></param>
        /// <returns></returns>
        public async Task<ApiResult<Processes>> UpdateProcessAsync(int processid, CreateOrUpdateProcessDto updateOrUpdateProcessDto)
        {
            try
            {
                var process =await processRepository.GetByIdAsync(processid);
                if (process == null)
                {
                    return ApiResult<Processes>.Fail(ResultCode.Fail, "工序不存在");
                }
                var returninfo=mapper.Map(updateOrUpdateProcessDto, process);
                await processRepository.UpdateAsync(returninfo);
                return ApiResult<Processes>.Success(ResultCode.Ok, returninfo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
