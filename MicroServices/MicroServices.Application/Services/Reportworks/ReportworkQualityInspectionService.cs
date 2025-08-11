using AutoMapper;
using MicroServices.Application.IService.Reportworks;
using MicroServices.Domain.Bom;
using MicroServices.Domain.FacatoryFloors;
using MicroServices.Domain.Materials;
using MicroServices.Domain.ProcessInfo;
using MicroServices.Domain.Product_Plan;
using MicroServices.Domain.Product_Plan;
using MicroServices.Domain.Qualify;
using MicroServices.Domain.Reportworks;
using MicroServices.Domain.Sites;
using MicroServices.Models.Dtos.Reportworks;
using MicroServices.Repository.IRepository.Reportworks;
using MricoServices.Domain.RBAC;
using MricoServices.Repository.IRepository;
using MricoServices.Shared.ApiResult;
using SqlSugar.Extensions;
using System.Drawing.Drawing2D;

namespace MicroServices.Application.Services.Reportworks
{
    public class ReportworkQualityInspectionService : IReportworkQualityInspectionService
    {
        private readonly IBaseRepository<ReportworkQualityInspection> reportworkQualityInspectionRep;//报工质检
        private readonly IBaseRepository<InspectionResult> inspectionResultRep;//检测结果
        private readonly IBaseRepository<InspectionItem> inspectionItemRep;//检测项目
        private readonly IReportworkRecordRepository reportworkRecordRep;//报工记录
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
        private readonly IBaseRepository<InspectionItemType> inspectionItemTypeRep;//检测项目类型 
        private readonly IBaseRepository<Unite> uniteRep;//单位
        private readonly IBaseRepository<TypeInfos> typeInfosRep;//类型
        private readonly IMapper mapper;

        public ReportworkQualityInspectionService(IBaseRepository<ReportworkQualityInspection> reportworkQualityInspectionRep, IBaseRepository<InspectionResult> inspectionResultRep, IBaseRepository<InspectionItem> inspectionItemRep, IReportworkRecordRepository reportworkRecordRep, IBaseRepository<Team> teamRep, IBaseRepository<WorkOrder> work_OrderRep, IBaseRepository<WorkOrderTasks> work_Order_TasksRep, IBaseRepository<Sites> sitesRep, IBaseRepository<ProductPlan> productPlanRep, IBaseRepository<BOM> bomRep, IBaseRepository<ProcessRoute> processRouteRep, IBaseRepository<Product> productRep, IBaseRepository<ProcessComposition> processCompositionRep, IBaseRepository<Processes> processRep, IBaseRepository<User> userRep, IBaseRepository<FactoryFloor> factoryRep, IBaseRepository<InspectionItemType> inspectionItemTypeRep, IBaseRepository<Unite> uniteRep, IBaseRepository<TypeInfos> typeInfosRep, IMapper mapper)
        {
            this.reportworkQualityInspectionRep = reportworkQualityInspectionRep;
            this.inspectionResultRep = inspectionResultRep;
            this.inspectionItemRep = inspectionItemRep;
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
            this.inspectionItemTypeRep = inspectionItemTypeRep;
            this.uniteRep = uniteRep;
            this.typeInfosRep = typeInfosRep;
            this.mapper = mapper;
        }


        /// <summary>
        /// 报工质检列表 查询分页
        /// </summary>
        /// <param name="searchReportworkQualityInspectionDto">查询条件</param>
        /// <returns>返回报工质检列表 查询分页</returns>
        public async Task<ApiResult<ApiPaging<List<ReportworkQualityInspectionDto>>>> GetAllReportworkRecordAsync(SearchReportworkQualityInspectionDto searchReportworkQualityInspectionDto)
        {
            try
            {
                var query = reportworkQualityInspectionRep.GetAll().Where(x => !x.IsDeleted);
                #region
                if (!string.IsNullOrEmpty(searchReportworkQualityInspectionDto.ItemName))
                {
                    query = query.Where(x => x.ItemsName.Contains(searchReportworkQualityInspectionDto.ItemName));
                }
                if (!string.IsNullOrEmpty(searchReportworkQualityInspectionDto.TaskName))
                {
                    query = query.Where(x => x.TaskName.Contains(searchReportworkQualityInspectionDto.TaskName));
                }
                if (!string.IsNullOrEmpty(searchReportworkQualityInspectionDto.OrderName))
                {
                    query = query.Where(x => x.OrderName.Contains(searchReportworkQualityInspectionDto.OrderName));
                }
                if (!string.IsNullOrEmpty(searchReportworkQualityInspectionDto.Inspector))
                {
                    query = query.Where(x => x.Inspector.Contains(searchReportworkQualityInspectionDto.Inspector));
                }
                if (!string.IsNullOrEmpty(searchReportworkQualityInspectionDto.InspectionDept))
                {
                    query = query.Where(x => x.InspectionDept.Contains(searchReportworkQualityInspectionDto.InspectionDept));
                }
                if (!string.IsNullOrEmpty(searchReportworkQualityInspectionDto.InspectionResult))
                {
                    query = query.Where(x => x.InspectionResult.Contains(searchReportworkQualityInspectionDto.InspectionResult));
                }
                if (searchReportworkQualityInspectionDto.QualityStatus != null)
                {
                    query = query.Where(x => x.QualityStatus == searchReportworkQualityInspectionDto.QualityStatus);
                }
                if (searchReportworkQualityInspectionDto.ReportworkRecordId != null)
                {
                    query = query.Where(x => x.ReportworkRecordId == searchReportworkQualityInspectionDto.ReportworkRecordId);
                }
                if (!string.IsNullOrEmpty(searchReportworkQualityInspectionDto.ReportingDate))
                {
                    query = query.Where(x => x.ReportingDate.Date == DateTime.Parse(searchReportworkQualityInspectionDto.ReportingDate));
                }
                if (!string.IsNullOrEmpty(searchReportworkQualityInspectionDto.QualitystartDate))
                {
                    query = query.Where(reportworkRecord => reportworkRecord.QualityDate >= DateTime.Parse(searchReportworkQualityInspectionDto.QualitystartDate));
                }
                if (!string.IsNullOrEmpty(searchReportworkQualityInspectionDto.QualityendDate))
                {
                    query = query.Where(reportworkRecord => reportworkRecord.QualityDate < DateTime.Parse(searchReportworkQualityInspectionDto.QualityendDate));
                }
                if (searchReportworkQualityInspectionDto.ProcessId != null)
                {
                    query = query.Where(x => x.ProcessId == searchReportworkQualityInspectionDto.ProcessId);
                }
                if (searchReportworkQualityInspectionDto.ProductId != null)
                {
                    query = query.Where(x => x.ProductId == searchReportworkQualityInspectionDto.ProductId);
                }
                if (searchReportworkQualityInspectionDto.SiteId != null)
                {
                    query = query.Where(x => x.SiteId == searchReportworkQualityInspectionDto.SiteId);
                }
                if (searchReportworkQualityInspectionDto.TeamId != null)
                {
                    query = query.Where(x => x.TeamId == searchReportworkQualityInspectionDto.TeamId);
                }
                if (searchReportworkQualityInspectionDto.InspectionItemTypeId != null)
                {
                    query = query.Where(x => x.InspectionItemTypeId == searchReportworkQualityInspectionDto.InspectionItemTypeId);
                }
                #endregion
                var totalCount = query.Count();
                var totalPage = (int)Math.Ceiling(totalCount * 1.0 / searchReportworkQualityInspectionDto.PageSize);
                var pageData = query.OrderBy(it => it.Id).Skip((searchReportworkQualityInspectionDto.PageIndex - 1) * searchReportworkQualityInspectionDto.PageSize)
                    .Take(searchReportworkQualityInspectionDto.PageSize).ToList();
                //// 映射为DTO
                var resultData = mapper.Map<List<ReportworkQualityInspectionDto>>(pageData);

                var apiPagingData = new ApiPaging<List<ReportworkQualityInspectionDto>>
                {
                    TotalCount = totalCount,
                    TotalPage = totalPage,
                    Data = resultData
                };
                return ApiResult<ApiPaging<List<ReportworkQualityInspectionDto>>>.Success(ResultCode.Ok, apiPagingData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// 报工质检的软删除
        /// </summary>
        /// <param name="reportworkQualityInspectionid">报工质检id</param>
        /// <returns>返回受影响行数</returns>
        public async Task<ApiResult> DeleteReportworkQualityInspectionAsync(int reportworkQualityInspectionid)
        {
            try
            {
                var list = await reportworkQualityInspectionRep.GetAll().Where(d => d.Id == reportworkQualityInspectionid).AnyAsync();
                if (!list)
                {
                    return ApiResult.Fail(ResultCode.Fail, "该质检不存在");
                }
                var result = await reportworkQualityInspectionRep.SoftDeleteAsync(reportworkQualityInspectionid);
                return result > 0
                    ? ApiResult.Success(ResultCode.Ok)
                    : ApiResult.Fail(ResultCode.Fail, "质检删除失败");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// 报工质检的反填
        /// </summary>
        /// <param name="ReportworkQualityInspectionId"></param>
        /// <returns></returns>
        public async Task<ApiResult<ReportworkQualityInspectionDto>> GetFTReportworkQualityInspection(int ReportworkQualityInspectionId)
        {
            try
            {

                var reportworkQualityInspection = await reportworkQualityInspectionRep.GetByIdAsync(ReportworkQualityInspectionId);
                var resultData = mapper.Map<ReportworkQualityInspectionDto>(reportworkQualityInspection);
                if (reportworkQualityInspection == null)
                {
                    return ApiResult<ReportworkQualityInspectionDto>.Fail(ResultCode.Fail, "报工记录不存在");
                }
                return ApiResult<ReportworkQualityInspectionDto>.Success(ResultCode.Ok, resultData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// 质检
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult<ReportworkQualityInspectionDto>> Quality(int ReportworkQualityInspectionId,CreateReportworkRecordAndReportworkQualityInspection dto)
        {
           try
           {
                //质检
                var reportworkQualityInspection = await reportworkQualityInspectionRep.GetByIdAsync(ReportworkQualityInspectionId);
                //检验项目
                var inspectionItemlist = await inspectionItemRep.GetByIdAsync(reportworkQualityInspection.InspectionItemId);
                //检验项目修改
                var inspectionItemupdatedto = mapper.Map<InspectionItem>(inspectionItemlist);
                var inspectionResult = await inspectionItemRep.UpdateAsync(inspectionItemupdatedto);
                if (inspectionResult <= 0)
                {
                    return ApiResult<ReportworkQualityInspectionDto>.Fail(ResultCode.Fail, "生成检验项目失败");
                }
                var newInspectionItemId = reportworkQualityInspection.InspectionItemId; // 记录新增检验项目的 ID
                var inspectionResultlist = await inspectionResultRep.GetByIdAsync(newInspectionItemId);
                var qualifiedRate = (decimal)dto.inspectionResult.QualifiedQuantity / dto.inspectionResult.InspectionQuantity * 100 ;//合格率
                var criticalDefectRate = (decimal)inspectionItemlist.CriticalDefectCount / dto.inspectionResult.UnqualifiedQuantity*100;//致命
                var seriousDefectCount = (decimal)inspectionItemlist.SeriousDefectCount / dto.inspectionResult.UnqualifiedQuantity * 100;//严重
                var minorDefectCount = (decimal)inspectionItemlist.MinorDefectCount / dto.inspectionResult.UnqualifiedQuantity * 100;//轻微
                var inspectionConclusion = "";
                if (dto.inspectionResult.QualifiedRate.ObjToInt() > 35)
                {
                    inspectionConclusion = "不合格";
                }
                else
                {
                    inspectionConclusion = "合格";
                }
                var inspectionResultupdate = new CreateInspectionResult
                {
                    QualityDate = DateTime.Now,
                    InspectionQuantity = newInspectionItemId,
                    QualifiedQuantity = dto.inspectionResult.QualifiedQuantity,
                    UnqualifiedQuantity = dto.inspectionResult.UnqualifiedQuantity,
                    QualifiedRate = qualifiedRate.ToString(),
                    CriticalDefectRate = criticalDefectRate.ToString(),
                    SeriousDefectRate = seriousDefectCount.ToString(),
                    MinorDefectRate = minorDefectCount.ToString(),
                    InspectionConclusion = inspectionConclusion,
                    Remarks = dto.inspectionResult.Remarks
                };
                //检验结果修改
                var inspectionResultupdateto = mapper.Map<CreateInspectionResult, InspectionResult>(inspectionResultupdate);
                var inspectionResultResult = await inspectionResultRep.AddAsync(inspectionResultupdateto);
                if (inspectionResultResult <= 0)
                {
                    return ApiResult<ReportworkQualityInspectionDto>.Fail(ResultCode.Fail, "生成检验结果失败");
                }
                var newInspectionResultId = inspectionResultupdateto.Id; // 记录新增检验结果的 ID



                reportworkQualityInspection.InspectionItemId = newInspectionItemId; // 关联新增的检验项目
                reportworkQualityInspection.Qualified = inspectionResultupdateto.QualifiedQuantity;//合格
                reportworkQualityInspection.Unqualified = inspectionResultupdateto.UnqualifiedQuantity;//不合格
                reportworkQualityInspection.QualifiedRate = inspectionResultupdateto.QualifiedRate;//合格率
                reportworkQualityInspection.InspectionResult = inspectionResultupdateto.InspectionConclusion;//结果
                reportworkQualityInspection.QualityStatus = 1; // 1=已质检
                reportworkQualityInspection.QualityDate = inspectionResultupdateto.QualityDate; //检验时间
                var updatereportworkQualityInspectionResult = await reportworkQualityInspectionRep.UpdateAsync(reportworkQualityInspection);//修改质检
                if ( updatereportworkQualityInspectionResult <= 0)
                {
                    return ApiResult<ReportworkQualityInspectionDto>.Fail(ResultCode.Fail, "修改质检数据失败");
                }
                var reportworkRecordlist = await reportworkRecordRep.GetAll().FirstAsync(x => x.TaskNumber == reportworkQualityInspection.TaskNumber);
                reportworkRecordlist.Qualified = inspectionResultupdateto.QualifiedQuantity;//合格
                reportworkRecordlist.Unqualified = inspectionResultupdateto.UnqualifiedQuantity;//不合格
                reportworkRecordlist.QualifiedRate = inspectionResultupdateto.QualifiedRate;//合格率
                reportworkRecordlist.InspectionResult = inspectionResultupdateto.InspectionConclusion;//结果
                reportworkRecordlist.Status = 1; // 1=已质检
                reportworkRecordlist.QualityDate = inspectionResultupdateto.QualityDate; //检验时间
                var updatereportworkRecordlistResult = await reportworkRecordRep.UpdateAsync(reportworkRecordlist);//修改记录
                if (updatereportworkRecordlistResult <= 0)
                {
                    return ApiResult<ReportworkQualityInspectionDto>.Fail(ResultCode.Fail, "修改记录数据失败");
                }
                var resultData = mapper.Map<ReportworkQualityInspectionDto>(reportworkQualityInspection);
                return ApiResult<ReportworkQualityInspectionDto>.Success(ResultCode.Ok, resultData);

            }
           catch (Exception)
           {
               throw;
           }
        }
        
        /// <summary>
        /// 根据报工质检ID获取检测项目信息
        /// </summary>
        /// <param name="reportworkQualityInspectionId">报工质检ID</param>
        /// <returns>返回检测项目详细信息</returns>
        public async Task<ApiResult<InspectionItemDisplayDto>> GetInspectionItemByQualityInspectionIdAsync(int reportworkQualityInspectionId)
        {
            try
            {
                // 根据报工质检ID获取报工质检信息
                var reportworkQualityInspection = await reportworkQualityInspectionRep.GetByIdAsync(reportworkQualityInspectionId);
                if (reportworkQualityInspection == null || reportworkQualityInspection.IsDeleted)
                {
                    return ApiResult<InspectionItemDisplayDto>.Fail(ResultCode.Fail, "报工质检记录不存在");
                }

                // 根据检测项目ID获取检测项目信息
                var inspectionItem = await inspectionItemRep.GetByIdAsync(reportworkQualityInspection.InspectionItemId);
                if (inspectionItem == null || inspectionItem.IsDeleted)
                {
                    return ApiResult<InspectionItemDisplayDto>.Fail(ResultCode.Fail, "检测项目不存在");
                }

                // 获取检测项目类型信息
                var inspectionItemType = await inspectionItemTypeRep.GetByIdAsync(inspectionItem.InspectionItemTypeId);
                
                // 获取单位信息
                var unite = await uniteRep.GetByIdAsync(inspectionItem.UnitId);

                // 构建返回的DTO
                var inspectionItemDisplayDto = new InspectionItemDisplayDto
                {
                    Id = inspectionItem.Id,
                    ItemName = inspectionItem.ItemName,
                    ItemCode = inspectionItem.ItemCode,
                    InspectionItemTypeId = inspectionItem.InspectionItemTypeId,
                    InspectionItemTypeName = inspectionItemType?.InspectionItemTypeName ?? "",
                    ToolingFixtureTypeId = inspectionItem.ToolingFixtureTypeId,
                    ToolingFixtureTypeName = "", // 如果需要工装夹具类型名称，需要额外查询
                    InspectionRequirement = inspectionItem.InspectionRequirement,
                    StandardValue = inspectionItem.StandardValue,
                    UnitId = inspectionItem.UnitId,
                    UnitName = unite.UniteName,
                    ToleranceUpper = inspectionItem.ToleranceUpper,
                    ToleranceLower = inspectionItem.ToleranceLower,
                    CriticalDefectCount = inspectionItem.CriticalDefectCount,
                    SeriousDefectCount = inspectionItem.SeriousDefectCount,
                    MinorDefectCount = inspectionItem.MinorDefectCount,
                    Remarks = inspectionItem.Remarks,
                    ReportworkQualityInspectionId = reportworkQualityInspectionId,
                    ReportworkQualityInspectionName = reportworkQualityInspection.ItemsName
                };

                return ApiResult<InspectionItemDisplayDto>.Success(ResultCode.Ok, inspectionItemDisplayDto);
            }
            catch (Exception ex)
            {
                return ApiResult<InspectionItemDisplayDto>.Fail(ResultCode.Fail, $"获取检测项目信息失败: {ex.Message}");
            }
        }
    }
}
