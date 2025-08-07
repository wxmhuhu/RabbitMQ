using MicroServices.Models.Dtos.Materials;
using MricoServices.Shared.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Application.IService.Materials
{
    public interface IMaterialService
    {
        Task<ApiResult<ApiPaging<List<MaterialDto>>>> GetMaterialsAsync(MicroServices.Models.Dtos.Materials.Search search);
    }
}
