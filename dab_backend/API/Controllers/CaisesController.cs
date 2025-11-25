using Microsoft.AspNetCore.Mvc;
using DAL.DTOs;
using Service.Services;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CaisesController : ControllerBase
{
    private readonly ICaisseService _caisseService;
    private readonly ILogger<CaisesController> _logger;

    public CaisesController(ICaisseService caisseService, ILogger<CaisesController> logger)
    {
        _caisseService = caisseService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CaisseDto>>> GetAll()
    {
        try
        {
            var caises = await _caisseService.GetAllCaisesAsync();
            return Ok(caises);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all caises");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CaisseDto>> GetById(int id)
    {
        try
        {
            var caisse = await _caisseService.GetCaisseByIdAsync(id);
            return Ok(caisse);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting caisse");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<ActionResult<CaisseDto>> Create(CaisseCreateDto caisseCreateDto)
    {
        try
        {
            var caisse = await _caisseService.CreateCaisseAsync(caisseCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = caisse.Id }, caisse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating caisse");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CaisseDto>> Update(int id, CaisseUpdateDto caisseUpdateDto)
    {
        try
        {
            if (id != caisseUpdateDto.Id)
                return BadRequest("ID mismatch");

            var caisse = await _caisseService.UpdateCaisseAsync(caisseUpdateDto);
            return Ok(caisse);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating caisse");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _caisseService.DeleteCaisseAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting caisse");
            return StatusCode(500, "Internal server error");
        }
    }
}
