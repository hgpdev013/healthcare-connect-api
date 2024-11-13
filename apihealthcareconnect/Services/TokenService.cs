using apihealthcareconnect.Interfaces;
using apihealthcareconnect.ViewModel.Reponses.Login;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace apihealthcareconnect.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException();
        }

        public string GenerateJwtToken(int userId, string email, string userType, DateTime expires)
        {
            var claims = new[]
            {
                new Claim("email", email),
                new Claim("userId", userId.ToString()),
                new Claim("userType", userType),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, DateTime.UtcNow.AddHours(1).ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var audiences = _configuration.GetSection("JwtSettings:Audiences").Get<string[]>();

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string? GetDataFromJwtToken(string token, string dataKey)
        {
            var handler = new JwtSecurityTokenHandler();

            if (!handler.CanReadToken(token))
            {
                return null;
            }

            var jsonToken = handler.ReadJwtToken(token);

            var response = jsonToken?.Claims?.FirstOrDefault(c => c.Type == dataKey)?.Value;

            return response;
        }
    }
}
