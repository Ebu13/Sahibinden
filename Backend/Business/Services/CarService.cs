using Backend.Data;
using Backend.Models;
using Backend.Business.Requests;
using Microsoft.EntityFrameworkCore;
using Backend.Business.Mapping;

namespace Backend.Business.Services
{
    public class CarService
    {
        private readonly SahibindenContext _context;

        public CarService(SahibindenContext context)
        {
            _context = context;
        }

        public async Task<List<Car>> GetCarsByMenuIdAsync(int menuId)
        {
            return await _context.Cars
                .Where(car => car.MenuId == menuId)
                .ToListAsync();
        }

        public async Task<List<CarRequestDto>> GetAllAsync()
        {
            var cars = await _context.Cars.ToListAsync();
            return cars.Select(car => car.ToDto()).ToList();
        }

        public async Task<CarRequestDto?> GetByIdAsync(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            return car?.ToDto();
        }

        public async Task<CarRequestDto> AddAsync(CarRequestDto carRequest)
        {
            var car = carRequest.ToEntity();

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return car.ToDto();
        }

        public async Task<CarRequestDto?> UpdateAsync(int id, CarRequestDto carRequest)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return null;
            }

            car.UserId = carRequest.UserId;
            car.MenuId = carRequest.MenuId;
            car.Year = carRequest.Year;
            car.Price = carRequest.Price;

            await _context.SaveChangesAsync();

            return car.ToDto();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return false;
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
