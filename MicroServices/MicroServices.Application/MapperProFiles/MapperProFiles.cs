using AutoMapper;
using MicroServices.Domain.Allocate;
using MicroServices.Domain.Bom;
using MicroServices.Domain.InStorage;
using MicroServices.Domain.Inventory;
using MicroServices.Domain.ProcessInfo;
using MicroServices.Domain.Product_Plan;
using MicroServices.Domain.Product_Plan;
using MicroServices.Domain.Reportworks;
using MicroServices.Models.Dtos;
using MicroServices.Models.Dtos.AllocateAndInventory.Allocate;
using MicroServices.Models.Dtos.AllocateAndInventory.Inventory;
using MicroServices.Models.Dtos.Bom;
using MicroServices.Models.Dtos.House;
using MicroServices.Models.Dtos.Product_PlanDtos;
using MicroServices.Models.Dtos.RBACDtos;
using MicroServices.Models.Dtos.Reportworks;
using MicroServices.Models.Dtos.StorgeDTOS;
using MricoServices.Domain.RBAC;
using static MicroServices.Models.Dtos.Product_PlanDtos.SearchWorkOrderDtos;

namespace MricoServices.Application.MapperProFiles
{
    public class MapperProFiles:Profile
    {
        public MapperProFiles()
        {
            ///用户
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<CreateUpdateUserDto, UserDto>().ReverseMap();
            CreateMap<CreateUpdateUserDto, User>().ReverseMap();

            ///角色
            CreateMap<RoleDto, Role>().ReverseMap();
            CreateMap<CreateUpdateRoleDto, RoleDto>().ReverseMap();
            CreateMap<CreateUpdateRoleDto, Role>().ReverseMap();

            ///权限
            CreateMap<PermissionDto, Permission>().ReverseMap();
            CreateMap<CreateUpdatePermissionDto, PermissionDto>().ReverseMap();
            CreateMap<CreateUpdatePermissionDto, Permission>().ReverseMap();

            //菜单
            CreateMap<MenuDto, Menu>().ReverseMap();
            CreateMap<CreateUpdateMenuDto, MenuDto>().ReverseMap();
            CreateMap<CreateUpdateMenuDto, Menu>().ReverseMap();
            //报工记录
            CreateMap<ReportworkRecord, ReportworkRecordDto>().ReverseMap();
            CreateMap<ReportworkRecord, CreateReportworkRecordDto>().ReverseMap();
            //报工质检
            CreateMap<ReportworkQualityInspection, ReportworkQualityInspectionDto>().ReverseMap();
            CreateMap<ReportworkQualityInspection, CreateReportworkQualityInspectionDto>().ReverseMap(); 
            CreateMap<InspectionItem, CreateInspectionItem>().ReverseMap();//检验项目
            CreateMap<InspectionItem, InspectionItemDisplayDto>().ReverseMap();//检测项目显示DTO
            CreateMap<InspectionResult, CreateInspectionResult>().ReverseMap(); //检验结果
            ///生产计划
            CreateMap<ProductPlanDto, ProductPlan>().ReverseMap();
            CreateMap<CreateUpdateProductionPlanDto, ProductPlanDto>().ReverseMap();
            CreateMap<CreateUpdateProductionPlanDto, ProductPlan>().ReverseMap();

            ///生产工单
            CreateMap<WorkOrderDtos, WorkOrder>().ReverseMap();
            CreateMap<CreateUpdateWorkOrderDtos, WorkOrderDtos>().ReverseMap();
            CreateMap<CreateUpdateWorkOrderDtos, WorkOrder>().ReverseMap();
            CreateMap<WorkOrder, ProductionSchedulingDto>().ReverseMap();

            ///工单任务
            CreateMap<WorkOrderTasksDto, WorkOrderTasks>().ReverseMap();
            CreateMap<CreateUpdateWorkOrderTasksDtos, WorkOrderTasksDto>().ReverseMap();
            CreateMap<CreateUpdateWorkOrderTasksDtos, WorkOrderTasks>().ReverseMap();

            //工序、工序组合、工序路线

            CreateMap<CreateUpdateInventoryDto,Inventory>().ReverseMap();
            CreateMap<Inventory, InventoryDto>().ReverseMap();
            CreateMap<CreateUpdateInventoryDetailDto, InventoryDetail>().ReverseMap();


            CreateMap<Allocate, AllocateDto>().ReverseMap();
            CreateMap<CreateUpdateAllocateDto, AllocateDto>().ReverseMap();
            CreateMap<CreateUpdateAllocateDetailDto, AllocateDto>().ReverseMap();
            CreateMap<Processes, ProcessDto>().ReverseMap();
            CreateMap<Processes, CreateOrUpdateProcessDto>().ReverseMap();
            CreateMap<ProcessComposition, ProcessCompositionDto>().ReverseMap();
            CreateMap<ProcessComposition, CreateOrUpdateProcessCompositionDto>().ReverseMap();
            CreateMap<ProcessRoute, ProcessRouteDto>().ReverseMap();
            CreateMap<ProcessRoute, CreateOrUpdateProcessRouteDto>().ReverseMap();

            //BOM
            CreateMap<BOM, BomDto>().ReverseMap();
            CreateMap<BOM, BomTreeDto>().ReverseMap();
            CreateMap<BOM, CreateOrUpdateBomDto>().ReverseMap();

			#region 仓库相关Dto
			//仓库相关Dto
			CreateMap<WareHouseDto, WareHouse>().ReverseMap();
			CreateMap<CreateUpdateWareHouseDto, WareHouseDto>().ReverseMap();
			CreateMap<CreateUpdateWareHouseDto, WareHouse>().ReverseMap();
			//库区相关Dto
			CreateMap<WareHouseAreaDto, WareHouseArea>().ReverseMap();
			CreateMap<CreateUpdateWareHouseAreaDto, WareHouseAreaDto>().ReverseMap();
			CreateMap<CreateUpdateWareHouseAreaDto, WareHouseArea>().ReverseMap();
			//库位相关Dto
			CreateMap<HouseLocationDto, WareHouseLocation>().ReverseMap();
			CreateMap<CreateUpdateHouseLocationDto, HouseLocationDto>().ReverseMap();
			CreateMap<CreateUpdateHouseLocationDto, WareHouseLocation>().ReverseMap();
			//入库单相关Dto
			CreateMap<ProductStorgeDto, ProductStorage>().ReverseMap();
			CreateMap<CreateUpdateProductStorgeDto, ProductStorgeDto>().ReverseMap();
			CreateMap<CreateUpdateProductStorgeDto, ProductStorage>().ReverseMap();
			//入库明细相关Dto
			CreateMap<ProductStorgeDetailDto, ProductStorageDetail>().ReverseMap();
			CreateMap<CreateUpdateProductStorgeDetailDto, ProductStorgeDetailDto>().ReverseMap();
			CreateMap<CreateUpdateProductStorgeDetailDto, ProductStorageDetail>().ReverseMap();
			//产品库存相关Dto
			CreateMap<ProductInventoryDto, ProductInventory>().ReverseMap();
			CreateMap<CreateUpdateProductInventoryDto, ProductInventoryDto>().ReverseMap();
			CreateMap<CreateUpdateProductInventoryDto, ProductInventory>().ReverseMap();

			#endregion
		}
	}
}
//1.业务使用MQ
//2.使用缓存的地方--分析业务
//3.串口++并口