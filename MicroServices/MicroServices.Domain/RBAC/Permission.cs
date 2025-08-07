using MricoServices.Shared;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MricoServices.Domain.RBAC
{
    public class Permission : AuditableEntity
    {
        public string PermissionName { get; set; }
        public string Resource { get; set; } // 例如：/api/users
        public string Action { get; set; }   // 例如：GET, POST, PUT, DELETE


        /// <summary>
        /// 权限编码 (唯一标识)，例如: "user:add", "role:view", "product:delete"
        /// 通常用于前端权限控制和后端鉴权。
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = false, ColumnDescription = "权限编码 (唯一标识)")]
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// 权限名称，例如: "用户添加", "角色查看", "产品删除"
        /// 用于在用户界面展示。
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = false, ColumnDescription = "权限名称")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 权限的详细描述，可选。
        /// </summary>
        [SugarColumn(Length = 255, IsNullable = true, ColumnDescription = "权限描述")]
        public string? Description { get; set; }


        // 导航属性：一个权限可以属于多个角色
        [SugarColumn(IsIgnore = true)]
        public List<RolePermission> RolePermissions { get; set; }
    }
}
