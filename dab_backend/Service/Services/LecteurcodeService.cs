using System;
using DAL.DTOs;
using DAL.Entities;
using DAL.Repositories;

namespace Service.Services;

public class LecteurcodeService : ILecteurcodeService
{
    private readonly IUnitOfWork _unitOfWork;

    public LecteurcodeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<LectearcodeDto> GetLecteurcodeByIdAsync(int id)
    {
        var lecteurcode = await _unitOfWork.Lecteurcodes.GetByIdAsync(id);
        if (lecteurcode == null)
            throw new KeyNotFoundException($"Lecteurcode with id {id} not found");

        return MapToDto(lecteurcode);
    }

    public async Task<IEnumerable<LectearcodeDto>> GetAllLecteurscodesAsync()
    {
        var lecteurcodes = await _unitOfWork.Lecteurcodes.GetAllAsync();
        return lecteurcodes.Select(MapToDto).ToList();
    }

    public async Task<LectearcodeDto> CreateLecteurcodeAsync(LecteurcodeCreateDto lecteurcodeCreateDto)
    {
        var lecteurcode = new Lecteurcode
        {
            Id = lecteurcodeCreateDto.Id,
            Typelecteur = lecteurcodeCreateDto.Typelecteur
        };

        var createdLecteurcode = await _unitOfWork.Lecteurcodes.AddAsync(lecteurcode);
        return MapToDto(createdLecteurcode);
    }

    public async Task<LectearcodeDto> UpdateLecteurcodeAsync(LecteurcodeUpdateDto lecteurcodeUpdateDto)
    {
        var lecteurcode = await _unitOfWork.Lecteurcodes.GetByIdAsync(lecteurcodeUpdateDto.Id);
        if (lecteurcode == null)
            throw new KeyNotFoundException($"Lecteurcode with id {lecteurcodeUpdateDto.Id} not found");

        lecteurcode.Typelecteur = lecteurcodeUpdateDto.Typelecteur;

        var updatedLecteurcode = await _unitOfWork.Lecteurcodes.UpdateAsync(lecteurcode);
        return MapToDto(updatedLecteurcode);
    }

    public async Task DeleteLecteurcodeAsync(int id)
    {
        await _unitOfWork.Lecteurcodes.DeleteAsync(id);
    }

    private static LectearcodeDto MapToDto(Lecteurcode lecteurcode)
    {
        return new LectearcodeDto
        {
            Id = lecteurcode.Id,
            Typelecteur = lecteurcode.Typelecteur
        };
    }
}
