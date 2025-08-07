using AutoMapper;
using Dm;
using MicroServices.Application.IService;
using MicroServices.Domain.Inventory;
using MicroServices.Models.Dtos.AllocateAndInventory.Inventory;
using MicroServices.Repository.IRepository.I_RBAC_Repository;
using MicroServices.Repository.Repository.RBAC_Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MricoServices.Domain.RBAC;
using MricoServices.Repository.IRepository;
using MricoServices.Shared.ApiResult;
using SqlSugar;
using SqlSugar.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MicroServices.Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IConfiguration configuration; 
        private readonly  IBaseRepository<Inventory>  inventoryRepository;
        private readonly  IBaseRepository<InventoryDetail> inventoryDetailRepository;
        private readonly IMapper mapper;

        public InventoryService(IBaseRepository<Inventory> inventoryRepository, IMapper mapper, IConfiguration configuration)
        {
            this.inventoryRepository = inventoryRepository;
            this.mapper = mapper;
            this.configuration = configuration;
            this.inventoryDetailRepository = inventoryDetailRepository;
        }
        /// <summary>
        /// 添加盘点
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> CreateInventoryAsync(CreateUpdateInventoryDto dto)
        {
            try
            {
                //添加盘点
                var list = await inventoryRepository.AddAsync(mapper.Map<Inventory>(dto)); 
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
        /// 软删除盘点
        /// </summary>
        public async Task<ApiResult> DeleteInventoryAsync(int inventoryId)
        {
            try
            {
                //获取盘点信息
                var inventory = await inventoryRepository.SoftDeleteAsync(inventoryId);
                return inventory >0
                    ? ApiResult.Success(ResultCode.Ok)
                    : ApiResult.Fail(ResultCode.Fail, "删除盘点失败");
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 获取盘点信息
        /// </summary>
        public async Task<ApiResult<ApiPaging<List<InventoryDto>>>>   GetInventoryAsync(Search search)
        {
            try
            {
                //获取盘点信息
                var list =   inventoryRepository.GetAll();
                if (!string.IsNullOrEmpty(search.number))
                {
                    list = list.Where(d => d.Number.Contains(search.number));
                }
                if (search.state>0)
                {
                    list = list.Where(d => d.State==search.state);
                }
                //if (search.date != null&& search.date>)
                //{
                //        DateTime start = search.date.ObjToDate().AddDays(-1).AddMilliseconds(1);
                //        DateTime end = search.date.ObjToDate().AddDays(1).AddMilliseconds(-1);
                //        list = list.Where(d => d.InventoryDate >= start&& d.InventoryDate >= search.date.ObjToDate());
                //} 
                var totalCount = await list.CountAsync();
                var totalPage = (int)Math.Ceiling(await list.CountAsync() * 1.0 / search.PageSize);
                var page = await list.OrderByDescending(d => d.CreatedAt).Skip((search.PageIndex - 1) * search.PageSize).Take(search.PageSize).ToListAsync();

                var data = mapper.Map<List<InventoryDto>>(page);

                // 封装为 ApiPaging 对象
                var apiPagingData = new ApiPaging<List<InventoryDto>>
                {
                    TotalCount = totalCount,
                    TotalPage = totalPage,
                    Data = data
                };

                // 返回成功的 ApiResult
                return ApiResult<ApiPaging<List<InventoryDto>>>.Success(ResultCode.Ok, apiPagingData);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 反填盘点民明细
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
       
        public Task<ApiResult<List<InventoryDetailDto>>> GetInventoryDetailOneAsync(int Id)
        {
            try
            {
                var inventoryDetail = inventoryDetailRepository.GetAll().Where(x => x.InventoryId == Id).ToList();
                var dto = mapper.Map<List<InventoryDetailDto>>(inventoryDetail);
                return Task.FromResult(ApiResult<List<InventoryDetailDto>>.Success(ResultCode.Ok, dto));
            }
            catch (Exception)
            {

                throw;
            } 
        }
        /// <summary>
        /// 反填盘点
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApiResult<InventoryDto>> GetInventoryOneAsync(int Id)
        {
            try
            {
                //获取盘点信息
                var inventory = await inventoryRepository.GetAll().FirstAsync(x => x.Id==Id); 
                //获取盘点明细
                return inventory != null
                ? ApiResult<InventoryDto>.Success(ResultCode.Ok, mapper.Map<InventoryDto>(inventory)) : ApiResult<InventoryDto>.Fail(ResultCode.Fail, "获取盘点信息失败"); 
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 审核盘点
        /// </summary>
        public async Task<ApiResult> ReviewInventoryAsync(List<int> Ids)
        {
            // 参数验证
            if (Ids == null || !Ids.Any())
            {
                return ApiResult.Fail(ResultCode.Fail, "请选择要审核的库存");
            } 
            //事务
            using (var db=new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = configuration.GetConnectionString("DefaultConnection"),
                DbType = DbType.PostgreSQL,
                IsAutoCloseConnection = true
            }))   
            try
            {
                    db.BeginTran();//开始事务

                    // 获取所有待审核的库存
                    var inventory = await inventoryRepository.GetAll().Where(x => Ids.Contains(x.Id)).ToListAsync();
                    // 更新库存审核状态
                    inventory.ForEach(x => x.IsReviewed = true);
                    // 更新审核时间
                     inventory.ForEach(x => x.ReviewersDate = DateTime.Now);
                    // 批量更新
                    var list = await inventoryRepository.UpdateAsync(mapper.Map<Inventory>(inventory));

                    db.CommitTran();//提交事务
                    // 返回结果
                    return list>0? ApiResult.Success(ResultCode.Ok) : ApiResult.Fail(ResultCode.Fail, "审核失败");
            }
            catch (Exception)
            {
                    db.RollbackTran();
                    throw;
            }
        } 
        /// <summary>
        /// 修改盘点
        /// </summary>
        public async Task<ApiResult> UpdateInventoryAsync(CreateUpdateInventoryDto dto)
        {
            try
            {
                //修改盘点
                var list = await inventoryRepository.UpdateAsync(mapper.Map<Inventory>(dto));
                //修改盘点
                return list > 0
                    ? ApiResult.Success(ResultCode.Ok)
                    : ApiResult.Fail(ResultCode.Fail, "修改失败");
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
