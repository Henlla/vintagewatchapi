using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;
using VintageTimePieceRepository.IRepository;
using VintageTimepieceService.IService;

namespace VintageTimepieceService.Service
{
    public class JwtConfigService : IJwtConfigService
    {
        private readonly IJwtConfigRepository _jwtConfigRepository;
        public JwtConfigService(IJwtConfigRepository jwtConfigRepository)
        {
            _jwtConfigRepository = jwtConfigRepository;
        }

        public APIResponse<string> CheckValidToken(string token)
        {
            return _jwtConfigRepository.TokenIsExpired(token);
        }

        public APIResponse<JwtSecurityToken> GetAccessTokenFromUser(User user)
        {
            return _jwtConfigRepository.GenerateFromAccountToJwtToken(user);
        }

        public APIResponse<User> GetUserFromAccessToken(string token)
        {
            return _jwtConfigRepository.GenerateFromJwtTokenToAccount(token);
        }
    }
}
