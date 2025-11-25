using System;
using DAL.DTOs;
using DAL.Entities;
using DAL.Repositories;

namespace Service.Services;

public class MenuService : IMenuService
{
    private readonly IUnitOfWork _unitOfWork;

    public MenuService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<MenuDto> GetMenuByIdAsync(int id)
    {
        var menu = await _unitOfWork.Menus.GetByIdAsync(id);
        if (menu == null)
            throw new KeyNotFoundException($"Menu with id {id} not found");

        return MapToDto(menu);
    }

    public async Task<IEnumerable<MenuDto>> GetAllMenusAsync()
    {
        var menus = await _unitOfWork.Menus.GetAllAsync();
        return menus.Select(MapToDto).ToList();
    }

    public async Task<MenuDto> CreateMenuAsync(MenuCreateDto menuCreateDto)
    {
        var menu = new Menu
        {
            Nom = menuCreateDto.Nom
        };

        var createdMenu = await _unitOfWork.Menus.AddAsync(menu);
        return MapToDto(createdMenu);
    }

    public async Task<MenuDto> UpdateMenuAsync(MenuUpdateDto menuUpdateDto)
    {
        var menu = await _unitOfWork.Menus.GetByIdAsync(menuUpdateDto.Id);
        if (menu == null)
            throw new KeyNotFoundException($"Menu with id {menuUpdateDto.Id} not found");

        menu.Nom = menuUpdateDto.Nom;

        var updatedMenu = await _unitOfWork.Menus.UpdateAsync(menu);
        return MapToDto(updatedMenu);
    }

    public async Task DeleteMenuAsync(int id)
    {
        await _unitOfWork.Menus.DeleteAsync(id);
    }

    private static MenuDto MapToDto(Menu menu)
    {
        return new MenuDto
        {
            Id = menu.Id,
            Nom = menu.Nom
        };
    }
}
