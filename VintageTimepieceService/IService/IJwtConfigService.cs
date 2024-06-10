using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models.Shared;
using VintageTimepieceModel.Models;

namespace VintageTimepieceService.IService
{
    public interface IJwtConfigService
    {
        public APIResponse<JwtSecurityToken> GetAccessTokenFromUser(User user);
        public APIResponse<string> CheckValidToken(string token);
        public APIResponse<User> GetUserFromAccessToken(string token);


    }
}
