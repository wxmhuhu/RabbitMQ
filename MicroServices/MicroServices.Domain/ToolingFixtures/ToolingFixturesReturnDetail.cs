using MricoServices.Shared;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Domain.ToolingFixtures
{ 
    /// <summary>
    /// 工具归还明细表
    /// </summary>
    [SugarTable("ToolingFixtureTypeReturnDetail")]
    public class ToolingFixturesReturnDetail : AuditableEntity
    { 

        [SugarColumn(ColumnName = " FixtureNumber ")]
        public string FixtureNumber { get; set; }  // 工装夹具编号

        [SugarColumn(ColumnName = "FixtureName")]
        public string FixtureName { get; set; }//工装夹具名称

        [SugarColumn(ColumnName = "FixtureTypeId")]
        public int FixtureTypeId { get; set; }//工装夹具类型

        [SugarColumn(ColumnName = "FixtureModelId")]
        public int FixtureModelId { get; set; }//工装夹具型号

        [SugarColumn(ColumnName = "WaraHouseId")]
        public int WaraHouseId { get; set; }//所在库位

        [SugarColumn(ColumnName = "Remark")]
        public string Remark { get; set; }// 备注

        [SugarColumn(ColumnName = "ToolingFixtureReturnId")]
        public int ToolingFixtureReturnId { get; set; }  // 外键
    } 
} 