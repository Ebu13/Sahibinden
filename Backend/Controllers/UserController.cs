using Backend.Business.Services;
using Backend.Business.Requests;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Backend.Business.Mapping;
using Microsoft.AspNetCore.Authorization;
using Backend.Logging;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IConfiguration _configuration;
        private readonly ILoggerService _logger;

        public UserController(IConfiguration configuration, UserService userService, ILoggerService logger)
        {
            _configuration = configuration;
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            _logger.LogInfo("GetUsers endpoint hit.");
            try
            {
                var users = await _userService.GetAllAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetUsers: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            _logger.LogInfo($"GetUser endpoint hit with id: {id}");
            try
            {
                var user = await _userService.GetByIdAsync(id);
                if (user == null)
                {
                    _logger.LogWarn($"User with id: {id} not found.");
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetUser: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<User>> PostUser(UserRequestDto userRequest)
        {
            _logger.LogInfo("PostUser endpoint hit.");
            try
            {
                var user = await _userService.AddAsync(userRequest);
                return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in PostUser: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutUser(int id, UserRequestDto userRequest)
        {
            _logger.LogInfo($"PutUser endpoint hit with id: {id}");
            try
            {
                var updatedUser = await _userService.UpdateAsync(id, userRequest);
                if (updatedUser == null)
                {
                    _logger.LogWarn($"User with id: {id} not found.");
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in PutUser: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            _logger.LogInfo($"DeleteUser endpoint hit with id: {id}");
            try
            {
                var deleted = await _userService.DeleteAsync(id);
                if (!deleted)
                {
                    _logger.LogWarn($"User with id: {id} not found.");
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteUser: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("username/{username}")]
        [Authorize]
        public async Task<ActionResult<int>> GetUserIdByUsername(string username)
        {
            _logger.LogInfo($"GetUserIdByUsername endpoint hit with username: {username}");
            try
            {
                var userId = await _userService.GetUserIdByUsernameAsync(username);
                if (userId == null)
                {
                    _logger.LogWarn($"Username {username} not found.");
                    return NotFound();
                }
                return Ok(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetUserIdByUsername: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            _logger.LogInfo("Login endpoint hit.");
            try
            {
                var user = await _userService.ValidateUserAsync(
                    loginRequest.Username ?? throw new ArgumentNullException(nameof(loginRequest.Username)),
                    loginRequest.Password ?? throw new ArgumentNullException(nameof(loginRequest.Password))
                );

                if (user == null)
                {
                    _logger.LogWarn($"Invalid login attempt for username: {loginRequest.Username}");
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
                    Role = user.Role
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Login: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
