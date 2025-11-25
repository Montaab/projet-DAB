using System;
using DAL.DTOs;

namespace Service.Services;

public interface IAgenceService
{
    Task<AgenceDto> GetAgenceByIdAsync(int id);
    Task<IEnumerable<AgenceDto>> GetAllAgencesAsync();
    Task<AgenceDto> CreateAgenceAsync(AgenceCreateDto agenceCreateDto);
    Task<AgenceDto> UpdateAgenceAsync(AgenceUpdateDto agenceUpdateDto);
    Task DeleteAgenceAsync(int id);
}
