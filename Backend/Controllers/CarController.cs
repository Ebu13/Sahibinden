using Backend.Business.Services;
using Backend.Business.Requests;
using Microsoft.AspNetCore.Mvc;
using Backend.Business.Mapping;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarService _carService;

        public CarsController(CarService carService)
        {
            _carService = carService;
        }

        [HttpGet("menu/{menuId}")]
        public async Task<ActionResult<List<CarRequestDto>>> GetCarsByMenuId(int menuId)
        {
            var cars = await _carService.GetCarsByMenuIdAsync(menuId);
            var carDtos = cars.Select(car => car.ToDto()).ToList();
            return Ok(carDtos);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarRequestDto>>> GetCars()
        {
            var cars = await _carService.GetAllAsync();
            return Ok(cars);
        }

        [HttpGet("{id}")]
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
        public async Task<ActionResult<CarRequestDto>> PostCar(CarRequestDto carRequest)
        {
            var car = await _carService.AddAsync(carRequest);
            return CreatedAtAction(nameof(GetCar), new { id = car.CarId }, car);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, CarRequestDto carRequest)
        {
            var updatedCar = await _carService.UpdateAsync(id, carRequest);

            if (updatedCar == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
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
