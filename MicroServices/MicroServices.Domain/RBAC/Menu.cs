using MricoServices.Shared;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MricoServices.Domain.RBAC
{
    //菜单表
    public class Menu : AuditableEntity
    {
        public string MenuName { get; set; } // 菜单名称，例如：用户管理、商品列表
        public string Path { get; set; }     // 路由路径或URL，例如：/user-manage
        public string Icon { get; set; }     // 菜单图标，例如：user-icon
        public int ParentId { get; set; }    // 父菜单ID，用于构建菜单树结构 (0表示根菜单)
        public int OrderNum { get; set; }    // 菜单排序号
        public int MenuType { get; set; }    // 菜单类型 (例如：0-目录, 1-菜单, 2-按钮/权限点)
        public bool IsHidden { get; set; }   // 是否隐藏菜单
        public string Component { get; set; } // 前端组件路径，例如：@/views/system/user/index.vue
        public List<int>? PermissionId { get; set; } // 关联的权限ID (如果有)


        // 导航属性：一个菜单可以被多个角色访问
        [SugarColumn(IsIgnore = true)]
        public List<RoleMenu> RoleMenus { get; set; }

        // 导航属性：子菜单 (如果需要构建完整的菜单树)
        [SugarColumn(IsIgnore = true)]
        public List<Menu> Children { get; set; }
    }
}
