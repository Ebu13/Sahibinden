using Backend.Business.Services;
using Backend.Business.Requests;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Backend.Business.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Logging;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly HomeService _homeService;
        private readonly ILoggerService _logger;

        public HomeController(HomeService homeService, ILoggerService logger)
        {
            _homeService = homeService;
            _logger = logger;
        }

        [HttpGet("menu/{menuId}")]
        [Authorize]
        public async Task<ActionResult<List<HomeRequestDto>>> GetHomesByMenuId(int menuId)
        {
            _logger.LogInfo($"GetHomesByMenuId endpoint hit with menuId: {menuId}");
            try
            {
                var homes = await _homeService.GetHomesByMenuIdAsync(menuId);
                if (homes == null || !homes.Any())
                {
                    _logger.LogWarn($"No homes found for menuId: {menuId}");
                    return NotFound();
                }

                var homeDtos = homes.Select(home => home.ToDto()).ToList();
                return Ok(homeDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetHomesByMenuId: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<HomeRequestDto>>> GetHomes()
        {
            _logger.LogInfo("GetHomes endpoint hit.");
            try
            {
                var homes = await _homeService.GetAllAsync();
                return Ok(homes);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetHomes: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<HomeRequestDto>> GetHome(int id)
        {
            _logger.LogInfo($"GetHome endpoint hit with id: {id}");
            try
            {
                var home = await _homeService.GetByIdAsync(id);
                if (home == null)
                {
                    _logger.LogWarn($"Home with id: {id} not found.");
                    return NotFound();
                }
                return Ok(home);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetHome: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Supplier")]
        public async Task<ActionResult<HomeRequestDto>> PostHome([FromBody] HomeRequestDto homeRequest)
        {
            _logger.LogInfo("PostHome endpoint hit.");
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("PostHome failed due to invalid model state.");
                return BadRequest(ModelState);
            }

            try
            {
                var home = await _homeService.AddAsync(homeRequest);
                return CreatedAtAction(nameof(GetHome), new { id = home.HomeId }, home);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in PostHome: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Supplier")]
        public async Task<IActionResult> PutHome(int id, [FromBody] HomeRequestDto homeRequest)
        {
            _logger.LogInfo($"PutHome endpoint hit with id: {id}");
            if (id != homeRequest.HomeId)
            {
                _logger.LogWarn($"PutHome failed due to ID mismatch: {id} != {homeRequest.HomeId}");
                return BadRequest("Home ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarn("PutHome failed due to invalid model state.");
                return BadRequest(ModelState);
            }

            try
            {
                var updatedHome = await _homeService.UpdateAsync(id, homeRequest);
                if (updatedHome == null)
                {
                    _logger.LogWarn($"Home with id: {id} not found.");
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in PutHome: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Supplier")]
        public async Task<IActionResult> DeleteHome(int id)
        {
            _logger.LogInfo($"DeleteHome endpoint hit with id: {id}");
            try
            {
                var deleted = await _homeService.DeleteAsync(id);
                if (!deleted)
                {
                    _logger.LogWarn($"Home with id: {id} not found.");
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteHome: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
