using Microsoft.AspNetCore.Mvc;
using DAL.DTOs;
using Service.Services;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LecteurcodesController : ControllerBase
{
    private readonly ILecteurcodeService _lecteurcodeService;
    private readonly ILogger<LecteurcodesController> _logger;

    public LecteurcodesController(ILecteurcodeService lecteurcodeService, ILogger<LecteurcodesController> logger)
    {
        _lecteurcodeService = lecteurcodeService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LectearcodeDto>>> GetAll()
    {
        try
        {
            var lecteurcodes = await _lecteurcodeService.GetAllLecteurscodesAsync();
            return Ok(lecteurcodes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all lecteurcodes");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LectearcodeDto>> GetById(int id)
    {
        try
        {
            var lecteurcode = await _lecteurcodeService.GetLecteurcodeByIdAsync(id);
            return Ok(lecteurcode);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting lecteurcode");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<ActionResult<LectearcodeDto>> Create(LecteurcodeCreateDto lecteurcodeCreateDto)
    {
        try
        {
            var lecteurcode = await _lecteurcodeService.CreateLecteurcodeAsync(lecteurcodeCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = lecteurcode.Id }, lecteurcode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating lecteurcode");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<LectearcodeDto>> Update(int id, LecteurcodeUpdateDto lecteurcodeUpdateDto)
    {
        try
        {
            if (id != lecteurcodeUpdateDto.Id)
                return BadRequest("ID mismatch");

            var lecteurcode = await _lecteurcodeService.UpdateLecteurcodeAsync(lecteurcodeUpdateDto);
            return Ok(lecteurcode);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating lecteurcode");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _lecteurcodeService.DeleteLecteurcodeAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting lecteurcode");
            return StatusCode(500, "Internal server error");
        }
    }
}
