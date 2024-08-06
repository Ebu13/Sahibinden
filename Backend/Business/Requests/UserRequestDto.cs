using Backend.Models;

namespace Backend.Business.Requests
{
    public class UserRequestDto
    {
        public int UserId { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Role { get; set; } = null!;


    }
}
