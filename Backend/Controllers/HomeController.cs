using Backend.Business.Services;
using Backend.Business.Requests;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Backend.Business.Mapping;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomesController : ControllerBase
    {
        private readonly HomeService _homeService;

        public HomesController(HomeService homeService)
        {
            _homeService = homeService;
        }

        [HttpGet("menu/{menuId}")]
        public async Task<ActionResult<List<HomeRequestDto>>> GetHomesByMenuId(int menuId)
        {
            var homes = await _homeService.GetHomesByMenuIdAsync(menuId);
            var homeDtos = homes.Select(home => home.ToDto()).ToList();
            return Ok(homeDtos);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Home>>> GetHomes()
        {
            var homes = await _homeService.GetAllAsync();
            return Ok(homes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Home>> GetHome(int id)
        {
            var home = await _homeService.GetByIdAsync(id);

            if (home == null)
            {
                return NotFound();
            }

            return Ok(home);
        }

        [HttpPost]
        public async Task<ActionResult<Home>> PostHome(HomeRequestDto homeRequest)
        {
            var home = await _homeService.AddAsync(homeRequest);
            return CreatedAtAction(nameof(GetHome), new { id = home.HomeId }, home);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHome(int id, HomeRequestDto homeRequest)
        {
            var updatedHome = await _homeService.UpdateAsync(id, homeRequest);

            if (updatedHome == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHome(int id)
        {
            var deleted = await _homeService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
