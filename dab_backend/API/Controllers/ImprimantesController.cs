using Microsoft.AspNetCore.Mvc;
using DAL.DTOs;
using Service.Services;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImprimantesController : ControllerBase
{
    private readonly IImprimanteService _imprimanteService;
    private readonly ILogger<ImprimantesController> _logger;

    public ImprimantesController(IImprimanteService imprimanteService, ILogger<ImprimantesController> logger)
    {
        _imprimanteService = imprimanteService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ImprimanteDto>>> GetAll()
    {
        try
        {
            var imprimantes = await _imprimanteService.GetAllImprimantesAsync();
            return Ok(imprimantes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all imprimantes");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ImprimanteDto>> GetById(int id)
    {
        try
        {
            var imprimante = await _imprimanteService.GetImprimanteByIdAsync(id);
            return Ok(imprimante);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting imprimante");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<ActionResult<ImprimanteDto>> Create(ImprimanteCreateDto imprimanteCreateDto)
    {
        try
        {
            var imprimante = await _imprimanteService.CreateImprimanteAsync(imprimanteCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = imprimante.Id }, imprimante);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating imprimante");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ImprimanteDto>> Update(int id, ImprimanteUpdateDto imprimanteUpdateDto)
    {
        try
        {
            if (id != imprimanteUpdateDto.Id)
                return BadRequest("ID mismatch");

            var imprimante = await _imprimanteService.UpdateImprimanteAsync(imprimanteUpdateDto);
            return Ok(imprimante);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating imprimante");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _imprimanteService.DeleteImprimanteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting imprimante");
            return StatusCode(500, "Internal server error");
        }
    }
}
