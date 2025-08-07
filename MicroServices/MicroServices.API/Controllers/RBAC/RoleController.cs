using MicroServices.Models.Dtos.RBACDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MricoServices.Application.IService.RBAC;
using MricoServices.Shared.ApiResult;

namespace MicroServices.API.Controllers.RBAC
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService roleService;

        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="searchRoleDto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ApiPaging<List<RoleDto>>>> GetAllRolesAsync([FromQuery]SearchRoleDto searchRoleDto)
        {
            return await roleService.GetAllRolesAsync(searchRoleDto);
        }
        /// <summary>
        /// 角色添加
        /// </summary>
        /// <param name="createUpdateRoleDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> AddRoleAsync(CreateUpdateRoleDto createUpdateRoleDto)
        {
            return await roleService.AddRoleAsync(createUpdateRoleDto);
        }
        /// <summary>
        /// 角色软删除
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ApiResult> DeleteRoleAsync(int roleId)
        {
            return await roleService.DeleteRoleAsync(roleId);
        }
        /// <summary>
        /// 角色更新
        /// </summary>
        /// <param name="createUpdateRoleDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ApiResult> UpdateRoleAsync(int roleid,CreateUpdateRoleDto createUpdateRoleDto)
        {
            return await roleService.UpdateRoleAsync(roleid,createUpdateRoleDto);
        }
    }
}
