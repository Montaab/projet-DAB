using System;
using DAL.DTOs;

namespace Service.Services;

public interface IProfileService
{
    Task<ProfileDto> GetProfileByIdAsync(int id);
    Task<IEnumerable<ProfileDto>> GetAllProfilesAsync();
    Task<ProfileDto> CreateProfileAsync(ProfileCreateDto profileCreateDto);
    Task<ProfileDto> UpdateProfileAsync(ProfileUpdateDto profileUpdateDto);
    Task DeleteProfileAsync(int id);
}
