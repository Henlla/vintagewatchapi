using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using VintageTimepieceModel;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;
using VintageTimePieceRepository.IRepository;

namespace VintageTimePieceRepository.Repository
{
    public class AuthenticateRepository : BaseRepository<User>, IAuthenticateRepository
    {
        private string defaultAvatar = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/avatar%2Fuserdefault.png?alt=media";
        private readonly IHashPasswordRepository _passwordRepository;

        public AuthenticateRepository(VintagedbContext context,
            IHashPasswordRepository hashPasswordRepository) : base(context)
        {
            _passwordRepository = hashPasswordRepository;
        }

        public async Task<User?> checkLogin(LoginModel loginModel)
        {
            var getUsers = await _context.Users.Where(us => us.Email.Equals(loginModel.Email) && us.IsDel == false).SingleOrDefaultAsync();
            if (getUsers == null)
            {
                return null;
            }
            var hashPassword = _passwordRepository.VerifyPassword(getUsers.Password, loginModel.Password);
            if (!hashPassword)
            {
                return null;
            }
            return getUsers;
        }

        public async Task<User?> CreateNewAccount(RegisterModel registerUser)
        {
            var existsUser = await GetUserByEmail(registerUser.Email);
            if (existsUser == null)
            {
                var newUser = new User();
                var role = await _context.Roles.FirstOrDefaultAsync(x => x.RoleName.Equals("USERS") && x.IsDel == false);
                newUser.Email = registerUser.Email;
                newUser.Password = registerUser.Password == null ? null : _passwordRepository.Hash(registerUser.Password);
                newUser.FirstName = registerUser.FirstName;
                newUser.LastName = registerUser.LastName;
                newUser.DateJoined = DateTime.Now;
                newUser.Avatar = defaultAvatar;
                newUser.RoleId = role.RoleId;
                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();
                return null;
            }
            return existsUser;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var result = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email) && u.IsDel == false);
            if (result == null)
                return null;
            return result;
        }

        public async Task<User?> updateUser(int userID, User user)
        {

            var currentUser = await GetUserById(userID);
            currentUser.FirstName = user.FirstName;
            currentUser.LastName = user.LastName;
            currentUser.PhoneNumber = user.PhoneNumber;
            currentUser.Email = user.Email;
            currentUser.Address = user.Address;
            var result = Update(currentUser);
            return await Task.FromResult( result);

        }

        public async Task<User?> GetUserById(int userID)
        {
            var result = await _context.Users.FirstOrDefaultAsync(u=>u.UserId==userID);
            return result;

        }
    }
}
