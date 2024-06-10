using System.IdentityModel.Tokens.Jwt;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;

namespace VintageTimePieceRepository.IRepository
{
    public interface IJwtConfigRepository
    {
        public APIResponse<JwtSecurityToken> GenerateFromAccountToJwtToken(User user);

        public APIResponse<string> TokenIsExpired(string token);

        public APIResponse<User> GenerateFromJwtTokenToAccount(string token);
    }
}
