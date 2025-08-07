using AutoMapper;
using MicroServices.Application.IService.ProcessInfo;
using MicroServices.Domain.Materials;
using MicroServices.Domain.ProcessInfo;
using MicroServices.Models.Dtos;
using MicroServices.Models.Dtos.ProductDtos;
using MicroServices.Repository.IRepository.I_Process_Repository;
using MricoServices.Shared.ApiResult;

namespace MicroServices.Application.Services.ProcessInfo
{
    public class ProcessRouteService : IProcessRouteService
    {
        private readonly IProcessRouteRepository processRouteRepository;
        private readonly IProcessCompositionRepository processCompositionRepository;
        private readonly IMapper mapper;

        public ProcessRouteService(IProcessRouteRepository processRouteRepository,IProcessCompositionRepository processCompositionRepository,IMapper mapper)
        {
            this.processRouteRepository = processRouteRepository;
            this.processCompositionRepository = processCompositionRepository;
            this.mapper = mapper;
        }
        /// <summary>
        /// 获取工艺路线
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<ApiPaging<List<ProcessRouteDto>>>> GetProcessRouteAsync(Models.Dtos.Search search)
        {
            try
            {
                var processRoute = processRouteRepository.GetAll();
                var totalCount = await processRoute.CountAsync();
                var totalPage = (int)Math.Ceiling(totalCount * 1.0 / search.PageSize); // 使用 totalCount 而非再次 count

                var pagedData = await processRoute.OrderByDescending(processRoute => processRoute.Id) // 假设 Production_Planning 有 CreatedAt 属性
                                           .Skip((search.PageIndex - 1) * search.PageSize)
                                           .Take(search.PageSize)
                                           .ToListAsync();


                var dtoinfo = mapper.Map<List<ProcessRouteDto>>(pagedData);
                var apiPagingData = new ApiPaging<List<ProcessRouteDto>>
                {
                    TotalCount = totalCount,
                    TotalPage = totalPage,
                    Data = dtoinfo
                };

                return ApiResult<ApiPaging<List<ProcessRouteDto>>>.Success(ResultCode.Ok, apiPagingData);
            }
            catch (Exception ex)
            {
                return ApiResult<ApiPaging<List<ProcessRouteDto>>>.Fail(ResultCode.Fail, ex.Message);
            }
        }
        /// <summary>
        /// 新增工艺路线
        /// </summary>
        /// <param name="createOrUpdateProcessRouteDto"></param>
        /// <returns></returns>
        public async Task<ApiResult<ProcessRoute>> CreateProcessRouteAsync(CreateOrUpdateProcessRouteDto createProcessRouteDto)
        {
            try
            {
                // 自动生成唯一工艺路线编号
                string routeCode;
                bool exists;
                var datePart = DateTime.Now.ToString("yyyyMMdd");
                var prefix = "GYLXBH" + datePart;
                var random = new Random();
                int tryCount = 0;
                do
                {
                    var randomNumber = random.Next(0, 10000).ToString("D4");
                    routeCode = prefix + randomNumber;
                    // 判断编号是否已存在
                    exists = await processRouteRepository.GetAll()
                        .AnyAsync(r => r.ProcessRouteCode == routeCode);
                    tryCount++;
                    if (tryCount > 20)
                        throw new Exception("生成唯一工艺路线编号失败，请重试。");
                } while (exists);

                // 赋值到DTO
                createProcessRouteDto.ProcessRouteCode = routeCode;

                var processRoute = mapper.Map<ProcessRoute>(createProcessRouteDto);
                await processRouteRepository.AddAsync(processRoute);

                return ApiResult<ProcessRoute>.Success(ResultCode.Ok, processRoute);
            }
            catch (Exception ex)
            {
                return ApiResult<ProcessRoute>.Fail(ResultCode.Fail, ex.Message);
            }
        }
        /// <summary>
        /// 批量删除工艺路线
        /// </summary>
        /// <param name="processRouteId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApiResult<int>> DeleteProcessRouteAsync(int[] processRouteId)
        {
            try
            {
                int i = 0;
                foreach (var item in processRouteId)
                {
                    //先判断该工艺路线下是否有工艺组成
                    var processComposition = await processCompositionRepository.GetByIdAsync(item);
                    if (processComposition != null)
                    {
                        return ApiResult<int>.Fail(ResultCode.Fail, "选中工艺路线下有工艺组成，请先删除其工艺组成");
                    }
                    //删除工艺路线
                    var processRoute = await processRouteRepository.SoftDeleteAsync(item);
                    if (processRoute == 0)
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
        /// 编辑工艺路线
        /// </summary>
        /// <param name="processRouteId"></param>
        /// <param name="UpdateProcessRouteDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApiResult<ProcessRoute>> UpdateProcessRouteAsync(int processRouteId, CreateOrUpdateProcessRouteDto UpdateProcessRouteDto)
        {
            try
            {
                var processRoute=await processRouteRepository.GetByIdAsync(processRouteId);
                if (processRoute == null)
                {
                    return ApiResult<ProcessRoute>.Fail(ResultCode.Fail, "工艺路线不存在");
                }
                var returninfo=mapper.Map(UpdateProcessRouteDto, processRoute);
                await processRouteRepository.UpdateAsync(returninfo);
                return ApiResult<ProcessRoute>.Success(ResultCode.Ok, returninfo);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
