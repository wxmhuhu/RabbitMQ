using MicroServices.Domain.Allocate;
using MicroServices.Models.Dtos.AllocateAndInventory.Allocate;
using MicroServices.Models.Dtos.AllocateAndInventory.Inventory;
using MricoServices.Shared.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Application.IService.AllocateAndInventory
{
    public interface IAllocateService//Allocate
    {
        //添加调拨
        Task<ApiResult> AddAllocateAsync(CreateUpdateInventoryDto dto);
        //查询调拨
        Task<ApiResult<ApiPaging<List<AllocateDto>>>> GetAllocateAsync(AllocateSearch search);

        //修改调拨
        Task<ApiResult> UpdateAllocateAsync(CreateUpdateInventoryDto dto);

        //查询调拨明细表
        Task<ApiResult<ApiPaging<List<AllocateDetailDto>>>> GetAllocateDetailAsync(CreateUpdateAllocateDetailDto dto);

        //反填调拨明细表
        Task<ApiResult<List<AllocateDetailDto>>> GetOneAllocateDetailAsync(int Id);
        //反填调拨
        Task<ApiResult<AllocateDto>> GetOneAllocateAsync(int Id);
        //批量导出调拨
        Task<ApiResult> ExportAllocateAsync(AllocateSearch search);
        //审核调拨--修改调拨表的审核状态，审核时间，审核人--批量操作
        Task<ApiResult> ReviewAllocateAsync(List<int> Ids);
    }
}
