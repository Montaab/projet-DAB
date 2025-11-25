using Microsoft.AspNetCore.Mvc;
using DAL.DTOs;
using Service.Services;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComposantsController : ControllerBase
{
    private readonly IComposantService _composantService;
    private readonly ILogger<ComposantsController> _logger;

    public ComposantsController(IComposantService composantService, ILogger<ComposantsController> logger)
    {
        _composantService = composantService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ComposantDto>>> GetAll()
    {
        try
        {
            var composants = await _composantService.GetAllComposantsAsync();
            return Ok(composants);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all composants");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ComposantDto>> GetById(int id)
    {
        try
        {
            var composant = await _composantService.GetComposantByIdAsync(id);
            return Ok(composant);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting composant");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<ActionResult<ComposantDto>> Create(ComposantCreateDto composantCreateDto)
    {
        try
        {
            var composant = await _composantService.CreateComposantAsync(composantCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = composant.Id }, composant);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating composant");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ComposantDto>> Update(int id, ComposantUpdateDto composantUpdateDto)
    {
        try
        {
            if (id != composantUpdateDto.Id)
                return BadRequest("ID mismatch");

            var composant = await _composantService.UpdateComposantAsync(composantUpdateDto);
            return Ok(composant);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating composant");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _composantService.DeleteComposantAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting composant");
            return StatusCode(500, "Internal server error");
        }
    }
}
