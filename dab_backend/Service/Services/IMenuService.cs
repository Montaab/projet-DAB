using System;
using DAL.DTOs;

namespace Service.Services;

public interface IMenuService
{
    Task<MenuDto> GetMenuByIdAsync(int id);
    Task<IEnumerable<MenuDto>> GetAllMenusAsync();
    Task<MenuDto> CreateMenuAsync(MenuCreateDto menuCreateDto);
    Task<MenuDto> UpdateMenuAsync(MenuUpdateDto menuUpdateDto);
    Task DeleteMenuAsync(int id);
}
