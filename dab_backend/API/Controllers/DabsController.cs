using Microsoft.AspNetCore.Mvc;
using DAL.DTOs;
using Service.Services;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DabsController : ControllerBase
{
    private readonly IDabService _dabService;
    private readonly ILogger<DabsController> _logger;

    public DabsController(IDabService dabService, ILogger<DabsController> logger)
    {
        _dabService = dabService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DabDto>>> GetAll()
    {
        try
        {
            var dabs = await _dabService.GetAllDabsAsync();
            return Ok(dabs);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all dabs");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DabDto>> GetById(int id)
    {
        try
        {
            var dab = await _dabService.GetDabByIdAsync(id);
            return Ok(dab);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting dab");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<ActionResult<DabDto>> Create(DabCreateDto dabCreateDto)
    {
        try
        {
            var dab = await _dabService.CreateDabAsync(dabCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = dab.Id }, dab);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating dab");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<DabDto>> Update(int id, DabUpdateDto dabUpdateDto)
    {
        try
        {
            if (id != dabUpdateDto.Id)
                return BadRequest("ID mismatch");

            var dab = await _dabService.UpdateDabAsync(dabUpdateDto);
            return Ok(dab);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating dab");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _dabService.DeleteDabAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting dab");
            return StatusCode(500, "Internal server error");
        }
    }
}
