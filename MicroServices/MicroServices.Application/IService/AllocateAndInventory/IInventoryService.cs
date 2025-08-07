using MicroServices.Domain.Inventory;
using MicroServices.Models.Dtos.AllocateAndInventory.Inventory;
using MricoServices.Domain.RBAC;
using MricoServices.Shared.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Application.IService
{
    /// <summary>
    /// 盘点服务接口  
    /// </summary>
    public interface IInventoryService
    {
        /// <summary>
        /// 盘点添加
        /// </summary>
        Task<ApiResult> CreateInventoryAsync(CreateUpdateInventoryDto dto);
        /// <summary>
        /// 盘点删除--软删除
        /// </summary>
        Task<ApiResult> DeleteInventoryAsync(int inventoryId);
        /// <summary>
        ///获取盘点列表 
        /// </summary>
         Task<ApiResult<ApiPaging<List<InventoryDto>>>> GetInventoryAsync(Search search);
        /// <summary>
        /// 反填盘点列表
        /// </summary>
        Task<ApiResult<InventoryDto>> GetInventoryOneAsync(int Id); 
        /// <summary>
        /// 反填盘点--明细列表
        /// </summary>
        Task<ApiResult<List<InventoryDetailDto>>> GetInventoryDetailOneAsync(int Id); 
        /// <summary>
        ///修改盘点列表 
        /// </summary>
        Task<ApiResult> UpdateInventoryAsync(CreateUpdateInventoryDto dto);
        /// <summary>
        ///审核盘点列表--批量审核
        ///     修改审核字段--为已审核状态
        /// </summary>
        Task<ApiResult> ReviewInventoryAsync(List<int> Ids); 
    }
}
