using Backend.Business.Services;
using Backend.Business.Requests;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly MenuService _menuService;

        public MenuController(MenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenus()
        {
            var menus = await _menuService.GetAllAsync();
            return Ok(menus);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Menu>> GetMenu(int id)
        {
            var menu = await _menuService.GetByIdAsync(id);

            if (menu == null)
            {
                return NotFound();
            }

            return Ok(menu);
        }

        [HttpGet("parent/{parentId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenusByParentId(int parentId)
        {
            var menus = await _menuService.GetByParentIdAsync(parentId);
            return Ok(menus);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Supplier")]
        public async Task<ActionResult<Menu>> PostMenu(MenuRequestDto menuRequest)
        {
            var menu = await _menuService.AddAsync(menuRequest);
            return CreatedAtAction(nameof(GetMenu), new { id = menu.MenuId }, menu);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Supplier")]
        public async Task<IActionResult> PutMenu(int id, MenuRequestDto menuRequest)
        {
            var updatedMenu = await _menuService.UpdateAsync(id, menuRequest);

            if (updatedMenu == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Supplier")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            var deleted = await _menuService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }

}