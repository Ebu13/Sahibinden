using Backend.Business.Services;
using Backend.Business.Requests;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
        [Authorize]
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
        [Authorize]
        public async Task<ActionResult<User>> PostUser(UserRequestDto userRequest)
        {
            var user = await _userService.AddAsync(userRequest);
            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }

        [HttpPut("{id}")]
        [Authorize]
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
        [Authorize]
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

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key"));

            // Null check for Jwt:ExpiresInMinutes
            var expiresInMinutesString = _configuration["Jwt:ExpiresInMinutes"];
            if (string.IsNullOrEmpty(expiresInMinutesString))
            {
                throw new ArgumentNullException("Jwt:ExpiresInMinutes", "Configuration value cannot be null or empty.");
            }

            if (!double.TryParse(expiresInMinutesString, out double expiresInMinutes))
            {
                throw new ArgumentException("Jwt:ExpiresInMinutes configuration value is not a valid number.");
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(expiresInMinutes),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Return the JWT token along with user details
            return Ok(new
            {
                Token = tokenString,
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password // It's not recommended to return the password in responses
            });
        }



    }
}
