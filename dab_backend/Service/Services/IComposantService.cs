using System;
using DAL.DTOs;

namespace Service.Services;

public interface IComposantService
{
    Task<ComposantDto> GetComposantByIdAsync(int id);
    Task<IEnumerable<ComposantDto>> GetAllComposantsAsync();
    Task<ComposantDto> CreateComposantAsync(ComposantCreateDto composantCreateDto);
    Task<ComposantDto> UpdateComposantAsync(ComposantUpdateDto composantUpdateDto);
    Task DeleteComposantAsync(int id);
}
