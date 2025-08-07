using MicroServices.Models.Dtos.RBACDtos;
using MricoServices.Domain.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MricoServices.Application.IService.RBAC
{
    // Interfaces/IPermissionService.cs
    public interface IPermissionService
    {
        Task<PermissionDto> GetPermissionByIdAsync(int permissionId);
        Task<List<PermissionDto>> GetAllPermissionsAsync();
        Task AddPermissionAsync(CreateUpdatePermissionDto createUpdatePermissionDto);
        Task UpdatePermissionAsync(CreateUpdatePermissionDto createUpdatePermissionDto);
        Task DeletePermissionAsync(int permissionId);
    }
}
