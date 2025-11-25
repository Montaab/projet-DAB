using System;
using DAL.DTOs;
using DAL.Entities;
using DAL.Repositories;

namespace Service.Services;

public class CaisseService : ICaisseService
{
    private readonly IUnitOfWork _unitOfWork;

    public CaisseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CaisseDto> GetCaisseByIdAsync(int id)
    {
        var caisse = await _unitOfWork.Caisses.GetByIdAsync(id);
        if (caisse == null)
            throw new KeyNotFoundException($"Caisse with id {id} not found");

        return MapToDto(caisse);
    }

    public async Task<IEnumerable<CaisseDto>> GetAllCaisesAsync()
    {
        var caisses = await _unitOfWork.Caisses.GetAllAsync();
        return caisses.Select(MapToDto).ToList();
    }

    public async Task<CaisseDto> CreateCaisseAsync(CaisseCreateDto caisseCreateDto)
    {
        var caisse = new Caisse
        {
            Id = caisseCreateDto.Id,
            Nombrebillets = caisseCreateDto.Nombrebillets,
            Valeurbillet = caisseCreateDto.Valeurbillet
        };

        var createdCaisse = await _unitOfWork.Caisses.AddAsync(caisse);
        return MapToDto(createdCaisse);
    }

    public async Task<CaisseDto> UpdateCaisseAsync(CaisseUpdateDto caisseUpdateDto)
    {
        var caisse = await _unitOfWork.Caisses.GetByIdAsync(caisseUpdateDto.Id);
        if (caisse == null)
            throw new KeyNotFoundException($"Caisse with id {caisseUpdateDto.Id} not found");

        caisse.Nombrebillets = caisseUpdateDto.Nombrebillets;
        caisse.Valeurbillet = caisseUpdateDto.Valeurbillet;

        var updatedCaisse = await _unitOfWork.Caisses.UpdateAsync(caisse);
        return MapToDto(updatedCaisse);
    }

    public async Task DeleteCaisseAsync(int id)
    {
        await _unitOfWork.Caisses.DeleteAsync(id);
    }

    private static CaisseDto MapToDto(Caisse caisse)
    {
        return new CaisseDto
        {
            Id = caisse.Id,
            Nombrebillets = caisse.Nombrebillets,
            Valeurbillet = caisse.Valeurbillet
        };
    }
}
