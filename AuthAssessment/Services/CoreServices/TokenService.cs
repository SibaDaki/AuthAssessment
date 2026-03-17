using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthAssessment.Services.IServices;
using Microsoft.IdentityModel.Tokens;
using AuthAssessment.Entities;


namespace AuthAssessment.Services.CoreServices
{

    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private const int TokenExpirationMinutes = 1440; // 24 hours

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"] ?? "your-super-secret-key-that-is-at-least-256-bits-long-change-this";
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("FirstName", user.FirstName),
            new Claim("LastName", user.LastName)
        };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(TokenExpirationMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}