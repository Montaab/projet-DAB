using System;
using DAL.DTOs;

namespace Service.Services;

public interface IImprimanteService
{
    Task<ImprimanteDto> GetImprimanteByIdAsync(int id);
    Task<IEnumerable<ImprimanteDto>> GetAllImprimantesAsync();
    Task<ImprimanteDto> CreateImprimanteAsync(ImprimanteCreateDto imprimanteCreateDto);
    Task<ImprimanteDto> UpdateImprimanteAsync(ImprimanteUpdateDto imprimanteUpdateDto);
    Task DeleteImprimanteAsync(int id);
}
