using Backend.Business.Services;
using Backend.Business.Requests;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Backend.Business.Mapping;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration, UserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<User>> PostUser(UserRequestDto userRequest)
        {
            var user = await _userService.AddAsync(userRequest);
            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutUser(int id, UserRequestDto userRequest)
        {
            var updatedUser = await _userService.UpdateAsync(id, userRequest);

            if (updatedUser == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("username/{username}")]
        [Authorize]
        public async Task<ActionResult<int>> GetUserIdByUsername(string username)
        {
            var userId = await _userService.GetUserIdByUsernameAsync(username);

            if (userId == null)
            {
                return NotFound();
            }

            return Ok(userId);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var user = await _userService.ValidateUserAsync(loginRequest.Username ?? throw new ArgumentNullException(nameof(loginRequest.Username)),
                                                            loginRequest.Password ?? throw new ArgumentNullException(nameof(loginRequest.Password)));

            if (user == null)
            {
                return Unauthorized();
            }

            // JWT token oluşturma
            var jwtTokenService = new JwtService(_configuration);
            var tokenString = jwtTokenService.GenerateToken(user.ToEntity());

            // JWT token ve kullanıcı detaylarını döndürme
            return Ok(new
            {
                Token = tokenString,
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Role= user.Role // Şifreyi döndürmekten kaçının
            });
        }


    }
}
