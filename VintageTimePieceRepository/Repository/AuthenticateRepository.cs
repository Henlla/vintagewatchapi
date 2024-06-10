using Microsoft.EntityFrameworkCore;
using VintageTimepieceModel;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;
using VintageTimePieceRepository.IRepository;

namespace VintageTimePieceRepository.Repository
{
    public class AuthenticateRepository : BaseRepository<User>, IAuthenticateRepository
    {
        private readonly IHashPasswordRepository _passwordRepository;

        public AuthenticateRepository(VintagedbContext context,
            IHashPasswordRepository hashPasswordRepository) : base(context)
        {
            _passwordRepository = hashPasswordRepository;
        }

        public async Task<User> CreateNewAccount(RegisterModel registerUser)
        {
            var existsUser = await Task.FromResult(await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(registerUser.Email) && u.IsDel == false));
            if (existsUser == null)
            {
                var newUser = new User();
                var role = await _context.Roles.FirstOrDefaultAsync(x => x.RoleName.Equals("USERS") && x.IsDel == false);
                newUser.Password = registerUser.Password == null ? null : _passwordRepository.Hash(registerUser.Password);
                newUser.FirstName = registerUser.FirstName;
                newUser.LastName = registerUser.LastName;
                newUser.Email = registerUser.Email;
                newUser.DateJoined = DateTime.Now;
                newUser.RoleId = role.RoleId;
                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();
                return newUser;
            }
            return null;
        }

        public async Task<User> GetUserByEmail(string value)
        {
            var result = await Task.FromResult(await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(value) && u.IsDel == false));
            if (result == null)
                return null;
            return result;
        }
    }
}
