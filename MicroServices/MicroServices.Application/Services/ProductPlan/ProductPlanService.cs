using AutoMapper;
using MicroServices.Application.IService.Bom;
using MicroServices.Application.IService.ProductPlan;
using MicroServices.Domain.Product_Plan;
using MicroServices.Models.Dtos.Bom;
using MicroServices.Models.Dtos.Product_PlanDtos;
using MicroServices.Repository.IRepository.I_Process_Repository;
using MicroServices.Repository.Repository.Process_Repository;
using MricoServices.Repository.IRepository;
using MricoServices.Shared.ApiResult;
using System.Transactions;
using Microsoft.Extensions.Logging;

namespace MicroServices.Application.Services.Product_Plan
{
    public class ProductPlanService : IProductPlanService
    {
        private readonly IBaseRepository<ProductPlan> productionPlanningRepository;
        private readonly IBaseRepository<WorkOrder> workorderrepository;
        private readonly IBaseRepository<SourceType> sourcetyperepository;
        private readonly IBomService bomService;
        private readonly IMapper mapper;
        private readonly IRabbitMQService rabbitMQService;
        private readonly ILogger<ProductPlanService> logger;

        public ProductPlanService(
            IBaseRepository<ProductPlan> productionPlanningRepository, 
            IBaseRepository<WorkOrder> workorderrepository,
            IBaseRepository<SourceType> sourcetyperepository, 
            IBomService bomService, 
            IMapper mapper,
            IRabbitMQService rabbitMQService,
            ILogger<ProductPlanService> logger)
        {
            this.productionPlanningRepository = productionPlanningRepository;
            this.workorderrepository = workorderrepository;
            this.sourcetyperepository = sourcetyperepository;
            this.bomService = bomService;
            this.mapper = mapper;
            this.rabbitMQService = rabbitMQService;
            this.logger = logger;
        }
        /// <summary>
        /// 生产计划添加
        /// </summary>
        /// <param name="createProductionPlanDto"></param>
        /// <returns></returns>
        public async Task<ApiResult> AddIProductionPlanServiceAsync(CreateUpdateProductionPlanDto createProductionPlanDto)
        {
            try
            {
                // 设置状态为1(未分解)
                createProductionPlanDto.Status = 1;

                // 自动生成唯一计划编号
                string planCode;
                bool exists;
                var datePart = DateTime.Now.ToString("yyyyMMdd");
                var prefix = "JHBH" + datePart;
                var random = new Random();
                int tryCount = 0;
                do
                {
                    var randomNumber = random.Next(0, 10000).ToString("D4");
                    planCode = prefix + randomNumber;
                    // 判断编号是否已存在
                    exists = await productionPlanningRepository.GetAll()
                        .AnyAsync(r => r.Plan_Id == planCode);
                    tryCount++;
                    if (tryCount > 20)
                        throw new Exception("生成唯一计划编号失败，请重试。");
                } while (exists);

                createProductionPlanDto.Plan_Id = planCode;

                var productionPlan = mapper.Map<ProductPlan>(createProductionPlanDto);
                var result = await productionPlanningRepository.AddAsync(productionPlan);

                if (result > 0)
                {
                    try
                    {
                        // 发布生产计划创建完成消息到RabbitMQ
                        var message = new ProductPlanCreatedMessage
                        {
                            Id = productionPlan.Id,
                            PlanId = productionPlan.Plan_Id,
                            PlanName = productionPlan.Plan_Name,
                            ProductName = productionPlan.ProductName,
                            PlanNums = productionPlan.PlanNums,
                            StartTime = productionPlan.StartTime,
                            EndTime = productionPlan.EndTime,
                            Status = productionPlan.Status,
                            Remark = productionPlan.Remark
                        };

                        var publishResult = await rabbitMQService.PublishProductPlanCreatedMessageAsync(message);
                        if (publishResult)
                        {
                            logger.LogInformation($"生产计划 {productionPlan.Plan_Id} 创建成功，RabbitMQ消息发布成功");
                        }
                        else
                        {
                            logger.LogWarning($"生产计划 {productionPlan.Plan_Id} 创建成功，但RabbitMQ消息发布失败");
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"生产计划 {productionPlan.Plan_Id} 创建成功，但RabbitMQ消息发布时发生异常");
                    }

                    return ApiResult.Success(ResultCode.Ok);
                }
                else
                {
                    return ApiResult.Fail(ResultCode.Fail, "生产计划添加失败");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"添加生产计划时发生异常: {ex.Message}");
                return ApiResult.Fail(ResultCode.Fail, "添加生产计划时发生内部错误。"); // 返回通用错误信息
            }
        }
        /// <summary>
        /// 生产计划删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResult> DeleteIProductionPlanServiceAsync(int id)
        {
            try
            {
                var exists = await productionPlanningRepository.GetAll().Where(d => d.Id == id).AnyAsync();
                if (!exists)
                {
                    return ApiResult.Fail(ResultCode.Fail, "该生产计划不存在");
                }

                var result = await productionPlanningRepository.SoftDeleteAsync(id); // 假设 IBaseRepository 提供了 SoftDeleteAsync 方法

                return result > 0
                    ? ApiResult.Success(ResultCode.Ok)
                    : ApiResult.Fail(ResultCode.Fail, "生产计划删除失败");
            }
            catch (Exception ex)
            {
                // 记录异常日志
                Console.WriteLine($"删除生产计划时发生异常: {ex.Message}");
                return ApiResult.Fail(ResultCode.Fail, "删除生产计划时发生内部错误。");
            }
        }
        /// <summary>
        /// 生产计划列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public async Task<ApiResult<ApiPaging<List<ProductPlanDto>>>> GetIProductionPlanServiceByIdAsync(Models.Dtos.Product_PlanDtos.Search search)
        {
            try
            {
                var query = productionPlanningRepository.GetAll();

                // 根据DTO中的条件进行过滤，这里假设 SearchProductionPlanDto 包含 PlanName 属性
                if (!string.IsNullOrEmpty(search.Plan_Id))
                {
                    query = query.Where(d => d.Plan_Id.Contains(search.Plan_Id));
                }
                if (!string.IsNullOrEmpty(search.Plan_Name))
                {
                    query = query.Where(d => d.Plan_Name.Contains(search.Plan_Name));
                }
                if(search.FromType != 0)
                {
                    query = query.Where(d => d.FromType == search.FromType);
                }
                if(!string.IsNullOrEmpty(search.ProductName))
                {
                    query = query.Where(d => d.ProductName == search.ProductName);
                }
                if(search.Status != 0)
                {
                    query = query.Where(d => d.Status == search.Status);
                }

                var totalCount = await query.CountAsync();
                var totalPage = (int)Math.Ceiling(totalCount * 1.0 / search.PageSize); // 使用 totalCount 而非再次 count

                var pagedData = await query.OrderByDescending(d => d.CreatedAt) // 假设 Production_Planning 有 CreatedAt 属性
                                           .Skip((search.PageIndex - 1) * search.PageSize)
                                           .Take(search.PageSize)
                                           .ToListAsync();

                var data = mapper.Map<List<ProductPlanDto>>(pagedData);

                foreach (var item in data)
                {
                    item.FromTypeName = (await sourcetyperepository.GetByIdAsync(item.FromType)).SourceTypeName;
                }

                var apiPagingData = new ApiPaging<List<ProductPlanDto>>
                {
                    TotalCount = totalCount,
                    TotalPage = totalPage,
                    Data = data
                };

                return ApiResult<ApiPaging<List<ProductPlanDto>>>.Success(ResultCode.Ok, apiPagingData);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 生产计划更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="UpdateProductionPlanDto"></param>
        /// <returns></returns>
        public async Task<ApiResult<ProductPlanDto>> UpdateIProductionPlanServiceAsync(int id, CreateUpdateProductionPlanDto UpdateProductionPlanDto)
        {
            try
            {
                var productionPlan = await productionPlanningRepository.GetByIdAsync(id);
                if (productionPlan == null)
                {
                    return ApiResult<ProductPlanDto>.Fail(ResultCode.Fail, "该生产计划不存在");
                }

                // 映射 DTO 的值到现有实体
                mapper.Map(UpdateProductionPlanDto, productionPlan);

                var updateSuccess = await productionPlanningRepository.UpdateAsync(productionPlan);

                return updateSuccess > 0
                    ? ApiResult<ProductPlanDto>.Success(ResultCode.Ok, mapper.Map<ProductPlanDto>(productionPlan))
                    : ApiResult<ProductPlanDto>.Fail(ResultCode.Fail, "生产计划更新失败");
            }
            catch (Exception ex)
            {
                // 记录异常日志
                Console.WriteLine($"更新生产计划时发生异常: {ex.Message}");
                return ApiResult<ProductPlanDto>.Fail(ResultCode.Fail, "更新生产计划时发生内部错误。");
            }
        }
        /// <summary>
        /// 分解生产计划
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApiResult> ProductionDismantle(int id)
        {
            try
            {
                using (var scope =new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //分解生产计划的方法
                    //首先根据id去查找要分解的生产计划的状态进行判断
                    var productionPlan = await productionPlanningRepository.GetByIdAsync(id);
                    //1、未分解 2、已分解 3、已撤回 4、进行中 5、已完成 6、已关闭
                    //如果是状态为未分解才能进行分解操作
                    switch (productionPlan.Status)
                    {
                        case 1: // 未分解
                            break; // 可以继续分解，后续代码继续执行
                        case 2:
                            return ApiResult.Fail(ResultCode.Fail, "该生产计划已分解");
                        case 3:
                            return ApiResult.Fail(ResultCode.Fail, "该生产计划已撤回");
                        case 4:
                            return ApiResult.Fail(ResultCode.Fail, "该生产计划正在进行中，无法分解");
                        case 5:
                            return ApiResult.Fail(ResultCode.Fail, "该生产计划已完成");
                        case 6:
                            return ApiResult.Fail(ResultCode.Fail, "该生产计划已关闭");
                        default:
                            return ApiResult.Fail(ResultCode.Fail, "未知状态，无法操作");
                    }
                    //根据生产计划的bom单id去查找相关的bom和子bom,根据查到的bom单id生成对应的工单
                    //首先获取完整的bom单结构
                    var bomResult = await bomService.GetBomTreeAsync(productionPlan.BomId);
                    if (bomResult == null)
                    {
                        return ApiResult.Fail(ResultCode.Fail, "获取BOM结构失败");
                    }
                    //提取所有BOM节点ID（包括子BOM）
                    var allBomIds = new List<int>();
                    void CollectBomIds(List<BomTreeDto> bomTree)
                    {
                        foreach (var bom in bomTree)
                        {
                            allBomIds.Add(bom.Id);
                            if (bom.Children != null && bom.Children.Any())
                            {
                                CollectBomIds(bom.Children);
                            }
                        }
                    }
                    //调用方法获取所有BOM节点ID
                    CollectBomIds(bomResult.data);


                    // 为每个BOM节点创建任务工单
                    var createdWorkOrders = new List<WorkOrder>();
                    foreach (var item in allBomIds.Distinct())
                    {
                        // 自动生成唯一工单编号
                        string workOrderCode;
                        bool exists;
                        var datePart = DateTime.Now.ToString("yyyyMMdd");
                        var prefix = "GDBH" + datePart;
                        var random = new Random();
                        int tryCount = 0;
                        do
                        {
                            var randomNumber = random.Next(0, 10000).ToString("D4");
                            workOrderCode = prefix + randomNumber;
                            // 判断编号是否已存在
                            exists = await workorderrepository.GetAll()
                                .AnyAsync(r => r.OrderNumber == workOrderCode);
                            tryCount++;
                            if (tryCount > 20)
                                throw new Exception("生成唯一工单编号失败，请重试。");
                        } while (exists);
                        var workOrder = new WorkOrder
                        {
                            OrderNumber = workOrderCode,
                            OrderName = "工单",
                            OrderProgress = "0%",
                            PlanId = id,
                            Status = 1  //1 待排产 2 已排产 3 未开始 4 进行中 5 已暂停 6 已完成 7 部分完成 8 已关闭 9 已取消
                        };
                        //新增工单
                        await workorderrepository.AddAsync(workOrder);
                    }
                    //  更新生产计划状态为已分解
                    productionPlan.Status = 2; // 已分解
                    await productionPlanningRepository.UpdateAsync(productionPlan);
                    scope.Complete();
                    return ApiResult.Success(ResultCode.Ok);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 发布生产计划创建完成消息到RabbitMQ
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <returns></returns>
        public async Task<bool> PublishProductPlanCreatedMessageAsync(ProductPlanCreatedMessage message)
        {
            try
            {
                return await rabbitMQService.PublishProductPlanCreatedMessageAsync(message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"发布生产计划创建消息失败: {message.PlanId}");
                return false;
            }
        }

        /// <summary>
        /// 辅助方法：生成一个新的唯一生产计划编号。
        /// </summary>
        /// <param name="prefix">编号前缀，如 "PP"。</param>
        /// <returns>生成的唯一生产计划编号。</returns>
        private async Task<string> GenerateUniqueProductionPlanCodeAsync(string prefix)
        {
            // 获取当前前缀下的所有生产计划编号
            // "PlanCode" 应该是你的 ProductPlan 实体中实际的编号属性名
            var existingCodes = await productionPlanningRepository.GetByPrefixWithoutSoftDelete<string>("Plan_Id", prefix)
                                                                 .Select(it => it.Plan_Id) // 选择编号字段
                                                                 .ToListAsync();

            int maxNumber = 0;
            if (existingCodes != null && existingCodes.Any())
            {
                // 从现有编号中提取数字部分并找到最大值
                maxNumber = existingCodes.Select(s => {
                    // 假设编号格式是 "PP" + 4位数字，例如 "PP0001"
                    // 提取数字部分并尝试转换
                    string numberPart = s.Replace(prefix, ""); // 移除前缀
                    int num;
                    return int.TryParse(numberPart, out num) ? num : 0;
                }).DefaultIfEmpty(0).Max(); // 如果没有有效的数字，默认为0
            }

            // 生成下一个编号
            int nextNumber = maxNumber + 1;
            return $"{prefix}{nextNumber:D4}"; // 例如 "PP0001", "PP0002" (D4表示至少4位，不足补零)
        }
    }
}
