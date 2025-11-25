using System;
using DAL.DTOs;
using DAL.Entities;
using DAL.Repositories;

namespace Service.Services;

public class RoleService : IRoleService
{
    private readonly IUnitOfWork _unitOfWork;

    public RoleService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<RoleDto> GetRoleByIdAsync(int id)
    {
        var role = await _unitOfWork.Roles.GetByIdAsync(id);
        if (role == null)
            throw new KeyNotFoundException($"Role with id {id} not found");

        return MapToDto(role);
    }

    public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
    {
        var roles = await _unitOfWork.Roles.GetAllAsync();
        return roles.Select(MapToDto).ToList();
    }

    public async Task<RoleDto> CreateRoleAsync(RoleCreateDto roleCreateDto)
    {
        var role = new Role
        {
            Nom = roleCreateDto.Nom,
            ProfileId = roleCreateDto.ProfileId
        };

        var createdRole = await _unitOfWork.Roles.AddAsync(role);
        return MapToDto(createdRole);
    }

    public async Task<RoleDto> UpdateRoleAsync(RoleUpdateDto roleUpdateDto)
    {
        var role = await _unitOfWork.Roles.GetByIdAsync(roleUpdateDto.Id);
        if (role == null)
            throw new KeyNotFoundException($"Role with id {roleUpdateDto.Id} not found");

        role.Nom = roleUpdateDto.Nom;
        role.ProfileId = roleUpdateDto.ProfileId;

        var updatedRole = await _unitOfWork.Roles.UpdateAsync(role);
        return MapToDto(updatedRole);
    }

    public async Task DeleteRoleAsync(int id)
    {
        await _unitOfWork.Roles.DeleteAsync(id);
    }

    private static RoleDto MapToDto(Role role)
    {
        return new RoleDto
        {
            Id = role.Id,
            Nom = role.Nom,
            ProfileId = role.ProfileId
        };
    }
}
