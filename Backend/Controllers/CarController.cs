using Backend.Business.Services;
using Backend.Business.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Business.Mapping;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarService _carService;

        public CarController(CarService carService)
        {
            _carService = carService;
        }

        [HttpGet("menu/{menuId}")]
        [Authorize]
        public async Task<ActionResult<List<CarRequestDto>>> GetCarsByMenuId(int menuId)
        {
            var cars = await _carService.GetCarsByMenuIdAsync(menuId);
            var carDtos = cars.Select(car => car.ToDto()).ToList();
            return Ok(carDtos);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CarRequestDto>>> GetCars()
        {
            var cars = await _carService.GetAllAsync();
            return Ok(cars);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<CarRequestDto>> GetCar(int id)
        {
            var car = await _carService.GetByIdAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CarRequestDto>> PostCar([FromBody] CarRequestDto carRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var car = await _carService.AddAsync(carRequest);
            return CreatedAtAction(nameof(GetCar), new { id = car.CarId }, car);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutCar(int id, [FromBody] CarRequestDto carRequest)
        {
            if (id != carRequest.CarId)
            {
                return BadRequest("Car ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedCar = await _carService.UpdateAsync(id, carRequest);

            if (updatedCar == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var deleted = await _carService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
