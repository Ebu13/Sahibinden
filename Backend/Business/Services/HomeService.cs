using Backend.Data;
using Backend.Models;
using Backend.Business.Requests;
using Microsoft.EntityFrameworkCore;
using Backend.Business.Mapping;

namespace Backend.Business.Services
{
    public class HomeService
    {
        private readonly SahibindenContext _context;

        public HomeService(SahibindenContext context)
        {
            _context = context;
        }

        public async Task<List<Home>> GetHomesByMenuIdAsync(int menuId)
        {
            return await _context.Homes
                .Where(home => home.MenuId == menuId)
                .ToListAsync();
        }

        public async Task<List<HomeRequestDto>> GetAllAsync()
        {
            var homes = await _context.Homes.ToListAsync();
            return homes.Select(home => home.ToDto()).ToList();
        }

        public async Task<HomeRequestDto?> GetByIdAsync(int id)
        {
            var home = await _context.Homes.FindAsync(id);
            return home?.ToDto();
        }

        public async Task<HomeRequestDto> AddAsync(HomeRequestDto homeRequest)
        {
            var home = homeRequest.ToEntity();

            _context.Homes.Add(home);
            await _context.SaveChangesAsync();

            return home.ToDto();
        }

        public async Task<HomeRequestDto?> UpdateAsync(int id, HomeRequestDto homeRequest)
        {
            var home = await _context.Homes.FindAsync(id);

            if (home == null)
            {
                return null;
            }

            home.UserId = homeRequest.UserId;
            home.MenuId = homeRequest.MenuId;
            home.Location = homeRequest.Location;
            home.Size = homeRequest.Size;
            home.Price = homeRequest.Price;
            home.PhotoPath = homeRequest.PhotoPath; // Include PhotoPath update

            await _context.SaveChangesAsync();

            return home.ToDto();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var home = await _context.Homes.FindAsync(id);

            if (home == null)
            {
                return false;
            }

            _context.Homes.Remove(home);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
