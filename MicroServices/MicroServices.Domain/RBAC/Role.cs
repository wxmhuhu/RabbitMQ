using MricoServices.Shared;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MricoServices.Domain.RBAC
{
    // 角色表
    public class Role : AuditableEntity
    {
        [SugarColumn(Length = 50, ColumnDescription = "角色名称", IsNullable = false)]
        public string RoleName { get; set; } = string.Empty;

        [SugarColumn(Length = 200, ColumnDescription = "角色描述", IsNullable = true)]
        public string? Description { get; set; }

        [SugarColumn(Length = 50, ColumnDescription = "角色编码", IsNullable = false)]
        public string RoleCode { get; set; } = string.Empty;

        [SugarColumn(ColumnDescription = "角色状态", DefaultValue = "1")]
        public bool IsEnabled { get; set; } = true;


       
        [SugarColumn(IsIgnore = true)]  
        public List<UserRole> UserRoles { get; set; }

        
        [SugarColumn(IsIgnore = true)] 
        public List<RolePermission> RolePermissions { get; set; }

        
        [SugarColumn(IsIgnore = true)] 
        public List<RoleMenu> RoleMenus { get; set; }
    }


    //  (角色权限中间表)
    public class RolePermission 
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int RoleId { get; set; }
        [SugarColumn(IsPrimaryKey = true)]
        public int PermissionId { get; set; }

   
        [SugarColumn(IsIgnore = true)]
        public Role Role { get; set; }
        [SugarColumn(IsIgnore = true)]
        public Permission Permission { get; set; }
    }

    //  (角色菜单中间表)
    public class RoleMenu
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int RoleId { get; set; }
        [SugarColumn(IsPrimaryKey = true)]
        public int MenuId { get; set; }

      
        [SugarColumn(IsIgnore = true)]
        public Role Role { get; set; }
        [SugarColumn(IsIgnore = true)]
        public Menu Menu { get; set; }
    }
}
