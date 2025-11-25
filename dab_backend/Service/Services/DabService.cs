using System;
using DAL.DTOs;
using DAL.Entities;
using DAL.Repositories;

namespace Service.Services;

public class DabService : IDabService
{
    private readonly IUnitOfWork _unitOfWork;

    public DabService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DabDto> GetDabByIdAsync(int id)
    {
        var dab = await _unitOfWork.Dabs.GetByIdAsync(id);
        if (dab == null)
            throw new KeyNotFoundException($"Dab with id {id} not found");

        return MapToDto(dab);
    }

    public async Task<IEnumerable<DabDto>> GetAllDabsAsync()
    {
        var dabs = await _unitOfWork.Dabs.GetAllAsync();
        return dabs.Select(MapToDto).ToList();
    }

    public async Task<DabDto> CreateDabAsync(DabCreateDto dabCreateDto)
    {
        var dab = new Dab
        {
            Statut = dabCreateDto.Statut,
            Montantdisponible = dabCreateDto.Montantdisponible,
            Localisation = dabCreateDto.Localisation,
            Idagence = dabCreateDto.Idagence
        };

        var createdDab = await _unitOfWork.Dabs.AddAsync(dab);
        return MapToDto(createdDab);
    }

    public async Task<DabDto> UpdateDabAsync(DabUpdateDto dabUpdateDto)
    {
        var dab = await _unitOfWork.Dabs.GetByIdAsync(dabUpdateDto.Id);
        if (dab == null)
            throw new KeyNotFoundException($"Dab with id {dabUpdateDto.Id} not found");

        dab.Statut = dabUpdateDto.Statut;
        dab.Montantdisponible = dabUpdateDto.Montantdisponible;
        dab.Localisation = dabUpdateDto.Localisation;
        dab.Idagence = dabUpdateDto.Idagence;

        var updatedDab = await _unitOfWork.Dabs.UpdateAsync(dab);
        return MapToDto(updatedDab);
    }

    public async Task DeleteDabAsync(int id)
    {
        await _unitOfWork.Dabs.DeleteAsync(id);
    }

    private static DabDto MapToDto(Dab dab)
    {
        return new DabDto
        {
            Id = dab.Id,
            Statut = dab.Statut,
            Montantdisponible = dab.Montantdisponible,
            Localisation = dab.Localisation,
            Idagence = dab.Idagence
        };
    }
}
