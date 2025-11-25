using System;
using DAL.DTOs;
using DAL.Entities;
using DAL.Repositories;

namespace Service.Services;

public class ComposantService : IComposantService
{
    private readonly IUnitOfWork _unitOfWork;

    public ComposantService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ComposantDto> GetComposantByIdAsync(int id)
    {
        var composant = await _unitOfWork.Composants.GetByIdAsync(id);
        if (composant == null)
            throw new KeyNotFoundException($"Composant with id {id} not found");

        return MapToDto(composant);
    }

    public async Task<IEnumerable<ComposantDto>> GetAllComposantsAsync()
    {
        var composants = await _unitOfWork.Composants.GetAllAsync();
        return composants.Select(MapToDto).ToList();
    }

    public async Task<ComposantDto> CreateComposantAsync(ComposantCreateDto composantCreateDto)
    {
        var composant = new Composant
        {
            NomType = composantCreateDto.NomType,
            Etat = composantCreateDto.Etat,
            Dateinstallation = composantCreateDto.Dateinstallation,
            Iddab = composantCreateDto.Iddab
        };

        var createdComposant = await _unitOfWork.Composants.AddAsync(composant);
        return MapToDto(createdComposant);
    }

    public async Task<ComposantDto> UpdateComposantAsync(ComposantUpdateDto composantUpdateDto)
    {
        var composant = await _unitOfWork.Composants.GetByIdAsync(composantUpdateDto.Id);
        if (composant == null)
            throw new KeyNotFoundException($"Composant with id {composantUpdateDto.Id} not found");

        composant.NomType = composantUpdateDto.NomType;
        composant.Etat = composantUpdateDto.Etat;
        composant.Dateinstallation = composantUpdateDto.Dateinstallation;
        composant.Iddab = composantUpdateDto.Iddab;

        var updatedComposant = await _unitOfWork.Composants.UpdateAsync(composant);
        return MapToDto(updatedComposant);
    }

    public async Task DeleteComposantAsync(int id)
    {
        await _unitOfWork.Composants.DeleteAsync(id);
    }

    private static ComposantDto MapToDto(Composant composant)
    {
        return new ComposantDto
        {
            Id = composant.Id,
            NomType = composant.NomType,
            Etat = composant.Etat,
            Dateinstallation = composant.Dateinstallation,
            Iddab = composant.Iddab
        };
    }
}
