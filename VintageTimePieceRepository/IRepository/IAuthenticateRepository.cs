using System.IdentityModel.Tokens.Jwt;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;

namespace VintageTimePieceRepository.IRepository
{
    public interface IAuthenticateRepository
    {
        public Task<APIResponse<string>> CreateNewAccount(RegisterModel registerUser);
        public Task<User> GetUserByUserNameAndPassword(LoginModel user);
        public Task<JwtSecurityToken> GenerateJwtToken(User user);
        public Task<string> GenerateRefreshToken();
        public Task SaveRefreshToken(RefreshToken refreshToken);
        public Task<APIResponse<string>> GenerateNewToken(TokenModel model);
    }
}
