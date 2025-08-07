using MricoServices.Domain.RBAC;
using MricoServices.Shared;
using MricoServices.Shared.ApiResult;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Models.Dtos.RBACDtos
{
    /// <summary>
    /// 用户登录后的JWT
    /// </summary>
    public class TokenDto : UserDto
    {
        public string Token { get; set; }
    }

    /// <summary>
    /// 用户信息Dto
    /// </summary>
    public class UserDto : AuditableEntity
    {
        [SugarColumn(Length = 50, ColumnDescription = "用户名", IsNullable = false)]
        public string Username { get; set; } = string.Empty;

        [SugarColumn(Length = 100, ColumnDescription = "邮箱", IsNullable = true)]
        public string? Email { get; set; }

        [SugarColumn(Length = 20, ColumnDescription = "手机号码", IsNullable = true)]
        public string? PhoneNumber { get; set; }

        [SugarColumn(ColumnDescription = "账户是否激活", DefaultValue = "1")] // 默认值为1 (true)
        public bool IsActive { get; set; } = true;

        [SugarColumn(ColumnDescription = "最后登录时间", IsNullable = true)]
        public DateTime? LastLoginAt { get; set; }

        [SugarColumn(ColumnDescription = "用户角色ID列表", IsNullable = true, IsIgnore = true)] 
        public List<int>RoleId { get; set; }
    }
    /// <summary>
    /// 用户创建或更新Dto
    /// </summary>
    public class CreateUpdateUserDto
    {
        [SugarColumn(Length = 50, ColumnDescription = "用户名", IsNullable = false)]
        public string Username { get; set; } = string.Empty;

        [SugarColumn(Length = 255, ColumnDescription = "密码哈希值", IsNullable = false)]
        public string PasswordHash { get; set; } = string.Empty;

        [SugarColumn(Length = 50, ColumnDescription = "真实姓名", IsNullable = true)]
        public string? NickName { get; set; }

        [SugarColumn(Length = 100, ColumnDescription = "邮箱", IsNullable = true)]
        public string? Email { get; set; }

        [SugarColumn(Length = 20, ColumnDescription = "手机号码", IsNullable = true)]
        public string? PhoneNumber { get; set; }
    }

    /// <summary>
    /// 用户查询 DTO
    /// </summary>
    public class UserSearch : PageModel
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
    }
}
