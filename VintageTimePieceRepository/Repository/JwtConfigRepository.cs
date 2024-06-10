using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;
using VintageTimePieceRepository.IRepository;

namespace VintageTimePieceRepository.Repository
{
    public class JwtConfigRepository : IJwtConfigRepository
    {

        private readonly IConfiguration _configuration;
        private readonly VintagedbContext _context;

        public JwtConfigRepository(VintagedbContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }

        public APIResponse<JwtSecurityToken> GenerateFromAccountToJwtToken(User user)
        {
            var fullName = user.FirstName + " " + user.LastName;
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,user.Role.RoleName),
                new Claim("UserId", user.UserId.ToString()),
                new Claim("DisplayName",fullName),
                new Claim("Username",fullName),
                new Claim("RoleName", user.Role.RoleName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:JwtSecret"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:JwtIssuer"],
                _configuration["Jwt:JwtAudience"],
                claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: signIn);
            return new APIResponse<JwtSecurityToken>
            {
                Message = "Get token success",
                isSuccess = true,
                Data = token
            };
        }

        public APIResponse<User> GenerateFromJwtTokenToAccount(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            if (token.IsNullOrEmpty())
            {
                return new APIResponse<User>
                {
                    Message = "The token is empty",
                    isSuccess = false,
                };
            }
            var jwtToken = handler.ReadJwtToken(token);
            var claim = jwtToken.Claims.Select(c => new { c.Type, c.Value }).ToList();
            var userId = claim.FirstOrDefault(c => c.Type.Equals("UserId")).Value;
            var user = _context.Users.FirstOrDefault(u => u.UserId == int.Parse(userId));
            if (user == null)
            {
                return new APIResponse<User>
                {
                    Message = "Get user from token string fail",
                    isSuccess = false,
                };
            }
            return new APIResponse<User>
            {
                Message = "Get user from token string success",
                isSuccess = true,
                Data = user
            };
        }


        public APIResponse<string> TokenIsExpired(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:JwtIssuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:JwtAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:JwtSecret"]))
            };
            try
            {
                var claimPrincipal = handler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                if (validatedToken.ValidTo < DateTime.Now)
                {
                    return new APIResponse<string>
                    {
                        Message = "Token is expired",
                        isSuccess = true,
                    };
                }
                return new APIResponse<string>
                {
                    Message = "Token is valid",
                    isSuccess = false,
                };
            }
            catch (SecurityTokenExpiredException)
            {
                return new APIResponse<string>
                {
                    Message = "Token is expired",
                    isSuccess = true,
                };
            }
            catch (Exception)
            {
                return new APIResponse<string>
                {
                    Message = "Token validate fail",
                    isSuccess = false,
                };
            }
        }
    }
}
