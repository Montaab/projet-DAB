using System;
using DAL.DTOs;

namespace Service.Services;

public interface IUtilisateurService
{
    Task<UtilisateurDto> GetUtilisateurByIdAsync(int id);
    Task<IEnumerable<UtilisateurDto>> GetAllUtilisateursAsync();
    Task<UtilisateurDto> CreateUtilisateurAsync(UtilisateurCreateDto utilisateurCreateDto);
    Task<UtilisateurDto> UpdateUtilisateurAsync(UtilisateurUpdateDto utilisateurUpdateDto);
    Task DeleteUtilisateurAsync(int id);
}
