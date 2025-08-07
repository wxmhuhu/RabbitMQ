using MicroServices.Models.Dtos.RBACDtos;
using MricoServices.Domain.RBAC;
using MricoServices.Repository.IRepository;
using MricoServices.Shared.ApiResult;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Repository.IRepository.I_RBAC_Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <returns></returns>
        Task<ApiResult<UserDto>> UserLogin(UserLoginDto userLoginDto);

        ///// <summary>
        ///// 根据用户名获取用户（通常用于登录验证）
        ///// </summary>
        ///// <param name="username">用户名</param>
        ///// <returns>用户实体，如果不存在则返回 null</returns>
        //Task<User> GetUserByUsernameAsync(string username);

        ///// <summary>
        ///// 根据用户ID获取用户及关联的角色信息
        ///// </summary> 
        ///// <param name="userId">用户ID</param>
        ///// <returns>包含角色导航属性的用户实体</returns>
        //Task<User> GetUserWithRolesAsync(int userId);

        ///// <summary>
        ///// 分页获取用户列表及关联的角色信息
        ///// </summary>
        ///// <param name="search">用户查询条件和分页信息</param>
        ///// <returns>分页结果，包含用户实体列表及总数总页数</returns>
        //Task<ApiPaging<List<User>>> GetPagedUsersWithRolesAsync(UserSearch search);

        ///// <summary>
        ///// 检查用户名是否已存在
        ///// </summary>
        ///// <param name="username">用户名</param>
        ///// <param name="excludeId">可选参数：排除的用户ID（用于更新时检查）</param>
        ///// <returns>如果用户名存在则返回 true</returns>
        //Task<bool> IsUsernameExistAsync(string username, int? excludeId = null);

        ///// <summary>
        ///// 检查邮箱是否已存在
        ///// </summary>
        ///// <param name="email">邮箱</param>
        ///// <param name="excludeId">可选参数：排除的用户ID（用于更新时检查）</param>
        ///// <returns>如果邮箱存在则返回 true</returns>
        //Task<bool> IsEmailExistAsync(string email, int? excludeId = null);
    }
}
