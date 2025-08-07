using MricoServices.Shared;
using SqlSugar;

namespace MicroServices.Models.Dtos.RBACDtos
{
    /// <summary>
    /// 菜单列表 DTO
    /// </summary>
    public class MenuDto : AuditableEntity // 如果 Menu 实体继承 AuditableEntity，DTO 也可继承
    {
        public string MenuName { get; set; }
        public string Path { get; set; }
        public string Icon { get; set; }
        public int ParentId { get; set; }
        public int OrderNum { get; set; }
        public int MenuType { get; set; }
        public bool IsHidden { get; set; }
        public string Component { get; set; }
        public List<MenuDto>? Children { get; set; } // 用于树形结构，可空
    }

    /// <summary>
    /// 菜单创建更新 DTO
    /// </summary>
    public class CreateUpdateMenuDto
    {
        public string MenuName { get; set; }
        public string Path { get; set; }
        public string Icon { get; set; }
        public int ParentId { get; set; }
        public int OrderNum { get; set; }
        public int MenuType { get; set; }
        public bool IsHidden { get; set; }
        public string Component { get; set; }
    }

    /// <summary>
    /// 菜单名、路径、父ID进行查询 DTO
    /// </summary>
    public class SearchMenuDto : PageModel // 继承自 PageModel，用于分页查询
    {
        public string? MenuName { get; set; }
        public string? Path { get; set; }
        public int? ParentId { get; set; } // 支持按父ID查询子菜单
        public int? MenuType { get; set; }
        public bool? IsHidden { get; set; }
    }
}
