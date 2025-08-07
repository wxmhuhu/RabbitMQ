using MicroServices.Domain.Equipments;
using MicroServices.Domain.FacatoryFloors;
using MicroServices.Domain.Materials;
using MicroServices.Domain.Allocate;
using MicroServices.Domain.Inventory;
using MicroServices.Domain.Bom;
using MicroServices.Domain.ProcessInfo;
using MicroServices.Domain.Product_Plan;
using MicroServices.Domain.ProductPlan;
using MicroServices.Domain.Qualify;
using MicroServices.Domain.Reportworks;
using MicroServices.Domain.Sites;
using MricoServices.Domain.RBAC;
using MricoServices.Shared;
using SqlSugar;
using MicroServices.Domain.InStorage;

namespace MricoServices.Infrastructure.Data
{
    public static class SqlSugarSetup
    {
        public static ISqlSugarClient GetSqlSugarClient(string connectionString, DbType dbType = DbType.PostgreSQL)
        {
            var db = new SqlSugarClient(new ConnectionConfig()
            {
                DbType = dbType,
                ConnectionString = connectionString,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute,
                MoreSettings = new ConnMoreSettings { },
            });

            // 1. 配置实体与数据库表的映射
            // 确保所有需要映射的实体都包含在 InitTables 中
            db.CodeFirst.InitTables(
                typeof(User),
                typeof(Role),
                typeof(UserRole),
                typeof(Permission),
                typeof(RolePermission),
                typeof(Menu),
                typeof(RoleMenu),
                typeof(ProductPlan), //生产计划表
                typeof(WorkOrder), //生产工单表
                typeof(WorkOrderTasks), // 工单任务表

                typeof(Equipment),//设备表
                typeof(EquipmentType),//设备类型表
                typeof(FactoryFloor),//车间表
                typeof(Team),//班组表
                typeof(TeamType),//班组类型
                typeof(TeamUser),//班组成员
                typeof (DeptInfo),//部门
                typeof(Material),//物料
                typeof(Unite),//单位
                typeof(TypeInfos),//类型
                typeof(Category),//分类
                typeof(PropertyInfos),//属性
                typeof(Product),//产品

                typeof(CommonDefects),//常见缺陷表
                typeof(DefectionType),//缺陷类型表
                typeof(DefectLevel),//缺陷等级表
                typeof(DefectLocation),//缺陷位置表
                typeof(ReportworkQualityInspection),//报工质检
                typeof(MicroServices.Domain.Reportworks.InspectionType),//报工质检类型表
                typeof(MicroServices.Domain.Reportworks.InspectionItem),//检验项目表
                typeof(MicroServices.Domain.Reportworks.InspectionItemType),//检测项目类型表
                typeof(InspectionResult),//检验结果表
                typeof(ReportworkRecord),//报工记录表
                typeof(Sites),//站点表
                typeof(SiteEquipment),//站点设备表
                typeof(SiteTools),//站点工具表
                typeof(Inventory), //盘点表
                typeof(InventoryDetail), //盘点明细
                typeof(Allocate), //调拨表
                typeof(AllocateDetail), //调拨明细
                typeof(Processes),//工序表
                typeof(ProcessComposition),//工序组成表
                typeof(ProcessRoute),//工艺路线表
                typeof(BOM),//Bom表
                typeof(BomProductMaterial),//Bom产品物料表
                typeof(ProcessBomMaterialProduct),//工序Bom物料产品表
                typeof(SourceType)//来源类型表
            );

			#region 仓库相关表的数据初始化

			db.CodeFirst.InitTables(typeof(ProductStorage)); // 初始化 产品入库表 实体对应的数据库表
			db.CodeFirst.InitTables(typeof(ProductStorageDetail)); // 初始化 产品入库明细表 实体对应的数据库表
			db.CodeFirst.InitTables(typeof(PurchaseStorage)); // 初始化 物料入库单 实体对应的数据库表
			db.CodeFirst.InitTables(typeof(PurchaseInventoryMaterial)); // 初始化 物料入库明细 实体对应的数据库表
			db.CodeFirst.InitTables(typeof(PurchaseTest)); // 初始化 采购检验单 实体对应的数据库表
			db.CodeFirst.InitTables(typeof(ReceiveOrReturn)); // 初始化 退料单编号 实体对应的数据库表
			db.CodeFirst.InitTables(typeof(WareHouse)); // 初始化 仓库 实体对应的数据库表
			db.CodeFirst.InitTables(typeof(WareHouseArea)); // 初始化 库区信息表 实体对应的数据库表
			db.CodeFirst.InitTables(typeof(WareHouseLocation)); // 初始化 库位信息表 实体对应的数据库表
			db.CodeFirst.InitTables(typeof(MaterialInventory)); // 初始化 库存表 实体对应的数据库表
			db.CodeFirst.InitTables(typeof(ProductInventory)); // 初始化 产品库存表 实体对应的数据库表
			#endregion

			// 2. 配置全局查询过滤器 (实现软删除的核心)
			db.QueryFilter.AddTableFilter<AuditableEntity>(it => it.IsDeleted == false);

            // 3. 配置 AOP 拦截 (用于自动填充审计字段)
            db.Aop.DataExecuting = (oldValue, entityInfo) =>
            {
                // TODO: 在实际应用中，这里需要从 HttpContextAccessor 或其他身份验证机制中获取当前用户ID和用户名
                // 推荐使用一个全局的用户上下文服务来获取，例如：
                // var currentUser = CurrentUserAccessor.GetCurrentUser(); // 假设你有这样的服务
                // int currentUserId = currentUser?.Id ?? 0; // 或者你的匿名用户ID
                // string currentUserName = currentUser?.UserName ?? "SystemUser";

                // 为演示，继续使用硬编码的示例值
                int currentUserId = 1; // 示例用户ID (请根据实际情况替换)
                string currentUserName = "SystemUser"; // 示例用户名 (请根据实际情况替换)

                // 检查实体是否是 AuditableEntity 类型或其子类
                if (entityInfo.EntityValue is AuditableEntity auditableEntity)
                {
                    if (entityInfo.OperationType == DataFilterType.InsertByObject) // 插入操作 (新增)
                    {
                        // 插入时填充创建信息
                        // CreatedAt 已经有默认值 DateTime.Now，但 AOP 重新赋值更安全
                        auditableEntity.CreatedAt = DateTime.Now;
                        auditableEntity.CreatedBy = currentUserId;
                        auditableEntity.CreatedByUserName = currentUserName;
                        auditableEntity.IsDeleted = false; // 确保新增时默认为未删除

                        // 确保其他审计字段在新增时为 null
                        auditableEntity.UpdatedAt = null;
                        auditableEntity.UpdatedBy = null;
                        auditableEntity.UpdatedByUserName = null;
                        auditableEntity.DeletedAt = null;
                        auditableEntity.DeletedBy = null;
                        auditableEntity.DeletedByUserName = null;
                    }
                    else if (entityInfo.OperationType == DataFilterType.UpdateByObject) // 更新操作
                    {
                        // **软删除逻辑：当 IsDeleted 从 false 变为 true 且 DeletedAt 为空时，才填充删除信息**
                        // 这个判断依赖于你的 Updateable 操作中将 IsDeleted 设置为 true
                        // 如果 OldValue 可用，并且 OldValue.IsDeleted 为 false，新值 auditableEntity.IsDeleted 为 true，
                        // 同时 auditableEntity.DeletedAt 为 null，这表示是首次软删除。
                        // 由于 entityInfo.OldValue 在 Updateable().SetColumns().Where() 中可能为 null，
                        // 我们主要依赖 auditableEntity.DeletedAt == null 来判断是否为首次标记删除。
                        if (auditableEntity.IsDeleted == true && auditableEntity.DeletedAt == null)
                        {
                            auditableEntity.DeletedAt = DateTime.Now;
                            auditableEntity.DeletedBy = currentUserId;
                            auditableEntity.DeletedByUserName = currentUserName;
                            // 当进行软删除时，你可能不希望同时更新 UpdatedAt/UpdatedBy，因为这通常表示业务更新。
                            // 如果你认为软删除也是一种更新，可以不加 return。
                            // 在这里加 return，意味着软删除只填充删除相关字段，不填充普通更新字段。
                            return; // 软删除操作处理完毕，直接返回
                        }
                        // else if (auditableEntity.IsDeleted == false && auditableEntity.DeletedAt != null)
                        // {
                        //     // 如果实体被“恢复”（IsDeleted 从 true 变为 false），可以清除删除审计信息
                        //     // 这段代码在你的 AuditableEntity 定义中没有被用到，暂不启用。
                        //     // auditableEntity.DeletedAt = null;
                        //     // auditableEntity.DeletedBy = null;
                        //     // auditableEntity.DeletedByUserName = null;
                        // }

                        // **通用更新逻辑：对于所有非软删除的更新操作，都更新 UpdatedAt 和 UpdatedBy**
                        // 这段代码只有在上面的软删除逻辑没有 return 的情况下才会执行
                        auditableEntity.UpdatedAt = DateTime.Now;
                        auditableEntity.UpdatedBy = currentUserId;
                        auditableEntity.UpdatedByUserName = currentUserName;
                    }
                }
            };

            // 配置AOP：SQL执行前 (调试用)
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine($"\n--- Executing SQL ---");
                Console.WriteLine($"SQL: {sql}");
                if (pars != null)
                {
                    foreach (var param in pars)
                    {
                        Console.WriteLine($"  Param: {param.ParameterName} = {param.Value}");
                    }
                }
                Console.WriteLine($"---------------------\n");
            };

            // 配置AOP：SQL执行后 (调试用)
            db.Aop.OnLogExecuted = (sql, pars) =>
            {
                // 可以在这里记录SQL执行时间等
            };

            return db;
        }
    }
}