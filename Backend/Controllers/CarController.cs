using Backend.Business.Services;
using Backend.Business.Requests;
using Microsoft.AspNetCore.Mvc;
using Backend.Business.Mapping;
using Microsoft.AspNetCore.Authorization;
using Backend.Logging;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarService _carService;
        private readonly ILoggerService _logger;

        public CarController(CarService carService, ILoggerService logger)
        {
            _carService = carService;
            _logger = logger;
        }

        [HttpGet("menu/{menuId}")]
        [Authorize]
        public async Task<ActionResult<List<CarRequestDto>>> GetCarsByMenuId(int menuId)
        {
            _logger.LogInfo($"GetCarsByMenuId endpoint hit with menuId: {menuId}");
            try
            {
                var cars = await _carService.GetCarsByMenuIdAsync(menuId);
                if (cars == null || !cars.Any())
                {
                    _logger.LogWarn($"No cars found for menuId: {menuId}");
                    return NotFound();
                }

                var carDtos = cars.Select(car => car.ToDto()).ToList();
                return Ok(carDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetCarsByMenuId: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CarRequestDto>>> GetCars()
        {
            _logger.LogInfo("GetCars endpoint hit.");
            try
            {
                var cars = await _carService.GetAllAsync();
                return Ok(cars);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetCars: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<CarRequestDto>> GetCar(int id)
        {
            _logger.LogInfo($"GetCar endpoint hit with id: {id}");
            try
            {
                var car = await _carService.GetByIdAsync(id);
                if (car == null)
                {
                    _logger.LogWarn($"Car with id: {id} not found.");
                    return NotFound();
                }
                return Ok(car);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetCar: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Supplier")]
        public async Task<ActionResult<CarRequestDto>> PostCar([FromBody] CarRequestDto carRequest)
        {
            _logger.LogInfo("PostCar endpoint hit.");
            if (!ModelState.IsValid)
            {
                _logger.LogWarn("PostCar failed due to invalid model state.");
                return BadRequest(ModelState);
            }

            try
            {
                var car = await _carService.AddAsync(carRequest);
                return CreatedAtAction(nameof(GetCar), new { id = car.CarId }, car);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in PostCar: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Supplier")]
        public async Task<IActionResult> PutCar(int id, [FromBody] CarRequestDto carRequest)
        {
            _logger.LogInfo($"PutCar endpoint hit with id: {id}");
            if (id != carRequest.CarId)
            {
                _logger.LogWarn($"PutCar failed due to ID mismatch: {id} != {carRequest.CarId}");
                return BadRequest("Car ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarn("PutCar failed due to invalid model state.");
                return BadRequest(ModelState);
            }

            try
            {
                var updatedCar = await _carService.UpdateAsync(id, carRequest);
                if (updatedCar == null)
                {
                    _logger.LogWarn($"Car with id: {id} not found.");
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in PutCar: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Supplier")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            _logger.LogInfo($"DeleteCar endpoint hit with id: {id}");
            try
            {
                var deleted = await _carService.DeleteAsync(id);
                if (!deleted)
                {
                    _logger.LogWarn($"Car with id: {id} not found.");
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteCar: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
