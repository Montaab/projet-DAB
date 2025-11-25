using System;
using DAL.DTOs;

namespace Service.Services;

public interface ILecteurcodeService
{
    Task<LectearcodeDto> GetLecteurcodeByIdAsync(int id);
    Task<IEnumerable<LectearcodeDto>> GetAllLecteurscodesAsync();
    Task<LectearcodeDto> CreateLecteurcodeAsync(LecteurcodeCreateDto lecteurcodeCreateDto);
    Task<LectearcodeDto> UpdateLecteurcodeAsync(LecteurcodeUpdateDto lecteurcodeUpdateDto);
    Task DeleteLecteurcodeAsync(int id);
}
