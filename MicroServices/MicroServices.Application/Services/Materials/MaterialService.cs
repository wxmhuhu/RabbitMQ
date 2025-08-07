using MicroServices.Application.IService.Materials;
using MicroServices.Domain.Materials;
using MicroServices.Models.Dtos.Materials;
using MicroServices.Repository.IRepository.I_Material_Repository;
using MricoServices.Shared.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Application.Services.Materials
{
public class MaterialService : IMaterialService
{
    private readonly IMaterialRepository materialRepository;

    public MaterialService(IMaterialRepository materialRepository)
    {
        this.materialRepository = materialRepository;
    }

    public async Task<ApiResult<ApiPaging<List<MaterialDto>>>> GetMaterialsAsync(MicroServices.Models.Dtos.Materials.Search search)
    {
        try
        {
            var materials = materialRepository.GetAll()
                .LeftJoin<Unite>((material, unite) =>material.Unit==unite.Id)
                .LeftJoin<TypeInfos>((material, unite, type) =>material.MaterialTypeId==type.Id)
                .LeftJoin<PropertyInfos>((material, unite, type, property) =>material.MaterialPropertyId==property.Id)
                .Select((material, unite, type, property) => new MaterialDto
                {
                    Id = material.Id,
                    MaterialCode = material.MaterialCode,
                    MaterialName = material.MaterialName,
                    Specification = material.Specification,
                    UnitName = unite.UniteName,
                    MaterialTypeName = type.TypeName,
                    MaterialPropertyName = property.MaterialPropertyName
                });


            var totalCount = await materials.CountAsync();
            var totalPage = (int)Math.Ceiling(totalCount * 1.0 / search.PageSize);

            var pagedData = await materials.OrderByDescending(material => material.Id)
                                           .Skip((search.PageIndex - 1) * search.PageSize)
                                           .Take(search.PageSize)
                                           .ToListAsync();

            var apiPagingData = new ApiPaging<List<MaterialDto>>
            {
                TotalCount = totalCount,
                TotalPage = totalPage,
                Data = pagedData
            };
            return ApiResult<ApiPaging<List<MaterialDto>>>.Success(ResultCode.Ok, apiPagingData);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
}
