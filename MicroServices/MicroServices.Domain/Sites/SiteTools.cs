using MricoServices.Shared;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Domain.Sites
{
    /// <summary>
    /// 站点工具表
    /// </summary>
    [SugarTable("SiteTools", TableDescription = "站点工具表")]
    public class SiteTools : AuditableEntity
    {
        [SugarColumn(ColumnDescription = "工装夹具编号")]
        public string ToolsCode { get; set; }

        [SugarColumn(ColumnDescription = "站点外键id")]
        public int SitesId { get; set; }

        [SugarColumn(ColumnDescription = "工装夹具名称")]
        public string ToolsName { get; set; }

        [SugarColumn(ColumnDescription = "工装夹具型号")]
        public string ToolsSize { get; set; }

        [SugarColumn(ColumnDescription = "数量")]
        public int Count { get; set; }

        [SugarColumn(ColumnDescription = "工装夹具类型id")]
        public int ToolsTypeId { get; set; }
    }
}
