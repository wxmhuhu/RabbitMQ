using MicroServices.Application.IService.Materials;
using MicroServices.Models.Dtos.Materials;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MricoServices.Shared.ApiResult;

namespace MicroServices.API.Controllers.Materials
{
[Route("api/[controller]/[action]")]
[ApiController]
public class MaterialController : ControllerBase
{
    private readonly IMaterialService materialService;

    public MaterialController(IMaterialService materialService)
    {
        this.materialService = materialService;
    }

    [HttpGet]
    public async Task<ApiResult<ApiPaging<List<MaterialDto>>>> GetMaterialsAsync([FromQuery]Search search)
    {
        return await materialService.GetMaterialsAsync(search);
    }
}
}
