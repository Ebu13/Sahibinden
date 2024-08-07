using Backend.Business.Services;
using Backend.Business.Requests;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Logging;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly MenuService _menuService;
        private readonly ILoggerService _logger;

        public MenuController(MenuService menuService, ILoggerService logger)
        {
            _menuService = menuService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenus()
        {
            _logger.LogInfo("GetMenus endpoint hit.");
            try
            {
                var menus = await _menuService.GetAllAsync();
                return Ok(menus);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetMenus: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Menu>> GetMenu(int id)
        {
            _logger.LogInfo($"GetMenu endpoint hit with id: {id}");
            try
            {
                var menu = await _menuService.GetByIdAsync(id);
                if (menu == null)
                {
                    _logger.LogWarn($"Menu with id: {id} not found.");
                    return NotFound();
                }
                return Ok(menu);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetMenu: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("parent/{parentId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenusByParentId(int parentId)
        {
            _logger.LogInfo($"GetMenusByParentId endpoint hit with parentId: {parentId}");
            try
            {
                var menus = await _menuService.GetByParentIdAsync(parentId);
                return Ok(menus);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetMenusByParentId: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Supplier")]
        public async Task<ActionResult<Menu>> PostMenu(MenuRequestDto menuRequest)
        {
            _logger.LogInfo("PostMenu endpoint hit.");
            try
            {
                var menu = await _menuService.AddAsync(menuRequest);
                return CreatedAtAction(nameof(GetMenu), new { id = menu.MenuId }, menu);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in PostMenu: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Supplier")]
        public async Task<IActionResult> PutMenu(int id, MenuRequestDto menuRequest)
        {
            _logger.LogInfo($"PutMenu endpoint hit with id: {id}");
            try
            {
                var updatedMenu = await _menuService.UpdateAsync(id, menuRequest);
                if (updatedMenu == null)
                {
                    _logger.LogWarn($"Menu with id: {id} not found.");
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in PutMenu: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Supplier")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            _logger.LogInfo($"DeleteMenu endpoint hit with id: {id}");
            try
            {
                var deleted = await _menuService.DeleteAsync(id);
                if (!deleted)
                {
                    _logger.LogWarn($"Menu with id: {id} not found.");
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteMenu: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
