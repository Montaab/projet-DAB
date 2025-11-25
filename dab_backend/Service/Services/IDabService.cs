using System;
using DAL.DTOs;

namespace Service.Services;

public interface IDabService
{
    Task<DabDto> GetDabByIdAsync(int id);
    Task<IEnumerable<DabDto>> GetAllDabsAsync();
    Task<DabDto> CreateDabAsync(DabCreateDto dabCreateDto);
    Task<DabDto> UpdateDabAsync(DabUpdateDto dabUpdateDto);
    Task DeleteDabAsync(int id);
}
