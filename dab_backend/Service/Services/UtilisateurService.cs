using System;
using DAL.DTOs;
using DAL.Entities;
using DAL.Repositories;

namespace Service.Services;

public class UtilisateurService : IUtilisateurService
{
    private readonly IUnitOfWork _unitOfWork;

    public UtilisateurService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UtilisateurDto> GetUtilisateurByIdAsync(int id)
    {
        var utilisateur = await _unitOfWork.Utilisateurs.GetByIdAsync(id);
        if (utilisateur == null)
            throw new KeyNotFoundException($"Utilisateur with id {id} not found");

        return MapToDto(utilisateur);
    }

    public async Task<IEnumerable<UtilisateurDto>> GetAllUtilisateursAsync()
    {
        var utilisateurs = await _unitOfWork.Utilisateurs.GetAllAsync();
        return utilisateurs.Select(MapToDto).ToList();
    }

    public async Task<UtilisateurDto> CreateUtilisateurAsync(UtilisateurCreateDto utilisateurCreateDto)
    {
        var utilisateur = new Utilisateur
        {
            Nom = utilisateurCreateDto.Nom,
            Email = utilisateurCreateDto.Email,
            MotDePasse = utilisateurCreateDto.MotDePasse,
            RoleId = utilisateurCreateDto.RoleId
        };

        var createdUtilisateur = await _unitOfWork.Utilisateurs.AddAsync(utilisateur);
        return MapToDto(createdUtilisateur);
    }

    public async Task<UtilisateurDto> UpdateUtilisateurAsync(UtilisateurUpdateDto utilisateurUpdateDto)
    {
        var utilisateur = await _unitOfWork.Utilisateurs.GetByIdAsync(utilisateurUpdateDto.Id);
        if (utilisateur == null)
            throw new KeyNotFoundException($"Utilisateur with id {utilisateurUpdateDto.Id} not found");

        utilisateur.Nom = utilisateurUpdateDto.Nom;
        utilisateur.Email = utilisateurUpdateDto.Email;
        utilisateur.RoleId = utilisateurUpdateDto.RoleId;

        var updatedUtilisateur = await _unitOfWork.Utilisateurs.UpdateAsync(utilisateur);
        return MapToDto(updatedUtilisateur);
    }

    public async Task DeleteUtilisateurAsync(int id)
    {
        await _unitOfWork.Utilisateurs.DeleteAsync(id);
    }

    private static UtilisateurDto MapToDto(Utilisateur utilisateur)
    {
        return new UtilisateurDto
        {
            Id = utilisateur.Id,
            Nom = utilisateur.Nom,
            Email = utilisateur.Email,
            RoleId = utilisateur.RoleId
        };
    }
}
