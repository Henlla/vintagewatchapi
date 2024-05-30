using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using VintageTimepieceModel.Models;
using VintageTimePieceRepository.IRepository;

namespace VintageTimePieceRepository.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration _configuration;
        public TokenRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(User user)
        {
            var fullName = user.FirstName + " " + user.LastName;
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,user.UserId.ToString()),
                new Claim("UserId", user.UserId.ToString()),
                new Claim("DisplayName",fullName),
                new Claim("Username",fullName),
                new Claim("Email",user.Email.ToString()),
                new Claim("RoleName", user.Role.RoleName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:JwtSecret"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:JwtIssuer"],
                _configuration["Jwt:JwtAudience"],
                claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signIn);
            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }

        public RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken()
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(3),
                Created = DateTime.Now
            };
            return refreshToken;
        }
    }
}
