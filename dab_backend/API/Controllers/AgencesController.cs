using Microsoft.AspNetCore.Mvc;
using DAL.DTOs;
using Service.Services;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgencesController : ControllerBase
{
    private readonly IAgenceService _agenceService;
    private readonly ILogger<AgencesController> _logger;

    public AgencesController(IAgenceService agenceService, ILogger<AgencesController> logger)
    {
        _agenceService = agenceService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AgenceDto>>> GetAll()
    {
        try
        {
            var agences = await _agenceService.GetAllAgencesAsync();
            return Ok(agences);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all agences");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AgenceDto>> GetById(int id)
    {
        try
        {
            var agence = await _agenceService.GetAgenceByIdAsync(id);
            return Ok(agence);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting agence");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<ActionResult<AgenceDto>> Create(AgenceCreateDto agenceCreateDto)
    {
        try
        {
            var agence = await _agenceService.CreateAgenceAsync(agenceCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = agence.Id }, agence);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating agence");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AgenceDto>> Update(int id, AgenceUpdateDto agenceUpdateDto)
    {
        try
        {
            if (id != agenceUpdateDto.Id)
                return BadRequest("ID mismatch");

            var agence = await _agenceService.UpdateAgenceAsync(agenceUpdateDto);
            return Ok(agence);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating agence");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _agenceService.DeleteAgenceAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting agence");
            return StatusCode(500, "Internal server error");
        }
    }
}
