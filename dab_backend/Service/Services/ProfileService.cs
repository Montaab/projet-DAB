using System;
using DAL.DTOs;
using DAL.Entities;
using DAL.Repositories;

namespace Service.Services;

public class ProfileService : IProfileService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProfileService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ProfileDto> GetProfileByIdAsync(int id)
    {
        var profile = await _unitOfWork.Profiles.GetByIdAsync(id);
        if (profile == null)
            throw new KeyNotFoundException($"Profile with id {id} not found");

        return MapToDto(profile);
    }

    public async Task<IEnumerable<ProfileDto>> GetAllProfilesAsync()
    {
        var profiles = await _unitOfWork.Profiles.GetAllAsync();
        return profiles.Select(MapToDto).ToList();
    }

    public async Task<ProfileDto> CreateProfileAsync(ProfileCreateDto profileCreateDto)
    {
        var profile = new Profile
        {
            Permission = profileCreateDto.Permission
        };

        var createdProfile = await _unitOfWork.Profiles.AddAsync(profile);
        return MapToDto(createdProfile);
    }

    public async Task<ProfileDto> UpdateProfileAsync(ProfileUpdateDto profileUpdateDto)
    {
        var profile = await _unitOfWork.Profiles.GetByIdAsync(profileUpdateDto.Id);
        if (profile == null)
            throw new KeyNotFoundException($"Profile with id {profileUpdateDto.Id} not found");

        profile.Permission = profileUpdateDto.Permission;

        var updatedProfile = await _unitOfWork.Profiles.UpdateAsync(profile);
        return MapToDto(updatedProfile);
    }

    public async Task DeleteProfileAsync(int id)
    {
        await _unitOfWork.Profiles.DeleteAsync(id);
    }

    private static ProfileDto MapToDto(Profile profile)
    {
        return new ProfileDto
        {
            Id = profile.Id,
            Permission = profile.Permission
        };
    }
}
