using AutoMapper;
using MicroServices.Application.IService.AllocateAndInventory;
using MicroServices.Domain.Allocate;
using MicroServices.Domain.Inventory;
using MicroServices.Models.Dtos.AllocateAndInventory.Allocate;
using MicroServices.Models.Dtos.AllocateAndInventory.Inventory;
using Microsoft.Extensions.Configuration;
using MricoServices.Repository.IRepository;
using MricoServices.Shared.ApiResult;
using SqlSugar;
using SqlSugar.Extensions;
using SqlSugar.SplitTableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MicroServices.Application.Services.AllocateAndInventory
{
    public class AllocateService : IAllocateService
    {
        private readonly IConfiguration configuration;
        private readonly IBaseRepository<AllocateDetail> allocateDetaileRepository;
        private readonly IBaseRepository<Allocate>  allocateRepository;
        private readonly IMapper mapper;
        public AllocateService(IBaseRepository<AllocateDetail> allocateDetaileRepository, IBaseRepository<Allocate> allocateRepository, IMapper mapper)
        {
            this.configuration = configuration;
            this.allocateDetaileRepository = allocateDetaileRepository;
            this.allocateRepository = allocateRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// 添加盘点
        /// </summary>
        /// <param name= "dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> AddAllocateAsync(CreateUpdateInventoryDto dto)
        {
            try
            {
                var list =await allocateRepository.AddAsync(mapper.Map<Allocate>(dto));
                return list > 0
                    ? ApiResult.Success(ResultCode.Ok)
                    : ApiResult.Fail(ResultCode.Fail, "创建失败");
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 批量导出调拨
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApiResult> ExportAllocateAsync(AllocateSearch search)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取调拨
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public async Task<ApiResult<ApiPaging<List<AllocateDto>>>> GetAllocateAsync(AllocateSearch search)
        {
            try
            {
                //获取调拨信息
                var list = allocateRepository.GetAll();
                if (!string.IsNullOrEmpty(search.number))
                {
                    list = list.Where(d => d.AllocateNumber.Contains(search.number));
                }
                if (search.state != null)
                {
                    list = list.Where(d => d.State == search.state);
                }
                if (search.date != null)
                {
                    DateTime start = search.date.ObjToDate().AddDays(-1).AddMilliseconds(1);
                    DateTime end = search.date.ObjToDate().AddDays(1).AddMilliseconds(-1);
                    list = list.Where(d => d.AllocateDate >= start && d.AllocateDate >= search.date.ObjToDate());
                } 
                var totalCount = await list.CountAsync();
                var totalPage = (int)Math.Ceiling(await list.CountAsync() * 1.0 / search.PageSize);
                var page = await list.OrderByDescending(d => d.CreatedAt).Skip((search.PageIndex - 1) * search.PageSize).Take(search.PageSize).ToListAsync(); 
                var data = mapper.Map<List<AllocateDto>>(page);

                // 封装为 ApiPaging 对象
                var apiPagingData = new ApiPaging<List<AllocateDto>>
                {
                    TotalCount = totalCount,
                    TotalPage = totalPage,
                    Data = data
                }; 
                // 返回成功的 ApiResult
                return ApiResult<ApiPaging<List<AllocateDto>>>.Success(ResultCode.Ok, apiPagingData);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<ApiResult<ApiPaging<List<AllocateDetailDto>>>> GetAllocateDetailAsync(CreateUpdateAllocateDetailDto dto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 反填调拨 --ok
        /// </summary>
        public async  Task<ApiResult<AllocateDto>> GetOneAllocateAsync(int Id)
        {
            try
            {
                //获取调拨信息
                var inventory = await allocateRepository.GetAll().FirstAsync(x => x.Id == Id);
                //获取调拨明细
                return inventory != null 
                ? ApiResult<AllocateDto>.Success(ResultCode.Ok, mapper.Map<AllocateDto>(inventory)) :  
                ApiResult<AllocateDto>.Fail(ResultCode.Fail, "获取调拨信息失败");
            }//Mapper.Map<AllocateDto>(inventory)
            //
            catch (Exception)
            {
                throw;
            } 
        }
        /// <summary>
        /// 反填调拨明细 --ok
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Task<ApiResult<List<AllocateDetailDto>>> GetOneAllocateDetailAsync(int Id)
        {
            try
            {
                var allocateDetail = allocateDetaileRepository.GetAll().Where(x => x.AllocateId == Id).ToList();
                var dto = mapper.Map<List<AllocateDetailDto>>(allocateDetail);
                return Task.FromResult(ApiResult<List<AllocateDetailDto>>.Success(ResultCode.Ok, dto)); 
            }
            catch (Exception)
            {
                throw;
            } 
        }
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public async Task<ApiResult> ReviewAllocateAsync(List<int> Ids)
        {
            // 参数验证
            if (Ids == null || !Ids.Any())
            {
                return ApiResult.Fail(ResultCode.Fail, "请选择要审核的库存");
            }
            //事务
            using (var db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = configuration.GetConnectionString("DefaultConnection"),
                DbType = DbType.PostgreSQL,
                IsAutoCloseConnection = true
            }))
                try
                {
                    db.BeginTran();//开始事务

                    // 获取所有待审核的库存
                    var inventory = await allocateRepository.GetAll().Where(x => Ids.Contains(x.Id)).ToListAsync();
                    // 更新库存审核状态
                    inventory.ForEach(x => x.IsReviewed = true);
                    // 更新审核时间
                    inventory.ForEach(x => x.ReviewDate = DateTime.Now);
                    // 批量更新
                    var list = await allocateRepository.UpdateAsync(mapper.Map<Allocate>(inventory));

                    db.CommitTran();//提交事务
                    // 返回结果
                    return list > 0 ? ApiResult.Success(ResultCode.Ok) : ApiResult.Fail(ResultCode.Fail, "审核失败");
                }
                catch (Exception)
                {
                    db.RollbackTran();
                    throw;
                }
        }
 
        
        public Task<ApiResult> UpdateAllocateAsync(CreateUpdateInventoryDto dto)
        {
            throw new NotImplementedException();
        }
        public Task<ApiResult<ApiPaging<List<AllocateDetailDto>>>> UpdateAllocateDetailAsync(CreateUpdateAllocateDetailDto dto)
        {
            throw new NotImplementedException();
        }

    }
}
