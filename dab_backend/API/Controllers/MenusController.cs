using Microsoft.AspNetCore.Mvc;
using DAL.DTOs;
using Service.Services;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenusController : ControllerBase
{
    private readonly IMenuService _menuService;
    private readonly ILogger<MenusController> _logger;

    public MenusController(IMenuService menuService, ILogger<MenusController> logger)
    {
        _menuService = menuService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MenuDto>>> GetAll()
    {
        try
        {
            var menus = await _menuService.GetAllMenusAsync();
            return Ok(menus);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all menus");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MenuDto>> GetById(int id)
    {
        try
        {
            var menu = await _menuService.GetMenuByIdAsync(id);
            return Ok(menu);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting menu");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<ActionResult<MenuDto>> Create(MenuCreateDto menuCreateDto)
    {
        try
        {
            var menu = await _menuService.CreateMenuAsync(menuCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = menu.Id }, menu);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating menu");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MenuDto>> Update(int id, MenuUpdateDto menuUpdateDto)
    {
        try
        {
            if (id != menuUpdateDto.Id)
                return BadRequest("ID mismatch");

            var menu = await _menuService.UpdateMenuAsync(menuUpdateDto);
            return Ok(menu);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating menu");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _menuService.DeleteMenuAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting menu");
            return StatusCode(500, "Internal server error");
        }
    }
}
