using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MricoServices.Shared.ApiResult
{
    /// <summary>
    /// 分页
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiPaging<T>
    {
        public int TotalCount { get; set; }
        public int TotalPage { get; set; }
        public T Data { get; set; }
    }
}
