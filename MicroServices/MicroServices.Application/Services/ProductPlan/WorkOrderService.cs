using AutoMapper;
using MicroServices.Application.IService.ProductPlan;
using MicroServices.Domain.Bom;
using MicroServices.Domain.Materials;
using MicroServices.Domain.Product_Plan;
using MicroServices.Domain.Product_Plan;
using MicroServices.Models.Dtos.Product_PlanDtos;
using MicroServices.Repository.IRepository.I_Material_Repository;
using MicroServices.Repository.IRepository.I_Product_Repository;
using MricoServices.Repository.IRepository;
using MricoServices.Shared.ApiResult;
using static MicroServices.Models.Dtos.Product_PlanDtos.SearchWorkOrderDtos;

namespace MicroServices.Application.Services.Product_Plan
{
    public class WorkOrderService : IWorkOrderService
    {
        private readonly IBaseRepository<WorkOrder> workorderrepository;
        private readonly IBaseRepository<ProductPlan> productPlanRepository;
        private readonly IBaseRepository<WorkOrderTasks> workOrderTasksRepository;
        private readonly IBaseRepository<BOM> bomrepository;
        private readonly IProductRepository productrepository;
        private readonly IUniteRepository uniteRepository;
        private readonly IBaseRepository<TypeInfos> typeInfosRepository;
        private readonly IMapper mapper;

        public WorkOrderService(IBaseRepository<WorkOrder> workorderrepository, IBaseRepository<ProductPlan> productPlanRepository, IBaseRepository<WorkOrderTasks> workOrderTasksRepository, IBaseRepository<BOM> bomrepository, IProductRepository productrepository, IUniteRepository uniteRepository, IBaseRepository<TypeInfos> typeInfosRepository, IMapper mapper)
        {
            this.workorderrepository = workorderrepository;
            this.productPlanRepository = productPlanRepository;
            this.workOrderTasksRepository = workOrderTasksRepository;
            this.bomrepository = bomrepository;
            this.productrepository = productrepository;
            this.uniteRepository = uniteRepository;
            this.typeInfosRepository = typeInfosRepository;
            this.mapper = mapper;
        }



        /// <summary>
        /// 获取所有生产计划（不带分页，请谨慎使用在大数据量场景）
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<List<WorkOrderDtos>>> GetAllWorkOrderServiceAsync()
        {
            try
            {
                var workOrders = await workorderrepository.GetAll().ToListAsync();
                var workOrderDtos = mapper.Map<List<WorkOrderDtos>>(workOrders);

                // 获取所有相关的计划ID
                var planIds = workOrders.Where(w => w.PlanId.HasValue).Select(w => w.PlanId.Value).Distinct().ToList();
                
                // 批量获取生产计划
                var productPlans = await productPlanRepository.GetAll()
                    .Where(p => planIds.Contains(p.Id))
                    .ToListAsync();

                // 填充生产计划相关信息
                foreach (var dto in workOrderDtos)
                {
                    if (dto.PlanId != null)
                    {
                        var plan = productPlans.FirstOrDefault(p => p.Id == dto.PlanId);
                        if (plan != null)
                        {
                            dto.PlannName = plan.Plan_Name;
                            dto.ProductName = plan.ProductName;
                            dto.ProductNumber = plan.Product_Id;
                            dto.SpecificationModel = plan.SpecificationModel;
                            dto.FinishedProduceType = plan.PoductType;
                            dto.Unit = plan.Unit;
                            dto.NeedTime = plan.NeedTime ?? DateTime.MinValue;
                            dto.PlanNums = plan.PlanNums ?? 0;
                        }
                    }
                }
                
                return ApiResult<List<WorkOrderDtos>>.Success(ResultCode.Ok, workOrderDtos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"获取所有生产计划时发生异常: {ex.Message}");
                return ApiResult<List<WorkOrderDtos>>.Fail(ResultCode.Fail, "获取所有生产计划时发生内部错误。");
            }
        }
        /// <summary>
        /// 根据ID获取单个生产计划
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResult<WorkOrderDtos>> GetWorkOrderServiceByIdAsync(int id)
        {
            try
            {
                var workOrder = await workorderrepository.GetByIdAsync(id);
                if (workOrder == null)
                {
                    return ApiResult<WorkOrderDtos>.Fail(ResultCode.Fail, "该生产计划不存在");
                }
                
                var workOrderDto = mapper.Map<WorkOrderDtos>(workOrder);
                
                // 如果有关联的计划ID，获取并填充计划信息
                if (workOrder.PlanId.HasValue)
                {
                    var plan = await productPlanRepository.GetByIdAsync(workOrder.PlanId.Value);
                    if (plan != null)
                    {
                        workOrderDto.PlannName = plan.Plan_Name;
                        workOrderDto.ProductName = plan.ProductName;
                        workOrderDto.ProductNumber = plan.Product_Id;
                        workOrderDto.SpecificationModel = plan.SpecificationModel;
                        workOrderDto.FinishedProduceType = plan.PoductType;
                        workOrderDto.Unit = plan.Unit;
                        workOrderDto.NeedTime = plan.NeedTime ?? DateTime.MinValue;
                        workOrderDto.PlanNums = plan.PlanNums ?? 0;
                    }
                }
                
                return ApiResult<WorkOrderDtos>.Success(ResultCode.Ok, workOrderDto);
            }
            catch (Exception ex)
            {
                // 可以在这里记录异常日志
                Console.WriteLine($"根据ID获取生产计划时发生异常: {ex.Message}");
                return ApiResult<WorkOrderDtos>.Fail(ResultCode.Fail, "获取生产计划时发生内部错误。");
            }
        }
        /// <summary>
        /// 软删除生产计划
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResult<int>> DeleteWorkOrderServiceAsync(int id)
        {
            try
            {
                var exists = await workorderrepository.GetAll().AnyAsync(wo => wo.Id == id);
                if (!exists)
                {
                    return ApiResult<int>.Fail(ResultCode.Fail, "该生产计划不存在");
                }

                var result = await workorderrepository.SoftDeleteAsync(id);
                return result > 0
                    ? ApiResult<int>.Success(ResultCode.Ok, result) // 通常返回受影响的行数
                    : ApiResult<int>.Fail(ResultCode.Fail, "生产计划删除失败");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"删除生产计划时发生异常: {ex.Message}");
                throw; // 重新抛出异常，让上层处理
            }
        }
        /// <summary>
        /// 更新生产计划
        /// </summary>
        /// <param name="updateWorkOrderDtos"></param>
        /// <returns></returns>
        public async Task<ApiResult<WorkOrder>> UpdateWorkOrderServiceAsync(int id, CreateUpdateWorkOrderDtos updateWorkOrderDtos)
        {
            try
            {
                // 1. 根据传入的ID获取现有工单实体
                var workOrder = await workorderrepository.GetByIdAsync(id);
                if (workOrder == null)
                {
                    return ApiResult<WorkOrder>.Fail(ResultCode.Fail, "该生产计划不存在");
                }

                // 2. 检查更新后的生产计划编号是否与其他计划冲突（排除自身）
                // 假设 WorkOrder 中有 OrderNumber 字段，用于唯一性校验
                if (await workorderrepository.GetAll().AnyAsync(wo => wo.OrderNumber == updateWorkOrderDtos.OrderNumber && wo.Id != id))
                {
                    return ApiResult<WorkOrder>.Fail(ResultCode.Fail, "该生产计划编号已存在");
                }

                // 3. 使用 AutoMapper 将 DTO 的值映射到现有实体上
                // 这会将 updateWorkOrderDtos 中的属性值更新到 workOrder 对象上
                mapper.Map(updateWorkOrderDtos, workOrder);

                // 4. 执行更新操作
                var updateSuccess = await workorderrepository.UpdateAsync(workOrder);

                // 5. 根据更新结果返回成功或失败信息
                return updateSuccess > 0
                    ? ApiResult<WorkOrder>.Success(ResultCode.Ok, mapper.Map<WorkOrder>(workOrder)) // 成功时返回更新后的 DTO
                    : ApiResult<WorkOrder>.Fail(ResultCode.Fail, "生产计划更新失败");
            }
            catch (Exception ex)
            {
                // 捕获并记录异常，然后返回一个通用的错误信息
                Console.WriteLine($"更新生产计划时发生异常: {ex.Message}");
                return ApiResult<WorkOrder>.Fail(ResultCode.Fail, "更新生产计划时发生内部错误。");
            }
        }
        /// <summary>
        /// 分页获取生产计划列表
        /// </summary>
        /// <param name="searchWorkOrderDtos"></param>
        /// <returns></returns>
        public async Task<ApiResult<ApiPaging<List<WorkOrderDtos>>>> PagingWorkOrderServiceAsync(SearchWorkOrderDtos searchWorkOrderDtos)
        {
            var query = workorderrepository.GetAll();

            // 根据生产计划编号进行筛选（假设 SearchWorkOrderDtos 中有 WorkOrderNumber 字段）
            if (!string.IsNullOrEmpty(searchWorkOrderDtos.OrderNumber))
            {
                query = query.Where(wo => wo.OrderNumber.Contains(searchWorkOrderDtos.OrderNumber));
            }

            // 可以添加其他筛选条件，例如按状态筛选
            if (searchWorkOrderDtos.Status.HasValue) // 假设 SearchWorkOrderDtos 中有 Status 字段
            {
                query = query.Where(wo => wo.Status == searchWorkOrderDtos.Status.Value);
            }

            var totalCount = await query.CountAsync();
            // 确保 PageSize 大于 0，避免除以零错误
            var totalPage = (int)Math.Ceiling(totalCount * 1.0 / (searchWorkOrderDtos.PageSize > 0 ? searchWorkOrderDtos.PageSize : 1));

            var pageData = await query.OrderByDescending(wo => wo.CreatedAt) // 假设 WorkOrder 实体有 CreatedAt 字段用于排序
                                      .Skip((searchWorkOrderDtos.PageIndex - 1) * searchWorkOrderDtos.PageSize)
                                      .Take(searchWorkOrderDtos.PageSize)
                                      .ToListAsync();

            var data = mapper.Map<List<WorkOrderDtos>>(pageData);

            // 获取所有相关的计划ID
            var planIds = pageData.Where(w => w.PlanId.HasValue).Select(w => w.PlanId.Value).Distinct().ToList();

            // 批量获取生产计划
            var productPlans = await productPlanRepository.GetAll()
                .Where(p => planIds.Contains(p.Id))
                .ToListAsync();

            // 填充生产计划相关信息
            foreach (var dto in data)
            {
                if (dto.PlanId != null)
                {
                    var plan = productPlans.FirstOrDefault(p => p.Id == dto.PlanId);
                    if (plan != null)
                    {
                        dto.PlannName = plan.Plan_Name;
                        dto.ProductName = plan.ProductName;
                        dto.ProductNumber = plan.Product_Id;
                        dto.SpecificationModel = plan.SpecificationModel;
                        dto.FinishedProduceType = plan.PoductType;
                        dto.Unit = plan.Unit;
                        dto.NeedTime = plan.NeedTime ?? DateTime.MinValue;
                        dto.PlanNums = plan.PlanNums ?? 0;
                    }
                }
            }

            var apiPagingData = new ApiPaging<List<WorkOrderDtos>>
            {
                TotalCount = totalCount,
                TotalPage = totalPage,
                Data = data
            };

            return ApiResult<ApiPaging<List<WorkOrderDtos>>>.Success(ResultCode.Ok, apiPagingData);
        }
        public async Task<ApiResult<ApiPaging<List<WorkOrderDtos>>>> GetAllWorkOrderServiceAsync(SearchWorkOrderDto search)
        {
            try
            {
                var query = workorderrepository.GetAll();

                // 根据搜索条件进行过滤
                if (!string.IsNullOrEmpty(search.OrderNumber))
                {
                    query = query.Where(d => d.OrderNumber.Contains(search.OrderNumber));
                }
                if (!string.IsNullOrEmpty(search.OrderName))
                {
                    query = query.Where(d => d.OrderName.Contains(search.OrderName));
                }
                if (search.Status.HasValue)
                {
                    query = query.Where(d => d.Status == search.Status.Value);
                }

                var totalCount = await query.CountAsync();
                var totalPage = (int)Math.Ceiling(totalCount * 1.0 / search.PageSize);

                var pagedData = await query.OrderByDescending(d => d.CreatedAt)
                                         .Skip((search.PageIndex - 1) * search.PageSize)
                                         .Take(search.PageSize)
                                         .ToListAsync();

                var data = mapper.Map<List<WorkOrderDtos>>(pagedData);

                // 补充关联数据
                foreach (var item in data)
                {
                    // 获取计划信息
                    if (item.PlanId > 0)
                    {
                        var plan = await productPlanRepository.GetByIdAsync(item.PlanId);
                        if (plan != null)
                        {
                            item.PlannName = plan.Plan_Name;
                            item.NeedTime = plan.NeedTime ?? DateTime.Now;
                            item.PlanNums = plan.PlanNums ?? 0;
                            item.PlanStartTime = plan.StartTime;
                            item.PlanEndTime = plan.EndTime;

                            // 获取产品信息
                            if (plan.ProductId.HasValue)
                            {
                                var product = await productrepository.GetByIdAsync(plan.ProductId.Value);
                                if (product != null)
                                {
                                    item.ProductName = product.ProductName;
                                    item.ProductNumber = product.ProductCode;
                                    item.SpecificationModel = product.Specification;

                                    // 获取产品类型名称
                                    var typeInfos = await typeInfosRepository.GetByIdAsync(product.ProductType);
                                    item.FinishedProduceType = typeInfos?.TypeName ?? product.ProductType.ToString();

                                    // 获取单位名称
                                    var unite = await uniteRepository.GetByIdAsync(product.Unit);
                                    item.Unit = unite?.UniteName ?? product.Unit.ToString();
                                }
                            }
                        }
                    }

                    // 设置默认值
                    item.FactProduceNums = 0; // 实际生产数量需要从其他表获取
                    item.FactStartTime = DateTime.Now; // 实际开工时间需要从其他表获取
                    item.FactFinishTime = DateTime.Now; // 实际完工时间需要从其他表获取
                }

                // 在内存中进行计划名称和产品名称的搜索过滤
                if (!string.IsNullOrEmpty(search.PlanName))
                {
                    data = data.Where(x => !string.IsNullOrEmpty(x.PlannName) && x.PlannName.Contains(search.PlanName)).ToList();
                }
                if (!string.IsNullOrEmpty(search.ProductName))
                {
                    data = data.Where(x => !string.IsNullOrEmpty(x.ProductName) && x.ProductName.Contains(search.ProductName)).ToList();
                }

                var apiPagingData = new ApiPaging<List<WorkOrderDtos>>
                {
                    TotalCount = totalCount,
                    TotalPage = totalPage,
                    Data = data
                };

                return ApiResult<ApiPaging<List<WorkOrderDtos>>>.Success(ResultCode.Ok, apiPagingData);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 更新生产工单状态并创建工单任务
        /// </summary>
        /// <param name="id">工单ID</param>
        /// <param name="status">要更新的状态</param>
        /// <param name="createUpdateWorkOrderTasksDtos">工单任务数据</param>
        /// <returns>更新结果</returns>
        public async Task<ApiResult<WorkOrder>> UpdateWorkOrderStatus(int id, int status, CreateUpdateWorkOrderTasksDtos createUpdateWorkOrderTasksDtos)
        {
            try
            {
                // 第一部分：查找并更新工单状态
                var workOrder = await workorderrepository.GetByIdAsync(id);
                if (workOrder == null)
                {
                    return ApiResult<WorkOrder>.Fail(ResultCode.Fail, $"未找到ID为{id}的生产工单");
                }

                // 更新工单状态
                workOrder.Status = status;
                var updateResult = await workorderrepository.UpdateAsync(workOrder);
                
                if (updateResult <= 0)
                {
                    return ApiResult<WorkOrder>.Fail(ResultCode.Fail, "更新生产工单状态失败");
                }

                // 自动生成唯一工序编号
                string worktaskCode;
                bool exists;
                var datePart = DateTime.Now.ToString("yyyyMMdd");
                var prefix = "GD" + datePart;
                var random = new Random();
                int tryCount = 0;
                do
                {
                    var randomNumber = random.Next(0, 10000).ToString("D4");
                    worktaskCode = prefix + randomNumber;
                    // 判断编号是否已存在
                    exists = await workOrderTasksRepository.GetAll()
                        .AnyAsync(r => r.TaskNumber== worktaskCode);
                    tryCount++;
                    if (tryCount > 20)
                        throw new Exception("生成唯一工序编号失败，请重试。");
                } while (exists);

                // 第二部分：创建工单任务
                var workOrderTask = new WorkOrderTasks
                {
                    WorkOrderId = workOrder.Id,
                    TaskNumber = worktaskCode,
                    TaskName = workOrder.OrderName+"工单",
                    SiteName= "站点一",
                    PlanNums = createUpdateWorkOrderTasksDtos.PlanNums,
                    PlanStartTime = createUpdateWorkOrderTasksDtos.PlanStartTime,
                    PlanFinishTime = createUpdateWorkOrderTasksDtos.PlanFinishTime,
                    NeedTime = createUpdateWorkOrderTasksDtos.NeedTime,
                    Status = createUpdateWorkOrderTasksDtos.Status ?? 1, // 默认为1（未开始）
                    Remark = createUpdateWorkOrderTasksDtos.Remark
                };
                workOrderTask.PlanProductLong = (workOrderTask.PlanFinishTime - workOrderTask.PlanStartTime).ToString();
                //workOrderTask.TaskNumber ="任务"+ workOrder.OrderName;

                var insertResult = await workOrderTasksRepository.AddAsync(workOrderTask);
                
                if (insertResult <= 0)
                {
                    // 如果工单任务创建失败，考虑回滚工单状态更新（如果有事务支持）
                    return ApiResult<WorkOrder>.Fail(ResultCode.Fail, "工单状态已更新，但创建工单任务失败");
                }

                return ApiResult<WorkOrder>.Success(ResultCode.Ok, workOrder);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"更新工单状态并创建工单任务时发生异常: {ex.Message}");
                return ApiResult<WorkOrder>.Fail(ResultCode.Fail, $"处理请求时发生错误: {ex.Message}");
            }
        }

        public async Task<ApiResult<List<ProductionSchedulingDto>>> ProductionScheduling(int id)
        {
            try
            {
                //根据工单id查询工单信息
                var workorder = await workorderrepository.GetByIdAsync(id);
                
                if (workorder == null)
                {
                    return ApiResult<List<ProductionSchedulingDto>>.Fail(ResultCode.Fail, "工单不存在");
                }

                // 手动构建 ProductionSchedulingDto
                var productionSchedulingDto = new ProductionSchedulingDto
                {
                    Id = workorder.Id,
                    OrderNumber = workorder.OrderNumber ?? "",
                    OrderName = workorder.OrderName ?? "",
                    PlanId = workorder.PlanId ?? 0,
                    PlanName = "",
                    ProductName = "",
                    ProductNumber = "",
                    SpecificationModel = "",
                    FinishedProduceType = "",
                    Unit = "",
                    BomId = 0,
                    BomCode = "",
                    BomVersion = "",
                    ProcessRouteId = 0
                };

                // 获取计划信息
                if (workorder.PlanId.HasValue)
                {
                    var plan = await productPlanRepository.GetByIdAsync(workorder.PlanId.Value);
                    if (plan != null)
                    {
                        productionSchedulingDto.PlanName = plan.Plan_Name;
                        
                        // 从计划中获取产品信息
                        if (plan.ProductId.HasValue)
                        {
                            var product = await productrepository.GetByIdAsync(plan.ProductId.Value);
                            if (product != null)
                            {
                                productionSchedulingDto.ProductName = product.ProductName;
                                productionSchedulingDto.ProductNumber = product.ProductCode;
                                productionSchedulingDto.SpecificationModel = product.Specification ?? "";
                                
                                // 获取产品类型名称
                                var typeInfos = await typeInfosRepository.GetByIdAsync(product.ProductType);
                                productionSchedulingDto.FinishedProduceType = typeInfos?.TypeName ?? product.ProductType.ToString();
                                
                                // 获取单位名称
                                var unite = await uniteRepository.GetByIdAsync(product.Unit);
                                productionSchedulingDto.Unit = unite?.UniteName ?? product.Unit.ToString();
                            }
                        }
                        
                        // 从计划中获取BOM信息
                        if (plan.BomId.HasValue)
                        {
                            var bom = await bomrepository.GetByIdAsync(plan.BomId.Value);
                            if (bom != null)
                            {
                                productionSchedulingDto.BomId = bom.Id;
                                productionSchedulingDto.BomCode = bom.BomCode;
                                productionSchedulingDto.BomVersion = bom.BomVersion;
                                productionSchedulingDto.ProcessRouteId = bom.ProcessRouteId;
                            }
                        }
                    }
                }

                var workorderList = new List<ProductionSchedulingDto> { productionSchedulingDto };

                return ApiResult<List<ProductionSchedulingDto>>.Success(ResultCode.Ok, workorderList);
            }
            catch (Exception ex)
            {
                return ApiResult<List<ProductionSchedulingDto>>.Fail(ResultCode.Fail, $"获取排产信息失败: {ex.Message}");
            }
        }
    }
}

