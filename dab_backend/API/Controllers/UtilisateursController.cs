using Microsoft.AspNetCore.Mvc;
using DAL.DTOs;
using Service.Services;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UtilisateursController : ControllerBase
{
    private readonly IUtilisateurService _utilisateurService;
    private readonly ILogger<UtilisateursController> _logger;

    public UtilisateursController(IUtilisateurService utilisateurService, ILogger<UtilisateursController> logger)
    {
        _utilisateurService = utilisateurService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UtilisateurDto>>> GetAll()
    {
        try
        {
            var utilisateurs = await _utilisateurService.GetAllUtilisateursAsync();
            return Ok(utilisateurs);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all utilisateurs");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UtilisateurDto>> GetById(int id)
    {
        try
        {
            var utilisateur = await _utilisateurService.GetUtilisateurByIdAsync(id);
            return Ok(utilisateur);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting utilisateur");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<ActionResult<UtilisateurDto>> Create(UtilisateurCreateDto utilisateurCreateDto)
    {
        try
        {
            var utilisateur = await _utilisateurService.CreateUtilisateurAsync(utilisateurCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = utilisateur.Id }, utilisateur);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating utilisateur");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UtilisateurDto>> Update(int id, UtilisateurUpdateDto utilisateurUpdateDto)
    {
        try
        {
            if (id != utilisateurUpdateDto.Id)
                return BadRequest("ID mismatch");

            var utilisateur = await _utilisateurService.UpdateUtilisateurAsync(utilisateurUpdateDto);
            return Ok(utilisateur);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating utilisateur");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _utilisateurService.DeleteUtilisateurAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting utilisateur");
            return StatusCode(500, "Internal server error");
        }
    }
}
