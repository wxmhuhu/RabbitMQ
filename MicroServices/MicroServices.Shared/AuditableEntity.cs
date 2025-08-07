using SqlSugar;

namespace MricoServices.Shared
{
    public class AuditableEntity:BaseEntity
    {
        // 审计字段
        public DateTime CreatedAt { get; set; } = DateTime.Now; // 创建时间

        [SugarColumn(IsNullable = true)]
        public int? CreatedBy { get; set; } // 创建人ID

        [SugarColumn(IsNullable = true)]
        public string? CreatedByUserName { get; set; } // 创建人用户名

        [SugarColumn(IsNullable = true)]
        public DateTime? UpdatedAt { get; set; } // 更新时间

        [SugarColumn(IsNullable = true)]
        public int? UpdatedBy { get; set; } // 更新人ID

        [SugarColumn(IsNullable = true)]
        public string? UpdatedByUserName { get; set; } // 更新人用户名


        // 软删除字段
        [SugarColumn(IsNullable = true)]
        public bool IsDeleted { get; set; } = false; // 默认为 false，表示未删除

        [SugarColumn(IsNullable = true)]
        public DateTime? DeletedAt { get; set; } // 删除时间戳

        [SugarColumn(IsNullable = true)]
        public int? DeletedBy { get; set; } // 删除人ID

        [SugarColumn(IsNullable = true)]
        public string? DeletedByUserName { get; set; } // 删除人用户名
    }
}
