using AutoMapper;
using MicroServices.Models.Dtos.RBACDtos;
using MicroServices.Models.Dtos.Reportworks;
using MicroServices.Repository.IRepository.I_RBAC_Repository;
using MricoServices.Application.IService.RBAC;
using MricoServices.Domain.RBAC;
using MricoServices.Shared.ApiResult;

namespace MricoServices.Application.Services.RBAC
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserService(IUserRepository _userRepository,IMapper mapper)
        {
            userRepository = _userRepository;
            this.mapper = mapper;
        }
        /// <summary>
        /// 登录获取用户信息
        /// </summary>
        /// <param name="userLoginDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApiResult> AuthenticateUserAsync(UserLoginDto userLoginDto)
        {
            // 1. 基本输入验证 (Service 层做更严格的DTO验证，但这里可以精简)
            if (userLoginDto == null || string.IsNullOrWhiteSpace(userLoginDto.LoginName) || string.IsNullOrWhiteSpace(userLoginDto.LoginPwd))
            {
                return ApiResult.Fail(ResultCode.Fail, "用户名或密码不能为空。");
            }
            try
            {
                // 2. 调用 UserRepository 进行登录验证
                var list = await userRepository.UserLogin(userLoginDto);

                // 3. 检查 UserRepository 返回的结果
                if (!list.IsSuc)
                {
                    // 如果 UserRepository 认证失败，则直接返回其失败信息
                    return ApiResult.Fail(list.code, list.msg);
                }
                // 4. 如果 UserRepository 认证成功，则获取用户信息
                var userDto = list.data;
                if (userDto == null) // 这是一种防御性检查，理论上 authResult.Data 在 IsSuccess 为 true 时不应为 null
                {
                    return ApiResult.Fail(ResultCode.Fail, "登录成功但获取用户信息失败。");
                }

                return ApiResult.Success(ResultCode.Ok); // 返回成功结果和用户信息
            }
            catch (Exception ex)
            {
                // 记录 Service 层可能发生的任何异常
                Console.WriteLine($"UserService.AuthenticateUserAsync 发生异常: {ex.Message}");
                return ApiResult.Fail(ResultCode.Fail, "登录过程中发生系统错误，请稍后再试。"); // 使用一个更通用的错误码
            }
        }

        /// <summary>
        /// 创建新用户 (业务逻辑的骨架)
        /// </summary>
        /// <param name="request">包含用户创建信息的请求DTO</param>
        /// <returns>操作结果</returns>
        public async Task<ApiResult<UserDto>> CreateUserAsync(CreateUpdateUserDto createUpdateUserDto)
        {
            try
            {
                var list = await userRepository.AddAsync(mapper.Map<User>(createUpdateUserDto));

                return list > 0
                    ? ApiResult<UserDto>.Success(ResultCode.Ok,mapper.Map<UserDto>(createUpdateUserDto))
                    : ApiResult<UserDto>.Fail(ResultCode.Fail,"创建失败");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 用户表显示
        /// </summary>
        /// <param name="userSearch"></param>
        /// <returns></returns>
        public async Task<ApiResult<ApiPaging<List<UserDto>>>> GetAllUserAsync(UserSearch userSearch)
        {
            try
            {
                var list = userRepository.GetAll().Where(x => !x.IsDeleted);
                var query = list.ToList();
                if (!string.IsNullOrEmpty(userSearch.Username))
                {
                    query = query.Where(x => x.Username.Contains(userSearch.Username)).ToList();
                }
                if (!string.IsNullOrEmpty(userSearch.Email))
                {
                    query = query.Where(x => x.Email.Contains(userSearch.Email)).ToList();
                }
                // 计算总数和总页数
                var totalCount = query.Count();
                var totalPage = (int)Math.Ceiling(totalCount * 1.0 / userSearch.PageSize);
                // 分页查询数据
                var pageData = query.OrderByDescending(x => x.Id).Skip((userSearch.PageIndex - 1) * userSearch.PageSize).Take(userSearch.PageSize);
                //// 映射为DTO
                var resultData = mapper.Map<List<UserDto>>(pageData);
                // 构建分页返回对象
                var apiPagingData = new ApiPaging<List<UserDto>>
                {
                    TotalCount = totalCount,
                    TotalPage = totalPage,
                    Data = resultData
                };

                // 返回成功结果
                return ApiResult<ApiPaging<List<UserDto>>>.Success(ResultCode.Ok, apiPagingData);
            }
            catch (Exception ex)
            {
                // 记录日志（实际项目中应添加日志记录）
                // logger.LogError(ex, "获取报工记录列表时发生异常");
                // 返回错误结果
                throw;
            }
        }
    }
}
