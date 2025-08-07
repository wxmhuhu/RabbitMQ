using AutoMapper;
using MicroServices.Application.IService.Reportworks;
using MicroServices.Domain.Bom;
using MicroServices.Domain.FacatoryFloors;
using MicroServices.Domain.Materials;
using MicroServices.Domain.ProcessInfo;
using MicroServices.Domain.Product_Plan;
using MicroServices.Domain.ProductPlan;
using MicroServices.Domain.Reportworks;
using MicroServices.Domain.Sites;
using MicroServices.Models.Dtos.Reportworks;
using MicroServices.Repository.IRepository.Reportworks;
using Microsoft.IdentityModel.Tokens;
using MricoServices.Domain.RBAC;
using MricoServices.Repository.IRepository;
using MricoServices.Shared.ApiResult;

namespace MicroServices.Application.Services.Reportworks
{
    public class ReportworkRecordService : IReportworkRecordService
    {
        private readonly IMapper mapper;
        private readonly IBaseRepository<Team> teamRep;//班组
        private readonly IBaseRepository<WorkOrder> work_OrderRep;//生产工单
        private readonly IBaseRepository<WorkOrderTasks> work_Order_TasksRep;//工单任务
        private readonly IBaseRepository<Sites> sitesRep;//站点
        private readonly IBaseRepository<ProductPlan> productPlanRep; // 生产计划
        private readonly IBaseRepository<BOM> bomRep; // BOM
        private readonly IBaseRepository<ProcessRoute> processRouteRep; // 工艺路线
        private readonly IBaseRepository<Product> productRep; // 产品
        private readonly IBaseRepository<ProcessComposition> processCompositionRep; // 工序组成
        private readonly IBaseRepository<Processes> processRep; // 工序
        private readonly IBaseRepository<User> userRep; //用户表
        private readonly IBaseRepository<FactoryFloor> factoryRep;//车间表
        private readonly IBaseRepository<ReportworkQualityInspection> reportworkQualityInspectionRep;//报工质检
        private readonly IBaseRepository<InspectionItemType> inspectionItemTypeRep;//检测项目类型 
        private readonly IBaseRepository<Unite> uniteRep;//单位
        private readonly IBaseRepository<TypeInfos> typeInfosRep;//类型
        private readonly IBaseRepository<DeptInfo> deptRep;//部门
        private readonly IReportworkRecordRepository reportworkRecordRep;//报工记录
        private readonly IBaseRepository<InspectionItem> inspectionItemRep;//检测项目
        public ReportworkRecordService(IMapper mapper, IReportworkRecordRepository reportworkRecordRep, IBaseRepository<Team> teamRep, IBaseRepository<WorkOrder> work_OrderRep, IBaseRepository<WorkOrderTasks> work_Order_TasksRep, IBaseRepository<Sites> sitesRep, IBaseRepository<ProductPlan> productPlanRep, IBaseRepository<BOM> bomRep, IBaseRepository<ProcessRoute> processRouteRep, IBaseRepository<Product> productRep, IBaseRepository<ProcessComposition> processCompositionRep, IBaseRepository<Processes> processRep, IBaseRepository<User> userRep, IBaseRepository<FactoryFloor> factoryRep, IBaseRepository<ReportworkQualityInspection> reportworkQualityInspectionRep, IBaseRepository<InspectionItem> inspectionItemRep, IBaseRepository<InspectionItemType> inspectionItemTypeRep, IBaseRepository<Unite> uniteRep, IBaseRepository<TypeInfos> typeInfosRep, IBaseRepository<DeptInfo> deptRep)
        {
            this.mapper = mapper;
            this.reportworkRecordRep = reportworkRecordRep;
            this.teamRep = teamRep;
            this.work_OrderRep = work_OrderRep;
            this.work_Order_TasksRep = work_Order_TasksRep;
            this.sitesRep = sitesRep;
            this.productPlanRep = productPlanRep;
            this.bomRep = bomRep;
            this.processRouteRep = processRouteRep;
            this.productRep = productRep;
            this.processCompositionRep = processCompositionRep;
            this.processRep = processRep;
            this.userRep = userRep;
            this.factoryRep = factoryRep;
            this.reportworkQualityInspectionRep = reportworkQualityInspectionRep;
            this.inspectionItemRep = inspectionItemRep;
            this.inspectionItemTypeRep = inspectionItemTypeRep;
            this.uniteRep = uniteRep;
            this.typeInfosRep = typeInfosRep;
            this.deptRep = deptRep;
        }
        /// <summary>
        /// 报工记录列表分页查询
        /// </summary>
        /// <param name="searchReportworkRecordDto">报工记录查询dto</param>
        /// <returns>返回报工记录列表分页查询</returns>
        public async Task<ApiResult<ApiPaging<List<ReportworkRecordDto>>>> GetAllReportworkRecordAsync(SearchReportworkRecordDto searchReportworkRecordDto)
        {
            try
            {
                #region
                // 获取所有报工记录
                var list = reportworkRecordRep.GetAll().Where(x=>!x.IsDeleted);
                var query = list.ToList();
                // 根据查询条件动态拼接过滤条件
                if (!string.IsNullOrEmpty(searchReportworkRecordDto.TaskNumber) || !string.IsNullOrEmpty(searchReportworkRecordDto.TaskName))
                {
                    if(searchReportworkRecordDto.TaskNumber != null)
                    {
                        query = query.Where(x => x.TaskNumber.Contains(searchReportworkRecordDto.TaskNumber)).ToList();
                    }
                    else
                    {
                        query = query.Where(x => x.TaskName.Contains(searchReportworkRecordDto.TaskName)).ToList();
                    }
                }
                if (!string.IsNullOrEmpty(searchReportworkRecordDto.OrderNumber) || !string.IsNullOrEmpty(searchReportworkRecordDto.OrderName))
                {
                    if (searchReportworkRecordDto.OrderNumber != null)
                    {
                        query = query.Where(x => x.OrderNumber.Contains(searchReportworkRecordDto.OrderNumber)).ToList();
                    }
                    else
                    {
                        query = query.Where(x => x.OrderName.Contains(searchReportworkRecordDto.OrderName)).ToList();
                    }
                }
                if (!string.IsNullOrEmpty(searchReportworkRecordDto.ReportingPerson))
                {
                    query = query.Where(x => x.ReportingPerson.Contains(searchReportworkRecordDto.ReportingPerson)).ToList();
                }

                if (searchReportworkRecordDto.Status != null)
                {
                    query = query.Where(x => x.Status == searchReportworkRecordDto.Status).ToList();
                }

                if (searchReportworkRecordDto.TeamId != null)
                {
                    query = query.Where(x => x.TeamId == searchReportworkRecordDto.TeamId).ToList();
                }
                if (!string.IsNullOrEmpty(searchReportworkRecordDto.ReportingDate))
                {
                    query = query.Where(x => x.ReportingDate.Date == DateTime.Parse(searchReportworkRecordDto.ReportingDate)).ToList();
                }
                if (!string.IsNullOrEmpty(searchReportworkRecordDto.QualitystartDate))
                {
                    query = query.Where(x => x.QualityDate >= DateTime.Parse(searchReportworkRecordDto.QualitystartDate)).ToList();
                }
                if (!string.IsNullOrEmpty(searchReportworkRecordDto.QualityendDate))
                {
                    query = query.Where(x => x.QualityDate < DateTime.Parse(searchReportworkRecordDto.QualityendDate)).ToList();
                }
                if (searchReportworkRecordDto.ProcessRouteId != null)
                {
                    query = query.Where(x => x.ProcessRouteId == searchReportworkRecordDto.ProcessRouteId).ToList();
                }

                if (searchReportworkRecordDto.ProcessId != null )
                {
                    query = query.Where(x => x.ProcessId == searchReportworkRecordDto.ProcessId).ToList();
                }

                if (searchReportworkRecordDto.Director != null)
                {
                    query = query.Where(x => x.Director == searchReportworkRecordDto.Director).ToList();
                }

                if (!string.IsNullOrEmpty(searchReportworkRecordDto.InspectionResult))
                {
                    query = query.Where(x => x.InspectionResult.Contains(searchReportworkRecordDto.InspectionResult)).ToList();
                }
                if (searchReportworkRecordDto.SiteId != null)
                {
                    query = query.Where(x => x.SiteId == searchReportworkRecordDto.SiteId).ToList();
                }
                #endregion
                // 计算总数和总页数
                var totalCount =  query.Count();
                var totalPage = (int)Math.Ceiling(totalCount * 1.0 / searchReportworkRecordDto.PageSize);
                // 分页查询数据
                var pageData =  query.OrderByDescending(x => x.Id).Skip((searchReportworkRecordDto.PageIndex - 1) * searchReportworkRecordDto.PageSize).Take(searchReportworkRecordDto.PageSize);
                //// 映射为DTO
                var resultData = mapper.Map<List<ReportworkRecordDto>>(pageData);
                // 构建分页返回对象
                var apiPagingData = new ApiPaging<List<ReportworkRecordDto>>
                {
                    TotalCount = totalCount,
                    TotalPage = totalPage,
                    Data = resultData
                };

                // 返回成功结果
                return ApiResult<ApiPaging<List<ReportworkRecordDto>>>.Success(ResultCode.Ok, apiPagingData);
            }
            catch (Exception ex)
            {
                // 记录日志（实际项目中应添加日志记录）
                // logger.LogError(ex, "获取报工记录列表时发生异常");
                // 返回错误结果
                throw;
            }
        }
        /// <summary>
        /// 报工记录的软删除
        /// </summary>
        /// <param name="reportworkRecordid">报工记录id</param>
        /// <returns>返回受影响行数</returns>
        public async Task<ApiResult> DeleteReportworkRecordAsync(int reportworkRecordid)
        {
            try
            {
                var list = await reportworkRecordRep.GetAll().Where(d => d.Id == reportworkRecordid).AnyAsync();
                if (!list)
                {
                    return ApiResult.Fail(ResultCode.Fail, "该记录不存在");
                }
                var result = await reportworkRecordRep.SoftDeleteAsync(reportworkRecordid);
                return result > 0
                    ? ApiResult.Success(ResultCode.Ok)
                    : ApiResult.Fail(ResultCode.Fail, "记录删除失败");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        /// <summary>
        /// 根据工单任务同时生成报工记录和报工质检
        /// </summary>
        /// <param name="dto">包含工单任务id及前端传参字段的dto</param>
        /// <returns>返回操作结果</returns>
        public async Task<ApiResult> GenerateReportworkRecordByWorkOrderTaskAsync(int workerorderId,ReportworkRecordAndReportworkQualityInspectionDto dto)
        {
            try
            {

                // 1. 获取工单任务
                var workOrderTask = await work_Order_TasksRep.GetAll().FirstAsync(x => x.Id == workerorderId);
                if (workOrderTask == null)
                {
                    return ApiResult.Fail(ResultCode.Fail, "工单任务不存在");
                }
                // 2. 获取工单
                var workOrder =  await work_OrderRep.GetAll().FirstAsync(x => x.Id == workOrderTask.WorkOrderId.Value);
                if (workOrder == null)
                { 
                    return ApiResult.Fail(ResultCode.Fail, "工单不存在"); 
                }

                // 3. 获取生产计划
                var productPlan = await productPlanRep.GetAll().FirstAsync(x => x.Id == workOrder.PlanId.Value);
                if (productPlan == null)
                { 
                    return ApiResult.Fail(ResultCode.Fail, "生产计划不存在");
                }
                // 4. 获取BOM
                var bom = await bomRep.GetAll().FirstAsync(x => x.Id == productPlan.BomId.Value) ;
                if (bom == null)
                {
                    return ApiResult.Fail(ResultCode.Fail, "BOM不存在");
                }

                // 5. 获取工艺路线Id、产品Id
                var processRouteId = (await bomRep.GetByIdAsync(bom.Id)).ProcessRouteId;
                var processRoute = await processRouteRep.GetAll().FirstAsync(x => x.Id == processRouteId);
                var productId = bom.ProductId;
                var product = await productRep.GetAll().FirstAsync(x => x.Id == productId);
                var unite = await uniteRep.GetAll().FirstAsync(x => x.Id == product.Unit);
                var typeInfos = await typeInfosRep.GetAll().FirstAsync(x => x.Id == product.ProductType);

                // 6. 获取工序Id（取工艺路线下首道工序）
                var processComposition = await processCompositionRep.GetAll().Where(x => x.ProcessRouteId == processRouteId).OrderBy(x => x.SerialNumber).FirstAsync();
                var processId = processComposition.ProcessId;
                var process = await processRep.GetAll().FirstAsync(x=>x.Id == processId);

                // 7. 站点Id和名称（根据工单任务获取站点名称，再根据站点名称获取站点Id和车间id ，再获取车间负责人）
                var siteId = 0;
                var reportingPersonid = 0;

                var siteName = workOrderTask.SiteName ?? string.Empty;

                if (!string.IsNullOrEmpty(siteName))
                {
                    var site = await sitesRep.GetAll().FirstAsync(s => s.SiteName == siteName);
                    if (site != null)
                    {
                        siteId = site.Id;
                        reportingPersonid = site.WorkShopId;
                    }
                       
                }
                var factory = await factoryRep.GetAll().FirstAsync(x=>x.Id == reportingPersonid);
                if (factory == null)
                {
                    return ApiResult.Fail(ResultCode.Fail, "车间不存在");
                }
                var reportingPerson = await userRep.GetAll().FirstAsync(x => x.Id == factory.FactoryManager);

                // 8. 根据班组名称获取班组信息和任务负责人
                var teamId = workOrderTask.ClassGroupId;
                var team = await teamRep.GetByIdAsync(teamId);
                var directorid = team.Director;
                var user = await userRep.GetAll().FirstAsync(x => x.Id == directorid);


                // 9. 构建报工记录
                var reportworkRecord = new ReportworkRecord
                {
                    WorkOrderId = workOrder.Id,
                    OrderNumber = workOrder.OrderNumber,
                    OrderName = workOrder.OrderName,
                    WorkOrderTasksId = workOrderTask.Id,//先获取
                    TaskNumber = workOrderTask.TaskNumber,
                    TaskName = workOrderTask.TaskName,
                    SiteId = siteId,
                    SiteName = siteName,
                    ProcessRouteId = processRouteId,
                    ProcessRouteName = processRoute?.ProcessRouteName,
                    ProcessId = processId,
                    ProcessCode = process?.ProcessCode,
                    ProcessName = process?.ProcessName,
                    TeamId = teamId,
                    TeamName = team.TeamName,
                    Director = user.NickName,
                    ReportingPerson = reportingPerson.NickName,
                    ReportingQuantity = productPlan.PlanNums ?? 0,
                    ReportingDate = DateTime.Now,
                    ProductId = productId,
                    ProductCode = product.ProductCode,
                    ProductName = product.ProductName,
                    Specification = product.Specification,
                    Unit = unite.UniteName,
                    ProductType = typeInfos.TypeName,
                    Status = 0,
                    Desc = dto.reportworkRecorddto.Desc,//手动
                    Id = dto.reportworkRecorddto.Id//手动
                };

                // 10. 保存报工记录
                var reportworkRecordResult = await reportworkRecordRep.AddAsync(reportworkRecord);
                if (reportworkRecordResult <= 0)
                {
                    return ApiResult.Fail(ResultCode.Fail, "生成报工记录失败");
                }

                // 11. 通过ItemCode获取检测项目信息
                var itemsName = string.Empty;
                var inspectionItemCode = string.Empty;
                var inspectionItemTypeId = 0;

                if (dto.reportworkqualityinspectionDto.InspectionItemId != null )
                {
                    // 通过ItemCode查找检测项目
                    var inspectionItem = await inspectionItemRep.GetAll().FirstAsync(x => x.Id == dto.reportworkqualityinspectionDto.InspectionItemId);
                    if (inspectionItem != null)
                    {
                        itemsName = inspectionItem.ItemName;
                        inspectionItemCode = inspectionItem.ItemCode;
                        inspectionItemTypeId = inspectionItem.InspectionItemTypeId;
                    }
                    else
                    {
                        return ApiResult.Fail(ResultCode.Fail, $"未找到ItemCode为{dto.reportworkqualityinspectionDto.ItemCode}的检测项目");
                    }
                }
                else
                {
                    return ApiResult.Fail(ResultCode.Fail, "ItemCode不能为空");
                }
                var inspectionItemTypelist = await inspectionItemTypeRep.GetAll().FirstAsync(x => x.Id == inspectionItemTypeId);
                var InspectionDeptid = workOrderTask.DepartmentId;
                var dept = await deptRep.GetByIdAsync(InspectionDeptid);
                var Inspectorid = workOrderTask.PeopleId;
                var users = await userRep.GetByIdAsync(Inspectorid);
                // 12. 构建报工质检记录
                var reportworkQualityInspection = new ReportworkQualityInspection
                {
                    InspectionItemId = dto.reportworkqualityinspectionDto.InspectionItemId,
                    ItemsName = itemsName,
                    ItemCode = inspectionItemCode,
                    InspectionItemTypeId = inspectionItemTypeId,
                    InspectionItemTypeName = inspectionItemTypelist.InspectionItemTypeName,
                    ReportworkRecordId = reportworkRecord.Id, // 关联新创建的报工记录
                    WorkOrderId = reportworkRecord.WorkOrderId,
                    OrderNumber = reportworkRecord.OrderNumber,
                    OrderName = reportworkRecord.OrderName,
                    WorkOrderTasksId = reportworkRecord.WorkOrderTasksId,
                    TaskNumber = reportworkRecord.TaskNumber,
                    TaskName = reportworkRecord.TaskName,
                    SiteId = reportworkRecord.SiteId,
                    SiteName =  reportworkRecord.SiteName,
                    ProcessId = reportworkRecord.ProcessId,
                    ProcessCode = reportworkRecord.ProductCode,
                    ProcessName = reportworkRecord?.ProcessName,
                    TeamId =reportworkRecord.TeamId,
                    TeamName = reportworkRecord.TeamName,
                    Director = reportworkRecord.Director,
                    ReportingPerson = reportworkRecord.ReportingPerson,
                    ReportingQuantity = reportworkRecord.ReportingQuantity,
                    ReportingDate = DateTime.Now,
                    ProductId = productId,
                    ProductCode = product.ProductCode,
                    ProductName = product.ProductName,
                    Specification = product.Specification,
                    Unit = unite.UniteName,
                    ProductType = typeInfos.TypeName,
                    InspectionDept = dept.DeptName,
                    Inspector = users.NickName,
                    QualityStatus = 0 
                };

                // 13. 保存报工质检记录
                var reportworkQualityInspectionResult = await reportworkQualityInspectionRep.AddAsync(reportworkQualityInspection);
                if (reportworkQualityInspectionResult <= 0)
                {
                    return ApiResult.Fail(ResultCode.Fail, "生成报工质检记录失败");
                }
                var workertask = await work_Order_TasksRep.GetByIdAsync(workerorderId);
                workertask.Status = 4;
                var result = await work_Order_TasksRep.UpdateAsync(workertask);

                if (result <= 0)
                {
                    return ApiResult.Fail(ResultCode.Fail, "修改工单任务状态失败");
                }
                return ApiResult.Success(ResultCode.Ok);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// 报工记录反填
        /// </summary>
        /// <param name="reportworkRecordId">工单任务ID</param>
        /// <returns>返回反填的报工记录信息</returns>
        public async Task<ApiResult<ReportworkRecordDto>> GetReportworkRecordInfoByWorkOrderTaskAsync(int reportworkRecordId)
        {
            try
            {

                var reportworkRecord = await reportworkRecordRep.GetByIdAsync(reportworkRecordId);
                var resultData = mapper.Map<ReportworkRecordDto>(reportworkRecord);
                if (reportworkRecord == null)
                {
                    return ApiResult<ReportworkRecordDto>.Fail(ResultCode.Fail, "报工记录不存在");
                }
                return ApiResult<ReportworkRecordDto>.Success(ResultCode.Ok, resultData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

      
    }
}

