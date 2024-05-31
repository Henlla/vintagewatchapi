using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;

namespace VintageTimepieceService.IService
{
    public interface IAuthenticateService
    {
        public Task<User> CheckLogin(LoginModel user);
        public Task<JwtSecurityToken> GetAccessToken(User user);
        public Task<string> GetRefreshToken();
        public Task SaveRefreshToken(RefreshToken refreshToken);
        public Task<APIResponse<string>> ReNewtoken(TokenModel model);
    }
}
