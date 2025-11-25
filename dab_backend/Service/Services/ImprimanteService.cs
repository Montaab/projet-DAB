using System;
using DAL.DTOs;
using DAL.Entities;
using DAL.Repositories;

namespace Service.Services;

public class ImprimanteService : IImprimanteService
{
    private readonly IUnitOfWork _unitOfWork;

    public ImprimanteService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ImprimanteDto> GetImprimanteByIdAsync(int id)
    {
        var imprimante = await _unitOfWork.Imprimantes.GetByIdAsync(id);
        if (imprimante == null)
            throw new KeyNotFoundException($"Imprimante with id {id} not found");

        return MapToDto(imprimante);
    }

    public async Task<IEnumerable<ImprimanteDto>> GetAllImprimantesAsync()
    {
        var imprimantes = await _unitOfWork.Imprimantes.GetAllAsync();
        return imprimantes.Select(MapToDto).ToList();
    }

    public async Task<ImprimanteDto> CreateImprimanteAsync(ImprimanteCreateDto imprimanteCreateDto)
    {
        var imprimante = new Imprimante
        {
            Id = imprimanteCreateDto.Id,
            Niveaupapier = imprimanteCreateDto.Niveaupapier
        };

        var createdImprimante = await _unitOfWork.Imprimantes.AddAsync(imprimante);
        return MapToDto(createdImprimante);
    }

    public async Task<ImprimanteDto> UpdateImprimanteAsync(ImprimanteUpdateDto imprimanteUpdateDto)
    {
        var imprimante = await _unitOfWork.Imprimantes.GetByIdAsync(imprimanteUpdateDto.Id);
        if (imprimante == null)
            throw new KeyNotFoundException($"Imprimante with id {imprimanteUpdateDto.Id} not found");

        imprimante.Niveaupapier = imprimanteUpdateDto.Niveaupapier;

        var updatedImprimante = await _unitOfWork.Imprimantes.UpdateAsync(imprimante);
        return MapToDto(updatedImprimante);
    }

    public async Task DeleteImprimanteAsync(int id)
    {
        await _unitOfWork.Imprimantes.DeleteAsync(id);
    }

    private static ImprimanteDto MapToDto(Imprimante imprimante)
    {
        return new ImprimanteDto
        {
            Id = imprimante.Id,
            Niveaupapier = imprimante.Niveaupapier
        };
    }
}
