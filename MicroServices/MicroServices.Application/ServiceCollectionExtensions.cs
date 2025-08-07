using MicroServices.Application.IService;
using MicroServices.Application.IService.AllocateAndInventory;
using MicroServices.Application.IService.Bom;
using MicroServices.Application.IService.Bom;
using MicroServices.Application.IService.Houses;
using MicroServices.Application.IService.ProcessInfo;
using MicroServices.Application.IService.ProcessInfo;
using MicroServices.Application.IService.Product_Plan;
using MicroServices.Application.IService.ProductPlan;
using MicroServices.Application.IService.Products;
using MicroServices.Application.IService.Reportworks;
using MicroServices.Application.IService.StorgeServices;
using MicroServices.Application.Services;
using MicroServices.Application.Services.AI;
using MicroServices.Application.Services.AllocateAndInventory;
using MicroServices.Application.Services.Bom;
using MicroServices.Application.Services.House;
using MicroServices.Application.Services.ProcessInfo;
using MicroServices.Application.Services.Product_Plan_Service;
using MicroServices.Application.Services.Products;
using MicroServices.Application.Services.Reportworks;
using MicroServices.Application.Services.StorgeService;
using MicroServices.Repository.IRepository.IInventory;
using MicroServices.Repository.Repository.Inventorys;
using MicroServices.Application.IService.Product_Plan;
using Microsoft.Extensions.DependencyInjection;
using MricoServices.Application.IService.RBAC;
using MricoServices.Application.MapperProFiles;
using MricoServices.Application.Services.RBAC;
using MicroServices.Application.IService.Materials;
using MicroServices.Application.Services.Materials;
using MicroServices.Repository.IRepository.I_Product_Repository;
using MicroServices.Repository.Repository.Product_Repository;
using MicroServices.Repository.IRepository.I_Material_Repository;
using MicroServices.Repository.Repository.Material_Repository;

namespace MicroServices.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            // 注册 AutoMapper
            services.AddAutoMapper(typeof(MapperProFiles));
            // 注册应用层服务
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IReportworkRecordService, ReportworkRecordService>();
            // services.AddScoped<IMenuService, MenuService>();
            // services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IProcessService, ProcessService>();//工序
            services.AddScoped<IProcessCompositionService, ProcessCompositionService>();//工序组合
            services.AddScoped<IProcessRouteService, ProcessRouteService>();//工序路线
            services.AddScoped<IProductPlanService, ProductPlanService>();//生产计划
            services.AddScoped<IWorkOrderService, WorkOrderService>();//工单
            services.AddScoped<IBomService, BomService>();//BOM
            services.AddScoped<IProductService, ProductService>();//产品
            services.AddScoped<IReportworkQualityInspectionService, ReportworkQualityInspectionService>();//报工质检
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<IAllocateService, AllocateService>();
			//注册仓库相关服务
			services.AddScoped<IWareHouseService, WareHouseService>();
			services.AddScoped<IHouseAreaService, HouseAreaService>();
			services.AddScoped<IHouseLocationService, HouseLocationService>();

			//注册出入库相关服务
			services.AddScoped<IProductStorgeService, ProductStorgeService>();
			services.AddScoped<IMaterialInventoryRepository, MaterialInventoryRepository>();
			services.AddScoped<IProductInventoryRepository, ProductInventoryRepository>();

            services.AddScoped<IMaterialService, MaterialService>();
            //工单任务
            services.AddScoped<IWorkOrderTasksService, WorkOrderTasksService>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUniteRepository, UniteReposiotory>();
            return services;
        }
    }
}
