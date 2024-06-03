using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using VintageTimepieceModel;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;
using VintageTimePieceRepository.IRepository;

namespace VintageTimePieceRepository.Repository
{
    public class AuthenticateRepository : IAuthenticateRepository
    {
        private readonly IConfiguration _configuration;
        private readonly VintagedbContext _context;
        private readonly IHashPasswordRepository _passwordRepository;


        public AuthenticateRepository(IConfiguration configuration,
            VintagedbContext context,
            IHashPasswordRepository hashPasswordRepository)
        {
            _configuration = configuration;
            _context = context;
            _passwordRepository = hashPasswordRepository;
        }
        public async Task<User?> GetUserByUserName(LoginModel user)
        {
            var userLogin = await _context.Users.SingleOrDefaultAsync(u => u.Username.Equals(user.Username)
            || u.Email.Equals(user.Username));

            if (!_passwordRepository.VerifyPassword(userLogin.Password, user.Password))
            {
                return null;
            }
            return userLogin;
        }

        public async Task<JwtSecurityToken> GenerateJwtToken(User user)
        {
            var fullName = user.FirstName + " " + user.LastName;
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
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
                expires: DateTime.Now.AddSeconds(20),
                signingCredentials: signIn);
            return token;
        }

        public async Task<string> GenerateRefreshToken()
        {
            var refreshTokenString = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            return refreshTokenString;
        }


        public async Task SaveRefreshToken(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();
        }


        public async Task<APIResponse<string>> GenerateNewToken(TokenModel model)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var secretBytesKey = Encoding.UTF8.GetBytes(_configuration["Jwt:JwtSecret"]);
            var tokenValidateParam = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretBytesKey),
                ValidateLifetime = false
            };
            try
            {
                // Access token check validate
                var tokenVerification = jwtHandler.ValidateToken(model.AccessToken, tokenValidateParam, out var validatedToken);

                // check alg
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
                    if (!result)
                    {
                        return new APIResponse<string>
                        {
                            Message = "Invalid Token",
                            isSuccess = false
                        };
                    }
                }

                // check access token expired
                var utcDate = long.Parse(tokenVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                var expiredDate = ConvertUnixDateToDateTime(utcDate);
                if (expiredDate > DateTime.Now)
                {
                    return new APIResponse<string>
                    {
                        Message = "Access Token is expired",
                        isSuccess = false
                    };
                }

                // Check refresh token is exists in db
                var storedRefreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token.Equals(model.RefreshToken));
                if (storedRefreshToken == null)
                {
                    return new APIResponse<string>
                    {
                        Message = "Refresh token not exists",
                        isSuccess = false
                    };
                }

                // Check access token id and refresh token id
                var jti = tokenVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                if (!storedRefreshToken.JwtId.Equals(jti))
                {
                    return new APIResponse<string>
                    {
                        Message = "Token doesn't match",
                        isSuccess = false
                    };
                }

                // Update used token and revoke token
                storedRefreshToken.IsUsed = true;
                storedRefreshToken.IsRevoke = true;
                _context.Update(storedRefreshToken);
                await _context.SaveChangesAsync();


                // create new token
                var user = await _context.Users.SingleOrDefaultAsync(x => x.UserId.Equals(storedRefreshToken.UserId));
                var token = await GenerateJwtToken(user);
                var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

                return new APIResponse<string>
                {
                    Message = "Renew token success",
                    isSuccess = true,
                    Data = accessToken
                };
            }
            catch (Exception e)
            {
                return new APIResponse<string>
                {
                    Message = "Exception_Authenticate_Repository: " + e.Message,
                    isSuccess = false
                };
            }
        }


        private DateTime ConvertUnixDateToDateTime(long utcDate)
        {
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval.AddSeconds(utcDate).ToUniversalTime();
            return dateTimeInterval;
        }


        public async Task<APIResponse<string>> CreateNewAccount(RegisterModel registerUser)
        {
            var existsUserEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(registerUser.Email));
            var existsUsername = await _context.Users.FirstOrDefaultAsync(u => u.Username.Equals(registerUser.Username));
            if (existsUserEmail != null || existsUsername != null)
            {
                return new APIResponse<string>
                {
                    Message = "User exists!",
                    isSuccess = false,
                };
            }
            var newUser = new User();
            newUser.Username = registerUser.Username;
            newUser.Password = _passwordRepository.Hash(registerUser.Password);
            newUser.Email = registerUser.Email;
            newUser.DateJoined = DateTime.Now;
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return new APIResponse<string>
            {
                Message = "Register success",
                isSuccess = true,
            };
        }
    }
}
