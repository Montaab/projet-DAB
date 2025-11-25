using System;
using DAL.DTOs;

namespace Service.Services;

public interface ICaisseService
{
    Task<CaisseDto> GetCaisseByIdAsync(int id);
    Task<IEnumerable<CaisseDto>> GetAllCaisesAsync();
    Task<CaisseDto> CreateCaisseAsync(CaisseCreateDto caisseCreateDto);
    Task<CaisseDto> UpdateCaisseAsync(CaisseUpdateDto caisseUpdateDto);
    Task DeleteCaisseAsync(int id);
}
