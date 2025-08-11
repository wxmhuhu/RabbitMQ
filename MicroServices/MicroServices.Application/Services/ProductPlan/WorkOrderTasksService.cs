using AutoMapper;
using MicroServices.Application.IService.ProductPlan;
using MicroServices.Domain.Bom;
using MicroServices.Domain.FacatoryFloors;
using MicroServices.Domain.ProcessInfo;
using MicroServices.Domain.Product_Plan;
using MicroServices.Domain.Product_Plan;
using MicroServices.Models.Dtos.Product_PlanDtos;
using MricoServices.Domain.RBAC;
using MricoServices.Repository.IRepository;
using MricoServices.Shared.ApiResult;

namespace MicroServices.Application.Services.Product_Plan
{
    public class WorkOrderTasksService : IWorkOrderTasksService
    {
        private readonly IBaseRepository<WorkOrderTasks> workordertasksrepository;
        private readonly IBaseRepository<WorkOrder> workorderRep;
        private readonly IBaseRepository<ProductPlan> planRep;
        private readonly IBaseRepository<BOM> bomRep;
        private readonly IBaseRepository<ProcessRoute> processRouteRep;
        private readonly IBaseRepository<ProcessComposition> processCompositionRep; // 工序组成
        private readonly IBaseRepository<Processes> processRep; // 工序
        private readonly IBaseRepository<Team> teamRep; // 班组
        private readonly IBaseRepository<User> userRep; // 用户
        private readonly IMapper mapper;

        public WorkOrderTasksService(
            IBaseRepository<WorkOrderTasks> workordertasksrepository, 
            IMapper mapper, 
            IBaseRepository<WorkOrder> workorderRep, 
            IBaseRepository<ProductPlan> planRep, 
            IBaseRepository<BOM> bomRep, 
            IBaseRepository<ProcessRoute> processRouteRep, 
            IBaseRepository<ProcessComposition> processCompositionRep, 
            IBaseRepository<Processes> processRep,
            IBaseRepository<Team> teamRep,
            IBaseRepository<User> userRep)
        {
            this.workordertasksrepository = workordertasksrepository;
            this.mapper = mapper;
            this.workorderRep = workorderRep;
            this.planRep = planRep;
            this.bomRep = bomRep;
            this.processRouteRep = processRouteRep;
            this.processCompositionRep = processCompositionRep;
            this.processRep = processRep;
            this.teamRep = teamRep;
            this.userRep = userRep;
        }
        /// <summary>
        /// 工单任务 无分页查询
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<List<WorkOrderTasksDto>>> GetAllWorkOrderTasksServiceAsync()
        {
            try
            {
                var tasks = await workordertasksrepository.GetAll().ToListAsync();
                var dtos = mapper.Map<List<WorkOrderTasksDto>>(tasks);
                foreach (var task in dtos)
                {
                    var order = await workorderRep.GetAll().Where(x => x.Id == task.WorkOrderId).FirstAsync();
                    task.WorkOrderNumber = order.OrderNumber;
                    task.WorkOrderName = order.OrderName;
                    var productId = order.PlanId;
                    var productplan = await planRep.GetAll().FirstAsync(x => x.Id == productId);
                    var bomId = productplan.BomId;
                    var bom = await bomRep.GetAll().FirstAsync(x => x.Id == bomId) ;
                    var processRouteId = (await bomRep.GetByIdAsync(bom.Id)).ProcessRouteId;
                    var processRoute  = await processRouteRep.GetAll().FirstAsync(x=>x.Id == processRouteId);
                    var processComposition = await processCompositionRep.GetAll().Where(x => x.ProcessRouteId == processRouteId).OrderBy(x => x.SerialNumber).FirstAsync();
                    var processId = processComposition?.ProcessId;
                    var process = await processRep.GetAll().FirstAsync(x => x.Id == processId);
                    task.ProcessRoute = processRoute.ProcessRouteName;//工艺流程
                    task.ProcessName = process.ProcessName;//工序名称
                    task.ProcessNumber = process.ProcessCode;//工序编号
                }
                return ApiResult<List<WorkOrderTasksDto>>.Success(ResultCode.Ok, dtos);
            }
            catch (Exception ex)
            {
                return ApiResult<List<WorkOrderTasksDto>>.Fail(ResultCode.Fail, $"获取工单任务列表失败: {ex.Message}");
            }
        }
        /// <summary>
        /// 获取id工单任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResult<WorkOrderTasksDto>> GetWorkOrderTasksServiceByIdAsync(int id)
        {
            try
            {
                var task = await workordertasksrepository.GetByIdAsync(id);
                if (task == null)
                {
                    return ApiResult<WorkOrderTasksDto>.Fail(ResultCode.Fail, "未找到工单任务");
                }
                var dto = mapper.Map<WorkOrderTasksDto>(task);
                var order = await workorderRep.GetAll().Where(x => x.Id == task.WorkOrderId).FirstAsync();
                dto.WorkOrderNumber = order.OrderNumber;
                dto.WorkOrderName = order.OrderName;
                var productId = order.PlanId;
                var productplan = await planRep.GetAll().FirstAsync(x => x.Id == productId);
                var bomId = productplan.BomId;
                var bom = await bomRep.GetAll().FirstAsync(x => x.Id == bomId);
                var processRouteId = (await bomRep.GetByIdAsync(bom.Id)).ProcessRouteId;
                var processRoute = await processRouteRep.GetAll().FirstAsync(x => x.Id == processRouteId);
                var processComposition = await processCompositionRep.GetAll().Where(x => x.ProcessRouteId == processRouteId).OrderBy(x => x.SerialNumber).FirstAsync();
                var processId = processComposition?.ProcessId;
                var process = await processRep.GetAll().FirstAsync(x => x.Id == processId);
                dto.ProcessRoute = processRoute.ProcessRouteName;//工艺流程
                dto.ProcessName = process.ProcessName;//工序名称
                dto.ProcessNumber = process.ProcessCode;//工序编号
                return ApiResult<WorkOrderTasksDto>.Success(ResultCode.Ok, dto);
            }
            catch (Exception ex)
            {
                return ApiResult<WorkOrderTasksDto>.Fail(ResultCode.Fail, $"获取工单任务失败: {ex.Message}");
            }
        }
        /// <summary>
        /// 删除工单任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResult<int>> DeleteWorkOrderTasksServiceAsync(int id)
        {
            try
            {
                var list = await workorderRep.GetByIdAsync(id);
                return await workordertasksrepository.DeleteAsync(id) > 0 ? ApiResult<int>.Success(ResultCode.Ok, id) : ApiResult<int>.Fail(ResultCode.Fail, "删除工单任务失败");
            } 
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 派工之后修改状态已下达
        /// </summary>
        /// <param name="workTasksId">工单任务ID</param>
        /// <param name="dto">派工数据</param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateWorkOrderTasksServiceAsync(int workTasksId, CreateUpdateWorkOrderTasksDtos dto)
        {
            try
            {
                // 1. 验证工单任务是否存在
                var workerTask = await workordertasksrepository.GetByIdAsync(workTasksId);
                if (workerTask == null)
                {
                    return ApiResult.Fail(ResultCode.Fail, "工单任务不存在");
                }
                // 2. 验证状态转换的合法性
                if (workerTask.Status != 1) // 只有未派工状态才能进行派工
                {
                    return ApiResult.Fail(ResultCode.Fail, "只有未派工状态的任务才能进行派工操作");
                }
                // 7. 更新工单任务状态为已下达
                workerTask.Status = 2; // 已下达
                
                // 8. 手动更新需要修改的字段，避免ID被意外修改
               
                if (dto.ClassGroupId.HasValue)
                    workerTask.ClassGroupId = dto.ClassGroupId.Value;
                if (dto.MainPeople.HasValue)
                    workerTask.MainPeople = dto.MainPeople.Value;
                if (!string.IsNullOrEmpty(dto.DispatchRemark))
                    workerTask.DispatchRemark = dto.DispatchRemark;
                if (dto.DepartmentId.HasValue)
                    workerTask.DepartmentId = dto.DepartmentId.Value;
                if (dto.PeopleId.HasValue)
                    workerTask.PeopleId = dto.PeopleId.Value;
                if (!string.IsNullOrEmpty(dto.QualityRemark))
                    workerTask.QualityRemark = dto.QualityRemark;
                
                // 9. 保存更新
                var result = await workordertasksrepository.UpdateAsync(workerTask);
                if (result <= 0)
                {
                    return ApiResult.Fail(ResultCode.Fail, "派工失败，请检查数据完整性");
                }

                return ApiResult.Success(ResultCode.Ok);
            }
            catch (Exception ex)
            {
                // 记录详细错误信息
                return ApiResult.Fail(ResultCode.Fail, $"派工操作异常: {ex.Message}");
            }
        }
        /// <summary>
        /// 点击开工按钮
        /// </summary>
        /// <param name="workerTaskId"></param>
        /// <returns></returns>
        public async Task<ApiResult> Meugah(int workerTaskId)
        {
            var task = await workordertasksrepository.GetByIdAsync(workerTaskId);
            task.Status = 3;//进行中
            var result = await workordertasksrepository.UpdateAsync(task);
            if (result <= 0)
            {
                return ApiResult.Fail(ResultCode.Fail, "开工失败");
            }
            return ApiResult.Success(ResultCode.Ok);
        }
        /// <summary>
        /// 工单任务 带分页的显示
        /// </summary>
        /// <param name="searchWorkOrderTasksDto"></param>
        /// <returns></returns>
        public async Task<ApiResult<ApiPaging<List<WorkOrderTasksDto>>>> PageWorkOrderTasksAsync(SearchWorkOrderTasksDto searchWorkOrderTasksDto)
        {
            try
            {
                var tasks = await workordertasksrepository.GetAll().ToListAsync();
                var dtos = mapper.Map<List<WorkOrderTasksDto>>(tasks);
                foreach (var task in dtos)
                {
                    var order = await workorderRep.GetAll().Where(x => x.Id == task.WorkOrderId).FirstAsync();
                    task.WorkOrderNumber = order.OrderNumber;
                    task.WorkOrderName = order.OrderName;
                    var productId = order.PlanId;
                    var productplan = await planRep.GetAll().FirstAsync(x => x.Id == productId);
                    var bomId = productplan.BomId;
                    var bom = await bomRep.GetAll().FirstAsync(x => x.Id == bomId);
                    var processRouteId = (await bomRep.GetByIdAsync(bom.Id)).ProcessRouteId;
                    var processRoute = await processRouteRep.GetAll().FirstAsync(x => x.Id == processRouteId);
                    var processComposition = await processCompositionRep.GetAll().Where(x => x.ProcessRouteId == processRouteId).OrderBy(x => x.SerialNumber).FirstAsync();
                    var processId = processComposition?.ProcessId;
                    var process = await processRep.GetAll().FirstAsync(x => x.Id == processId);
                    task.ProcessRoute = processRoute.ProcessRouteName;//工艺流程
                    task.ProcessName = process.ProcessName;//工序名称
                    task.ProcessNumber = process.ProcessCode;//工序编号
                    
                }
                // 计算总数和总页数
                var totalCount = dtos.Count();
                var totalPage = (int)Math.Ceiling(totalCount * 1.0 / searchWorkOrderTasksDto.PageSize);
                // 分页查询数据
                var pageData = dtos.OrderByDescending(x => x.WorkOrderId).Skip((searchWorkOrderTasksDto.PageIndex - 1) * searchWorkOrderTasksDto.PageSize).Take(searchWorkOrderTasksDto.PageSize);
                //// 映射为DTO
                var resultData = mapper.Map<List<WorkOrderTasksDto>>(pageData);
                // 构建分页返回对象
                var apiPagingData = new ApiPaging<List<WorkOrderTasksDto>>
                {
                    TotalCount = totalCount,
                    TotalPage = totalPage,
                    Data = resultData
                };
                return ApiResult<ApiPaging<List<WorkOrderTasksDto>>>.Success(ResultCode.Ok, apiPagingData);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
