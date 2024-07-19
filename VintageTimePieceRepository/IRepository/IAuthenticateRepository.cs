using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;

namespace VintageTimePieceRepository.IRepository
{
    public interface IAuthenticateRepository : IBaseRepository<User>
    {
        // R
        public Task<User?> GetUserById(int userId);
        public Task<User?> GetUserByEmail(string email);
        public Task<User?> checkLogin(LoginModel loginModel);
        // CUD
        public Task<User?> CreateNewAccount(RegisterModel registerUser);
        public Task<User> UpdateUserInformation(int userId, User user);
        public Task<User> DeleteUser(int userId);
        public Task<User?> UpdateUserImage(IFormFile file, int userId);
    }
}
