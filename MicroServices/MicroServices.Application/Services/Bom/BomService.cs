using AutoMapper;
using CSRedis;
using Dm;
using MicroServices.Application.IService.Bom;
using MicroServices.Domain.Bom;
using MicroServices.Models.Dtos.Bom;
using MicroServices.Models.Dtos.Product_PlanDtos;
using MicroServices.Repository.IRepository.I_BOM_Repository;
using MicroServices.Repository.IRepository.I_Material_Repository;
using MicroServices.Repository.IRepository.I_Process_Repository;
using MicroServices.Repository.IRepository.I_Product_Repository;
using MicroServices.Repository.Repository.Material_Repository;
using MicroServices.Repository.Repository.Process_Repository;
using MricoServices.Shared.ApiResult;
using SmartConference.Api.Filter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Application.Services.Bom
{
    public class BomService : IBomService
    {
        private readonly IBomRepository bomRepository;
        private readonly IProductRepository productRepository;
        private readonly IProcessRouteRepository processRouteRepository;
        private readonly IUniteRepository uniteRepository;
        private readonly IMapper mapper;
        private readonly CSRedisClient redis;
        private readonly RedisHelp<BomDto> helper;

        public BomService(IBomRepository bomRepository,IProductRepository productRepository,IProcessRouteRepository processRouteRepository,IUniteRepository uniteRepository,IMapper mapper, CSRedisClient redis, RedisHelp<BomDto> helper)
        {
            this.bomRepository = bomRepository;
            this.productRepository = productRepository;
            this.processRouteRepository = processRouteRepository;
            this.uniteRepository = uniteRepository;
            this.mapper = mapper;
            this.redis = redis;
            this.helper = helper;
        }
        /// <summary>
        /// 获取Bom
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<ApiPaging<List<BomDto>>>> GetBomAsync(Models.Dtos.Bom.Search search)
        {
            try
            {
                // 生成缓存键，包含搜索条件
                var cacheKey = $"BOM_List_{search.PageIndex}_{search.PageSize}_{search.BomCode ?? ""}_{search.ProductName ?? ""}_{search.BomVersion ?? ""}";
                
                // 使用RedisHelper缓存分页数据
                var cachedResult = await helper.GetRedisList(cacheKey, async () =>
                {
                    var bominfo = bomRepository.GetAll();

                    var totalCount = await bominfo.CountAsync();
                    var totalPage = (int)Math.Ceiling(totalCount * 1.0 / search.PageSize);

                    var pagedData = await bominfo.OrderByDescending(d => d.Id)
                                               .Skip((search.PageIndex - 1) * search.PageSize)
                                               .Take(search.PageSize)
                                               .ToListAsync();

                    var dtoList = mapper.Map<List<BomDto>>(pagedData);

                    // 根据产品id查询产品信息
                    foreach (var item in dtoList)
                    {
                        var product = await productRepository.GetByIdAsync(item.ProductId);
                        if (product != null)
                        {
                            item.ProductName = product.ProductName;
                            item.ProductCode = product.ProductCode;
                            item.Specification = product.Specification;
                            item.UniteName = (await uniteRepository.GetByIdAsync(product.Unit)).UniteName;
                        }
                    }

                    // 应用搜索过滤条件
                    if (!string.IsNullOrEmpty(search.BomCode))
                    {
                        dtoList = dtoList.Where(x => x.BomCode.Contains(search.BomCode)).ToList();
                    }
                    if (!string.IsNullOrEmpty(search.ProductName))
                    {
                        dtoList = dtoList.Where(x => x.ProductName.Contains(search.ProductName)).ToList();
                    }
                    if (!string.IsNullOrEmpty(search.BomVersion))
                    {
                        dtoList = dtoList.Where(x => x.BomVersion.Contains(search.BomVersion)).ToList();
                    }

                    return dtoList;
                }, 3600); // 缓存1小时

                // 获取总数用于分页计算
                var totalCount = await bomRepository.GetAll().CountAsync();
                var totalPage = (int)Math.Ceiling(totalCount * 1.0 / search.PageSize);

                var apiPagingData = new ApiPaging<List<BomDto>>
                {
                    TotalCount = totalCount,
                    TotalPage = totalPage,
                    Data = cachedResult
                };

                return ApiResult<ApiPaging<List<BomDto>>>.Success(ResultCode.Ok, apiPagingData);
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
        public async Task<ApiResult<List<BomTreeDto>>> GetBomTreeAsync(int? id)
        {
            // 获取所有BOM
            var bomList = await bomRepository.GetAll().ToListAsync();

            var dtolist = mapper.Map<List<BomTreeDto>>(bomList);

                    // 补充产品信息
                    foreach (var item in dtolist)
                    {
                        var product = await productRepository.GetByIdAsync(item.ProductId);
                        if (product != null)
                        {
                            item.ProductName = product.ProductName;
                            item.ProductCode = product.ProductCode;
                            item.Specification = product.Specification;
                            item.UniteName = (await uniteRepository.GetByIdAsync(product.Unit)).UniteName;
                        }
                    }

                    // 构建完整树结构
                    var lookup = dtolist.ToLookup(x => x.BomParentId);

                    // 修改后的构建方法
                    List<BomTreeDto> BuildTree(int parentId)
                    {
                        return lookup[parentId]
                            .Select(bom =>
                            {
                                bom.Children = BuildTree(bom.Id);
                                return bom;
                            }).ToList();
                    }

            // 根据是否有ID参数决定返回整树还是子树
            if (id == null)
            {
                var fullTree = BuildTree(0); // 返回完整树
                return ApiResult<List<BomTreeDto>>.Success(ResultCode.Ok, fullTree);
            }
            else
            {
                // 找到指定ID的BOM作为根节点构建子树
                var rootBom = dtolist.FirstOrDefault(x => x.Id == id);
                if (rootBom == null)
                {
                    return ApiResult<List<BomTreeDto>>.Fail(ResultCode.Fail, "未找到指定BOM");
                }

                rootBom.Children = BuildTree(rootBom.Id);
                return ApiResult<List<BomTreeDto>>.Success(ResultCode.Ok, new List<BomTreeDto> { rootBom });
            }
        }
        /// <summary>
        /// 新增Bom
        /// </summary>
        /// <param name="createBomDto"></param>
        /// <returns></returns>
        public async Task<ApiResult<BOM>> CreateBomAsync(CreateOrUpdateBomDto createBomDto)
        {
            try
            {
                // 自动生成唯一工序编号
                string routeCode;
                bool exists;
                var datePart = DateTime.Now.ToString("yyyyMMdd");
                var prefix = "BOM" + datePart;
                var random = new Random();
                int tryCount = 0;
                do
                {
                    var randomNumber = random.Next(0, 10000).ToString("D4");
                    routeCode = prefix + randomNumber;
                    // 判断编号是否已存在
                    exists = await bomRepository.GetAll()
                        .AnyAsync(r => r.BomCode == routeCode);
                    tryCount++;
                    if (tryCount > 20)
                        throw new Exception("生成唯一工序编号失败，请重试。");
                } while (exists);

                createBomDto.BomCode = routeCode;
                //判断父级id是否为空，如果为空则为顶级Bom单
                if (createBomDto.BomParentId == null)
                {
                    createBomDto.BomParentId = 0;
                }
                var bom = mapper.Map<BOM>(createBomDto);
                await bomRepository.AddAsync(bom);
                
                // 清除相关缓存
                await ClearBomCache();
                
                return ApiResult<BOM>.Success(ResultCode.Ok, bom);
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
        public async Task<ApiResult<int>> DeleteBomAsync(int[] Bomid)
        {
            try
            {
                int i = 0;
                foreach (var item in Bomid)
                {
                    var bom = await bomRepository.SoftDeleteAsync(item);
                    if (bom == 0)
                    {
                        return ApiResult<int>.Fail(ResultCode.Fail, "删除失败");
                    }
                    i++;
                }
                
                // 清除相关缓存
                await ClearBomCache();
                
                return ApiResult<int>.Success(ResultCode.Ok, 1);
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
        public async Task<ApiResult<BOM>> UpdateBomAsync(int Bomid, CreateOrUpdateBomDto updateBomDto)
        {
            try
            {
                var bominfo = await bomRepository.GetByIdAsync(Bomid);
                if (bominfo == null)
                {
                    return ApiResult<BOM>.Fail(ResultCode.Fail, "Bom不存在");
                }
                var returninfo = mapper.Map(updateBomDto, bominfo);
                await bomRepository.UpdateAsync(returninfo);
                
                // 清除相关缓存
                await ClearBomCache();
                
                return ApiResult<BOM>.Success(ResultCode.Ok, returninfo);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        /// <summary>
        /// 清除BOM相关缓存
        /// </summary>
        private async Task ClearBomCache()
        {
            try
            {
                // 清除所有BOM相关的缓存键
                var keys = await redis.KeysAsync("BOM_*");
                if (keys != null && keys.Any())
                {
                    await redis.DelAsync(keys.ToArray());
                }
            }
            catch (Exception)
            {
                // 缓存清除失败不影响主业务逻辑，只记录日志
                // 这里可以根据需要添加日志记录
            }
        }
    }
}
