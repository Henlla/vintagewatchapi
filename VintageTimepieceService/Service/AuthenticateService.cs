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
        public async Task<APIResponse<User>> CheckLogin(LoginModel user)
        {
            var result = await _authenticateRepository.checkLogin(user);
            if (result == null)
                return new APIResponse<User>
                {
                    Message = "Login fail",
                    isSuccess = false,
                };
            return new APIResponse<User>
            {
                Message = "Login success",
                isSuccess = true,
                Data = result
            };
        }

        public async Task<APIResponse<User>> GetUsersByEmail(string value)
        {
            var result = await _authenticateRepository.GetUserByEmail(value);
            if (result == null)
                return new APIResponse<User>
                {
                    Message = "Get user fail",
                    isSuccess = false,
                };
            return new APIResponse<User>
            {
                Message = "Get user success",
                isSuccess = true,
                Data = result
            };
        }

        public async Task<APIResponse<User>> RegisterAccount(RegisterModel registerAccount)
        {
            var result = await _authenticateRepository.CreateNewAccount(registerAccount);
            if (result != null)
                return new APIResponse<User>
                {
                    Message = "Register user fail, User exists",
                    isSuccess = false,
                    Data = result
                };
            return new APIResponse<User>
            {
                Message = "Register success",
                isSuccess = true,
                Data = result
            };
        }
    }
}
