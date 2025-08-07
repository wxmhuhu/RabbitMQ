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
    /// 站点表
    /// </summary>
    [SugarTable("Sites", TableDescription = "站点表")]
    public class Sites : AuditableEntity
    {
        [SugarColumn(ColumnDescription = "站点编号")]
        public string SiteCode { get; set; }

        [SugarColumn(ColumnDescription = "站点名称")]
        public string SiteName { get; set; }

        [SugarColumn(ColumnDescription = "站点类型")]
        public string SiteType { get; set; }

        [SugarColumn(ColumnDescription = "车间外键id")]
        public int WorkShopId { get; set; }

        [SugarColumn(ColumnDescription = "生产线外键id")]
        public int ProductionlineId { get; set; }

        [SugarColumn(ColumnDescription = "工序id")]
        public int FactoryFloorId { get; set; }

        [SugarColumn(ColumnDescription = "站点设备id")]
        public int SiteEquipmentId { get; set; }

        [SugarColumn(ColumnDescription = "站点工具id")]
        public int SiteToolsId { get; set; }

        [SugarColumn(ColumnDescription = "站点位置")]
        public string SitesAddress { get; set; }

        [SugarColumn(ColumnDescription = "状态 true:启用,false:禁用")]
        public bool State { get; set; }

        [SugarColumn(ColumnDescription = "站点描述")]
        public string SitesDesc { get; set; }

        [SugarColumn(ColumnDescription = "备注")]
        public string Remark { get; set; }

        //[SugarColumn(ColumnDescription = "站点设备")]
        //public List<SiteEquipment> SiteEquipments { get; set; } = new();

        //[SugarColumn(ColumnDescription = "站点工具")]
        //public List<SiteTools> SiteTools { get; set; } = new();

        //[SugarColumn(ColumnDescription = "站点人员")]
        //public List<Position> Positions { get; set; } = new();
    }
}
