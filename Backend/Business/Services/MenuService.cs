using Backend.Data;
using Backend.Models;
using Backend.Business.Requests;
using Microsoft.EntityFrameworkCore;
using Backend.Business.Mapping;

namespace Backend.Business.Services
{
    public class MenuService
    {
        private readonly SahibindenContext _context;

        public MenuService(SahibindenContext context)
        {
            _context = context;
        }

        public async Task<List<MenuRequestDto>> GetAllAsync()
        {
            var menus = await _context.Menus.ToListAsync();
            return menus.Select(menu => menu.ToDto()).ToList();
        }

        public async Task<MenuRequestDto?> GetByIdAsync(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            return menu?.ToDto();
        }

        public async Task<List<MenuRequestDto>> GetByParentIdAsync(int parentId)
        {
            var menus = await _context.Menus.Where(m => m.ParentId == parentId).ToListAsync();
            return menus.Select(menu => menu.ToDto()).ToList();
        }

        public async Task<MenuRequestDto> AddAsync(MenuRequestDto menuRequest)
        {
            var menu = menuRequest.ToEntity();

            _context.Menus.Add(menu);
            await _context.SaveChangesAsync();

            return menu.ToDto();
        }

        public async Task<MenuRequestDto?> UpdateAsync(int id, MenuRequestDto menuRequest)
        {
            var menu = await _context.Menus.FindAsync(id);

            if (menu == null)
            {
                return null;
            }

            menu.Name = menuRequest.Name;
            menu.ParentId = menuRequest.ParentId;

            await _context.SaveChangesAsync();

            return menu.ToDto();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var menu = await _context.Menus.FindAsync(id);

            if (menu == null)
            {
                return false;
            }

            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();

            return true;
        }
    }

}