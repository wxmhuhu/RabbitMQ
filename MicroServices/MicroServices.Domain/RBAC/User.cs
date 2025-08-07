using MricoServices.Shared;
using SqlSugar;

namespace MricoServices.Domain.RBAC
{
    // 用户表
    public class User : AuditableEntity
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

        [SugarColumn(ColumnDescription = "账户是否激活", DefaultValue = "1")] // 默认值为1 (true)
        public bool IsActive { get; set; } = true;

        [SugarColumn(ColumnDescription = "最后登录时间", IsNullable = true)]

        public DateTime? LastLoginAt { get; set; }

        [SugarColumn(ColumnDescription = "部门", IsNullable = true)]
        public int? DeptId { get; set; }

        // 导航属性：用户与角色 (多对多关系通过 UserRole 实现)
        [Navigate(NavigateType.OneToMany, "UserId")] // "UserId" 是 UserRole 类中关联 User 的外键属性名
        [SugarColumn(IsIgnore = true)] // 导航属性不需要映射到数据库列
        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }

    //  (用户角色中间表)
    public class UserRole 
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int UserId { get; set; }
        [SugarColumn(IsPrimaryKey = true)]
        public int RoleId { get; set; }

        // 导航属性：方便查询
        [Navigate(NavigateType.OneToOne, nameof(UserId))]
        [SugarColumn(IsIgnore = true)]
        public User User { get; set; }
        [Navigate(NavigateType.OneToOne, nameof(RoleId))]
        [SugarColumn(IsIgnore = true)]
        public Role Role { get; set; }
    }
}
