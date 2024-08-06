using Backend.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Business.Services
{

    namespace Backend.Business.Services
    {
        public class JwtService
        {
            private readonly IConfiguration _configuration;

            public JwtService(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public string GenerateToken(User user)
            {
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
                return tokenHandler.WriteToken(token);
            }
        }
    }


}
