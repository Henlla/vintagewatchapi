using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;

namespace VintageTimePieceRepository.IRepository
{
    public interface IAuthenticateRepository
    {
        public Task<User> GetUserByUserNameAndPassword(LoginModel user);
        public Task<JwtSecurityToken> GenerateJwtToken(User user);
        public Task<string> GenerateRefreshToken();
        public Task SaveRefreshToken(RefreshToken refreshToken);
        public Task<APIResponse<string>> GenerateNewToken(TokenModel model);
    }
}
