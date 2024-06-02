using System.IdentityModel.Tokens.Jwt;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;
using VintageTimePieceRepository.IRepository;
using VintageTimepieceService.IService;

namespace VintageTimepieceService.Service
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IAuthenticateRepository _authenticateRepository;
        public AuthenticateService(IAuthenticateRepository authenticateRepository)
        {
            _authenticateRepository = authenticateRepository;
        }
        public async Task<User> CheckLogin(LoginModel user)
        {
            return await _authenticateRepository.GetUserByUserNameAndPassword(user);
        }

        public async Task<JwtSecurityToken> GetAccessToken(User user)
        {
            return await _authenticateRepository.GenerateJwtToken(user);
        }

        public async Task<string> GetRefreshToken()
        {
            return await _authenticateRepository.GenerateRefreshToken();
        }


        public async Task SaveRefreshToken(RefreshToken refreshToken)
        {
            await _authenticateRepository.SaveRefreshToken(refreshToken);
        }
        public async Task<APIResponse<string>> ReNewtoken(TokenModel model)
        {
            return await _authenticateRepository.GenerateNewToken(model);
        }

        public async Task<APIResponse<string>> RegisterAccount(RegisterModel registerAccount)
        {
            return await _authenticateRepository.CreateNewAccount(registerAccount);
        }
    }
}
