using MicroServices.Models.Dtos.RBACDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MricoServices.Application.IService.RBAC;
using MricoServices.Shared.ApiResult;

namespace MicroServices.API.Controllers.RBAC
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userLoginDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> AuthenticateUserAsync(UserLoginDto userLoginDto)
        {
            return await userService.AuthenticateUserAsync(userLoginDto);
        }

        /// <summary>
        /// 用户添加
        /// </summary>
        /// <param name="createUpdateUserDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<UserDto>> CreateUserAsync(CreateUpdateUserDto createUpdateUserDto)
        {
            return await userService.CreateUserAsync(createUpdateUserDto);
        }
        /// <summary>
        /// 用户显示
        /// </summary>
        /// <param name="userSearch"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ApiPaging<List<UserDto>>>> GetAllUserAsync(UserSearch userSearch)
        {
            return await userService.GetAllUserAsync(userSearch);
        }
    }
}
