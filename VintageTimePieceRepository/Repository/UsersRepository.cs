using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VintageTimepieceModel;
using VintageTimepieceModel.Models;
using VintageTimePieceRepository.IRepository;

namespace VintageTimePieceRepository.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly VintagedbContext _context;
        public UsersRepository(VintagedbContext context)
        {
            _context = context;
        }

        public Task Add(User t)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> Get()
        {
            return await _context.Users.ToListAsync();
        }

        public Task GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByUsernameAndPassword(string username, string password)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Username.Equals(username) && x.Password.Equals(password));
        }

        public Task Update(User t)
        {
            throw new NotImplementedException();
        }
    }
}
