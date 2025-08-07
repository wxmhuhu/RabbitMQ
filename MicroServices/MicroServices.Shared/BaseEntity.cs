using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MricoServices.Shared
{
    public abstract class BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] // 假设所有主键都是自增的
        public int Id { get; set; }
    }
}
