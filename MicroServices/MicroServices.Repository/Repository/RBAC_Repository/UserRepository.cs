using MicroServices.Models.Dtos.RBACDtos;
using MicroServices.Repository.IRepository.I_RBAC_Repository;
using MricoServices.Domain.RBAC;
using MricoServices.Repository.Repository;
using MricoServices.Shared.ApiResult;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Repository.Repository.RBAC_Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ISqlSugarClient db) : base(db)
        {
            // BaseRepository 的构造函数已经设置了 base.Context = db;
            // 所以这里不需要额外的操作
        }

        public async Task<ApiResult<UserDto>> UserLogin(UserLoginDto userLoginDto)
        {
            // 1. 基本输入验证
            if (userLoginDto == null || string.IsNullOrWhiteSpace(userLoginDto.LoginName) || string.IsNullOrWhiteSpace(userLoginDto.LoginPwd))
            {
                return ApiResult<UserDto>.Fail(ResultCode.Fail,"用户名或密码不能为空。");
            }

            try
            {
                // 2. 根据登录名查询用户
                var user = await base.Context.Queryable<User>()
                                    .Where(u => u.Username == userLoginDto.LoginName)
                                    .FirstAsync();

                // 3. 判断用户是否存在
                if (user == null)
                {
                    // 出于安全考虑，无论用户名错还是密码错，都返回统一的错误信息
                    return ApiResult<UserDto>.Fail(ResultCode.Fail,"用户名或密码错误。");
                }

                // 4. 验证密码 (!!! 警告：直接明文比较密码，极度不安全 !!!)
                bool isPasswordCorrect = user.PasswordHash == userLoginDto.LoginPwd;

                if (!isPasswordCorrect)
                {
                    return ApiResult<UserDto>.Fail(ResultCode.Fail, "用户名或密码错误。");
                }

                // 5. 检查账户是否激活（可选，但推荐）
                if (!user.IsActive)
                {
                    return ApiResult<UserDto>.Fail(ResultCode.Fail, "账户已被禁用，请联系管理员。");
                }
    

                // --- 6. 查询用户的角色ID列表 ---
                // 通过 UserRole 关联表查询该用户的所有 RoleId
                var roleIds = await base.Context.Queryable<UserRole>()
                                                .Where(ur => ur.UserId == user.Id) // 根据用户ID筛选
                                                .Select(ur => ur.RoleId) // 只选择 RoleId 字段
                                                .ToListAsync();

                // 7. 更新最后登录时间 (可选，但推荐)
                user.LastLoginAt = DateTime.Now;
                await base.Context.Updateable(user)
                         .UpdateColumns(it => it.LastLoginAt)
                         .ExecuteCommandAsync();


                // 8. 登录成功，映射到 UserDto 并返回
                var userDto = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    IsActive = user.IsActive,
                    LastLoginAt = user.LastLoginAt,
                    RoleId = roleIds
                };

                return ApiResult<UserDto>.Success(ResultCode.Ok, userDto);
            }
            catch (Exception ex)
            {
                // 简单的异常捕获，实际项目中应使用日志框架
                Console.WriteLine($"登录操作发生异常: {ex.Message}");
                return ApiResult<UserDto>.Fail(ResultCode.Fail, "登录过程中发生系统错误，请稍后再试。");
            }
        }

        ///// <summary>
        ///// 根据用户名获取用户（通常用于登录验证）
        ///// </summary>
        ///// <param name="username">用户名</param>
        ///// <returns>用户实体，如果不存在则返回 null</returns>
        //public async Task<User> GetUserByUsernameAsync(string username)
        //{
        //    return await Context.Queryable<User>()
        //                                   .Includes<UserRole>(u => u.UserRoles) // 显式指定 UserRole
        //                                   //.ThenIncludes<Role>(ur => ur.Role)   // 显式指定 Role
        //                                   .FirstAsync(u => u.Username == username && !u.IsDeleted);
        //}

        //public async Task<User> GetUserWithRolesAsync(int userId)
        //{
        //    return await Context.Queryable<User>()
        //                        .Includes(u => u.UserRoles)
        //                        //.ThenIncludes(ur => ur.Role)
        //                        .FirstAsync(u => u.Id == userId && !u.IsDeleted);
        //}

        //public async Task<ApiPaging<List<User>>> GetPagedUsersWithRolesAsync(UserSearch search)
        //{
        //    var query = Context.Queryable<User>()
        //                       .Includes(u => u.UserRoles)
        //                       //.ThenIncludes(ur => ur.Role)
        //                       .Where(u => !u.IsDeleted);

        //    if (!string.IsNullOrWhiteSpace(search.Username))
        //    {
        //        query = query.Where(u => u.Username.Contains(search.Username));
        //    }
        //    if (!string.IsNullOrWhiteSpace(search.Email))
        //    {
        //        query = query.Where(u => u.Email.Contains(search.Email));
        //    }
        //    //if (search.IsActive.HasValue)
        //    //{
        //    //    query = query.Where(u => u.IsActive == search.IsActive.Value);
        //    //}

        //    query = query.OrderBy(u => u.Id, OrderByType.Desc);

        //    var totalCount = 0;
        //    var list = await query.ToPageListAsync(search.PageIndex, search.PageSize, RefAsync.Create(totalCount));

        //    var totalPages = (int)Math.Ceiling(totalCount / (double)search.PageSize);

        //    return new ApiPaging<List<User>>
        //    {
        //        Data = list,
        //        TotleCount = totalCount,
        //        TotlePage = totalPages
        //    };
        //}

        //public async Task<bool> IsUsernameExistAsync(string username, int? excludeId = null)
        //{
        //    var query = Context.Queryable<User>().Where(u => u.Username == username && !u.IsDeleted);
        //    if (excludeId.HasValue)
        //    {
        //        query = query.Where(u => u.Id != excludeId.Value);
        //    }
        //    return await query.AnyAsync();
        //}

        //public async Task<bool> IsEmailExistAsync(string email, int? excludeId = null)
        //{
        //    if (string.IsNullOrWhiteSpace(email)) return false;
        //    var query = Context.Queryable<User>().Where(u => u.Email == email && !u.IsDeleted);
        //    if (excludeId.HasValue)
        //    {
        //        query = query.Where(u => u.Id != excludeId.Value);
        //    }
        //    return await query.AnyAsync();
        //}
    }
}
