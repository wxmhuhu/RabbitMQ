using MicroServices.Models.Dtos.RBACDtos;
using MricoServices.Domain.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MricoServices.Application.IService.RBAC
{
    
    public interface IMenuService
    {
        Task<MenuDto> GetMenuByIdAsync(int menuId);
        Task<List<MenuDto>> GetAllMenusAsync();
        Task AddMenuAsync(CreateUpdateMenuDto createUpdateMenuDto);
        Task UpdateMenuAsync(CreateUpdateMenuDto createUpdateMenuDto);
        Task DeleteMenuAsync(int menuId); // 考虑到软删除，这里会标记 IsDeleted
        Task<List<MenuDto>> GetMenusByRoleIdsAsync(List<int> roleIds); // 根据角色获取菜单列表
    }

}
