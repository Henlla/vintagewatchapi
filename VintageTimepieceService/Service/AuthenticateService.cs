using Microsoft.AspNetCore.Http;
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
        // R
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
        public async Task<APIResponse<IEnumerable<User>>> GetAllUser()
        {
            var result = await _authenticateRepository.GetAllUser();
            return new APIResponse<IEnumerable<User>>
            {
                Message = "Get all user success",
                isSuccess = true,
                Data = result.ToList()
            };
        }
        public async Task<APIResponse<User>> GetOneUser(int userId)
        {
            var result = await _authenticateRepository.GetUserById(userId);
            if (result == null)
            {
                return new APIResponse<User>
                {
                    Message = "User not exists",
                    isSuccess = false,
                    Data = result
                };
            }
            return new APIResponse<User>
            {
                Message = "Get user success",
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

        // CUD
        public async Task<APIResponse<User>> DeleteUser(int id)
        {
            var result = await _authenticateRepository.DeleteUser(id);
            if (result == null)
            {
                return new APIResponse<User>
                {
                    Message = "Delete user fail",
                    isSuccess = false,
                    Data = result
                };
            }
            return new APIResponse<User>
            {
                Message = "Delete user success",
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
        public async Task<APIResponse<User>> UpdateUserImage(IFormFile file, int userId)
        {
            var result = await _authenticateRepository.UpdateUserImage(file, userId);
            if (result == null)
            {
                return new APIResponse<User>
                {
                    Message = "Update user fail",
                    isSuccess = false,
                    Data = result
                };
            }
            return new APIResponse<User>
            {
                Message = "Update user success",
                isSuccess = true,
                Data = result
            };
        }
        public async Task<APIResponse<User>> UpdateUserInformation(int userId, User user)
        {
            var result = await _authenticateRepository.UpdateUserInformation(userId, user);
            if (result == null)
            {
                return new APIResponse<User>
                {
                    Message = "Update information fail",
                    isSuccess = false,
                    Data = result
                };
            }
            return new APIResponse<User>
            {
                Message = "Update information success",
                isSuccess = true,
                Data = result
            };
        }
    }
}
