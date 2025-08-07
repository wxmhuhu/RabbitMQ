using MricoServices.Shared.ApiResult;
using MicroServices.Models.Dtos.RBACDtos;

namespace MricoServices.Application.IService.RBAC
{
    
    public interface IUserService 
    {
        /// <summary>
        /// 验证用户登录信息
        /// </summary>
        Task<ApiResult> AuthenticateUserAsync(UserLoginDto userLoginDto);

        ///// <summary>
        ///// 通过Id查用户
        ///// </summary>
        //Task<ApiResult<UserDto>> GetUserByIdAsync(int id);

        ///// <summary>
        ///// 通过查询条件查用户
        ///// </summary>
        //Task<ApiResult<ApiPaging<List<UserDto>>>> GetPagedUsersAsync(UserSearch search);

        ///// <summary>
        ///// 获取用户列表（分页）
        ///// </summary>
        ///// <returns></returns>
        //Task<ApiResult<ApiPaging<List<UserDto>>>> PageUserAsync();

        /// <summary>
        /// 新增用户信息
        /// </summary>
        Task<ApiResult<UserDto>> CreateUserAsync(CreateUpdateUserDto createUpdateUserDto);
        Task<ApiResult<ApiPaging<List<UserDto>>>> GetAllUserAsync(UserSearch userSearch);

        ///// <summary>
        ///// 更新用户信息
        ///// </summary>
        //Task<ApiResult<UserDto>> UpdateUserAsync(CreateUpdateUserDto  createUpdateUserDto);

        ///// <summary>
        ///// 逻辑删除
        ///// </summary>
        //Task<ApiResult> DeleteUserAsync(int id);

        ///// <summary>
        ///// 分配角色给用户
        ///// </summary>
        //Task<ApiResult> AssignRolesToUserAsync(int userId, List<int> roleIds);

        ///// <summary>
        ///// 移除用户的角色
        ///// </summary>
        //Task<ApiResult> RemoveRolesFromUserAsync(int userId, List<int> roleIds);

    }
}
