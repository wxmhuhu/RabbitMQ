using MricoServices.Shared;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Models.Dtos.RBACDtos
{
    /// <summary>
    /// 权限列表 DTO
    /// </summary>
    public class PermissionDto : AuditableEntity // 如果 Permission 实体继承 AuditableEntity，DTO 也可继承
    {
        public string PermissionName { get; set; }
        public string Description { get; set; }
        public string Resource { get; set; }
        public string Action { get; set; }
    }

    /// <summary>
    /// 权限创建更新 DTO
    /// </summary>
    public class CreateUpdatePermissionDto
    {
        public string PermissionName { get; set; }
        public string Description { get; set; }
        public string Resource { get; set; }
        public string Action { get; set; }
    }

    /// <summary>
    /// 权限名和资源进行查询 DTO
    /// </summary>
    public class SearchPermissionDto : PageModel // 继承自 PageModel，用于分页查询
    {
        public string? PermissionName { get; set; }
        public string? Resource { get; set; }
        public string? Action { get; set; }
    }
}
