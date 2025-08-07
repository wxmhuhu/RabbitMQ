using MicroServices.Application.IService;
using MicroServices.Models.Dtos.AllocateAndInventory.Inventory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MricoServices.Shared.ApiResult;

namespace MicroServices.API.Controllers.Inventory
{
    /// <summary>
    /// 盘点管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
       public class InventoryController : ControllerBase
    {
        private readonly IInventoryService inventoryService;
        private readonly IInventoryService inventoryDetailService;

        public InventoryController(IInventoryService inventoryService, IInventoryService inventoryDetailService)
        {
            this.inventoryService = inventoryService;
            this.inventoryDetailService = inventoryDetailService;
        }
        /// <summary>
        /// 获取盘点列表
        /// </summary> 
        [HttpGet]
        public async Task<ApiResult<ApiPaging<List<InventoryDto>>>> GetAllInventoryAsync([FromQuery] Search search)
        {
            return await inventoryService.GetInventoryAsync(search);
        }

        /// <summary>
        /// 反填盘点 
        /// </summary>
        [HttpGet]
        public async Task<ApiResult<InventoryDto>> GetOneInventoryAsync([FromQuery] int Id)
        {
            return await inventoryService.GetInventoryOneAsync(Id);
        }

        /// <summary>
        /// 反填盘点明细
        /// </summary>
        [HttpGet]
        public async Task<ApiResult<List<InventoryDetailDto>>> GetInventoryDetailOneAsync([FromQuery] int Id)
        {
            return await inventoryDetailService.GetInventoryDetailOneAsync(Id);
        }


        /// <summary>
        /// 审核盘点--批量
        /// </summary>
        [HttpGet]
        public async Task<ApiResult> ReviewInventoryAsync([FromQuery] List<int> Ids)
        {
            return await inventoryService.ReviewInventoryAsync(Ids);
        }

        /// <summary>
        /// 创建盘点
        /// </summary>
        [HttpPost]
        public async Task<ApiResult> CreateInventoryAsync([FromBody] CreateUpdateInventoryDto dto)
        {
            return await inventoryService.CreateInventoryAsync(dto);
        }
    }
}
