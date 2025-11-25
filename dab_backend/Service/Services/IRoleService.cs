using System;
using DAL.DTOs;

namespace Service.Services;

public interface IRoleService
{
    Task<RoleDto> GetRoleByIdAsync(int id);
    Task<IEnumerable<RoleDto>> GetAllRolesAsync();
    Task<RoleDto> CreateRoleAsync(RoleCreateDto roleCreateDto);
    Task<RoleDto> UpdateRoleAsync(RoleUpdateDto roleUpdateDto);
    Task DeleteRoleAsync(int id);
}
