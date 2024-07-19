using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using VintageTimepieceModel;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;
using VintageTimePieceRepository.IRepository;
using VintageTimePieceRepository.Util;

namespace VintageTimePieceRepository.Repository
{
    public class AuthenticateRepository : BaseRepository<User>, IAuthenticateRepository
    {
        private string defaultAvatar = "https://firebasestorage.googleapis.com/v0/b/vintagetimepece.appspot.com/o/avatar%2Fuserdefault.png?alt=media";
        private readonly IHashPasswordRepository _passwordRepository;
        private readonly IHelper _helper;

        public AuthenticateRepository(VintagedbContext context,
            IHashPasswordRepository hashPasswordRepository,
            IHelper helper) : base(context)
        {
            _passwordRepository = hashPasswordRepository;
            _helper = helper;
        }
        // R
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
        public async Task<User?> GetUserByEmail(string email)
        {
            var result = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email) && u.IsDel == false);
            if (result == null)
                return null;
            return result;
        }
        public async Task<IEnumerable<User>> GetAllUser()
        {
            var result = (from user in _context.Users
                          join role in _context.Roles on user.RoleId equals role.RoleId
                          where user.IsDel == false && role.IsDel == false
                          && role.RoleName == "USERS"
                          select user).AsEnumerable();
            return await Task.FromResult(result);
        }
        public async Task<User?> GetUserById(int userId)
        {
            var result = Task.FromResult(_context.Users.Where(u => u.UserId == userId && u.IsDel == false).SingleOrDefault());
            return await result;
        }
        // CUD
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
                newUser.PhoneNumber = registerUser.phoneNumber;
                newUser.Address = registerUser.address;
                newUser.RoleId = role.RoleId;
                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();
                return null;
            }
            return existsUser;
        }
        public async Task<User> UpdateUserInformation(int userId, User user)
        {
            var currentUser = _context.Users.Single(u => u.UserId == userId && u.IsDel == false);
            currentUser.Email = user.Email;
            currentUser.FirstName = user.FirstName;
            currentUser.LastName = user.LastName;
            currentUser.PhoneNumber = user.PhoneNumber;
            currentUser.Address = user.Address;
            var result = Update(currentUser);
            return await Task.FromResult(result);
        }
        public async Task<User> DeleteUser(int userId)
        {
            var currentData = _context.Users.Where(u => u.UserId == userId && u.IsDel == false).Single();
            currentData.IsDel = true;
            var result = Update(currentData);
            return await Task.FromResult(result);
        }
        public async Task<User?> UpdateUserImage(IFormFile file, int userId)
        {
            var user = _context.Users.Where(u => u.UserId == userId && u.IsDel == false).SingleOrDefault();
            if (user == null)
            {
                return await Task.FromResult(user);
            }
            var base64String = _helper.ConvertFileToBase64(file).Result;
            await _helper.DeleteImageFromFireBase(user.Avatar);
            var imageString = await _helper.UploadImageToFirebase(base64String, "avatar");
            user.Avatar = imageString;
            var result = Update(user);
            return await Task.FromResult(result);
        }
    }
}
