using System;
using DAL.DTOs;
using DAL.Entities;
using DAL.Repositories;

namespace Service.Services;

public class AgenceService : IAgenceService
{
    private readonly IUnitOfWork _unitOfWork;

    public AgenceService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AgenceDto> GetAgenceByIdAsync(int id)
    {
        var agence = await _unitOfWork.Agences.GetByIdAsync(id);
        if (agence == null)
            throw new KeyNotFoundException($"Agence with id {id} not found");

        return MapToDto(agence);
    }

    public async Task<IEnumerable<AgenceDto>> GetAllAgencesAsync()
    {
        var agences = await _unitOfWork.Agences.GetAllAsync();
        return agences.Select(MapToDto).ToList();
    }

    public async Task<AgenceDto> CreateAgenceAsync(AgenceCreateDto agenceCreateDto)
    {
        var agence = new Agence
        {
            Nom = agenceCreateDto.Nom,
            Adresse = agenceCreateDto.Adresse
        };

        var createdAgence = await _unitOfWork.Agences.AddAsync(agence);
        return MapToDto(createdAgence);
    }

    public async Task<AgenceDto> UpdateAgenceAsync(AgenceUpdateDto agenceUpdateDto)
    {
        var agence = await _unitOfWork.Agences.GetByIdAsync(agenceUpdateDto.Id);
        if (agence == null)
            throw new KeyNotFoundException($"Agence with id {agenceUpdateDto.Id} not found");

        agence.Nom = agenceUpdateDto.Nom;
        agence.Adresse = agenceUpdateDto.Adresse;

        var updatedAgence = await _unitOfWork.Agences.UpdateAsync(agence);
        return MapToDto(updatedAgence);
    }

    public async Task DeleteAgenceAsync(int id)
    {
        await _unitOfWork.Agences.DeleteAsync(id);
    }

    private static AgenceDto MapToDto(Agence agence)
    {
        return new AgenceDto
        {
            Id = agence.Id,
            Nom = agence.Nom,
            Adresse = agence.Adresse
        };
    }
}
