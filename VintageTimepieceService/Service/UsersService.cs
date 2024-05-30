using System.IdentityModel.Tokens.Jwt;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.ResponseModel;
using VintageTimePieceRepository.IRepository;
using VintageTimepieceService.IService;

namespace VintageTimepieceService.Service
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ITokenRepository _tokenRepository;
        public UsersService(IUsersRepository usersRepository, ITokenRepository tokenRepository)
        {
            _usersRepository = usersRepository;
            _tokenRepository = tokenRepository;

        }
        public async Task<User> CheckLogin(string username, string password)
        {
            return await _usersRepository.GetUserByUsernameAndPassword(username, password);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _usersRepository.Get();
        }
    }
}
