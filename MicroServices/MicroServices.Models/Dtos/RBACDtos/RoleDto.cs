using MricoServices.Shared;
using MricoServices.Shared.ApiResult;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Models.Dtos.RBACDtos
{
    /// <summary>
    /// 角色列表
    /// </summary>
    public class RoleDto : AuditableEntity
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
    }
    /// <summary>
    /// 角色创建更新DTO
    /// </summary>
    public class CreateUpdateRoleDto
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
    }
    /// <summary>
    /// 角色名进行查询DTO
    /// </summary>
    public class  SearchRoleDto: PageModel
    {
        public string? RoleName { get; set; }
    }
    /// <summary>
    /// 给角色分配用户,用于添加和更新用户
    /// </summary>
    public class RoleIdToAddAndUpdateUserDto
    {
        public List<int> UserIds { get; set; } = new List<int>();
    }
    /// <summary>
    /// 给角色分配权限,用于添加和更新权限
    /// </summary>
    public class RoleIdToAddAndUpdatePermissionDto
    {
        public List<int> PermissionIds { get; set; } = new List<int>();
    }

    public class RoleIdToAddAndUpdateMenuDto
    {
        public List<int> MenuIds { get; set; } = new List<int>();
    }
}
