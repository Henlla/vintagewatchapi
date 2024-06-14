using System.IdentityModel.Tokens.Jwt;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;

namespace VintageTimePieceRepository.IRepository
{
    public interface IAuthenticateRepository : IBaseRepository<User>
    {
        public Task<User> CreateNewAccount(RegisterModel registerUser);
        public Task<User> GetUserByEmail(string email);
        public Task<User> checkLogin(LoginModel loginModel);
    }
}
