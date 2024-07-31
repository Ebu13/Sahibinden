using Backend.Business.Services;
using Backend.Business.Requests;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly MenuService _menuService;

        public MenusController(MenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenus()
        {
            var menus = await _menuService.GetAllAsync();
            return Ok(menus);
        }

        [HttpGet("{id}")]
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
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenusByParentId(int parentId)
        {
            var menus = await _menuService.GetByParentIdAsync(parentId);
            return Ok(menus);
        }

        [HttpPost]
        public async Task<ActionResult<Menu>> PostMenu(MenuRequestDto menuRequest)
        {
            var menu = await _menuService.AddAsync(menuRequest);
            return CreatedAtAction(nameof(GetMenu), new { id = menu.MenuId }, menu);
        }

        [HttpPut("{id}")]
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
