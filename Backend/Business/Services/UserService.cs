using Backend.Data;
using Backend.Models;
using Backend.Business.Requests;
using Microsoft.EntityFrameworkCore;
using Backend.Business.Mapping;

namespace Backend.Business.Services
{
    public class UserService
    {
        private readonly SahibindenContext _context;

        public UserService(SahibindenContext context)
        {
            _context = context;
        }

        public async Task<List<UserRequestDto>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users.Select(user => user.ToDto()).ToList();
        }

        public async Task<UserRequestDto?> GetByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user?.ToDto();
        }

        public async Task<UserRequestDto> AddAsync(UserRequestDto userRequest)
        {
            var user = userRequest.ToEntity();

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user.ToDto();
        }

        public async Task<UserRequestDto?> UpdateAsync(int id, UserRequestDto userRequest)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return null;
            }

            user.Username = userRequest.Username;
            user.Email = userRequest.Email;
            user.Password = userRequest.Password;

            await _context.SaveChangesAsync();

            return user.ToDto();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<int?> GetUserIdByUsernameAsync(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            return user?.UserId; // Kullanıcı bulunursa userId'yi, bulunamazsa null döner.
        }

    }
}
