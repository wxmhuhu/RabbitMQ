using MicroServices.Models.Dtos.RBACDtos;
using MricoServices.Shared.ApiResult;

namespace MricoServices.Application.IService.RBAC
{
    // Interfaces/IRoleService.cs
    public interface IRoleService
    {
        /// <summary>
        /// 获取角色列表信息
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<ApiPaging<List<RoleDto>>>> GetAllRolesAsync(SearchRoleDto searchRoleDto);
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="createUpdateRoleDto"></param>
        /// <returns></returns>
        Task<ApiResult> AddRoleAsync(CreateUpdateRoleDto createUpdateRoleDto);
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="createUpdateRoleDto"></param>
        /// <returns></returns>
        Task<ApiResult<RoleDto>> UpdateRoleAsync(int roleid,CreateUpdateRoleDto createUpdateRoleDto);
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<ApiResult> DeleteRoleAsync(int roleId);
        /// <summary>
        /// 给角色分配用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<ApiResult> GetRoleIdToAddUser(int roleId, RoleIdToAddAndUpdateUserDto roleIdToAddAndUpdateUserDto);
        /// <summary>
        /// 给用户更新角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleIdToAddAndUpdateUserDto"></param>
        /// <returns></returns>
        Task<ApiResult> GetRoleToUpdateAddUser(int roleId, RoleIdToAddAndUpdateUserDto roleIdToAddAndUpdateUserDto);
        /// <summary>
        /// 给角色添加权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<ApiResult> GetRoleIdToAddPermission(int roleId, RoleIdToAddAndUpdatePermissionDto roleIdToAddAndUpdatePermissionDto);
        /// <summary>
        /// 给角色更新权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleIdToAddAndUpdatePermissionDto"></param>
        /// <returns></returns>
        Task<ApiResult> GetRoleIdToUpdatePermission(int roleId, RoleIdToAddAndUpdatePermissionDto roleIdToAddAndUpdatePermissionDto);
        /// <summary>
        /// 给角色添加菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleIdToAddAndUpdateMenuDto"></param>
        /// <returns></returns>
        Task<ApiResult> GetRoleIdToAddMenu(int roleId, RoleIdToAddAndUpdateMenuDto roleIdToAddAndUpdateMenuDto);
        /// <summary>
        /// 给角色更新菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleIdToAddAndUpdateMenuDto"></param>
        /// <returns></returns>
        Task<ApiResult> GetRoleIdToUpdateMenu(int roleId, RoleIdToAddAndUpdateMenuDto roleIdToAddAndUpdateMenuDto);
    }
}
