using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;

namespace VintageTimepieceService.IService
{
    public interface IAuthenticateService
    {
        // R
        public Task<APIResponse<User>> CheckLogin(LoginModel user);
        public Task<APIResponse<User>> GetUsersByEmail(string value);
        public Task<APIResponse<IEnumerable<User>>> GetAllUser();
        public Task<APIResponse<User>> GetOneUser(int userId);

        // CUD
        public Task<APIResponse<User>> RegisterAccount(RegisterModel registerAccount);
        public Task<APIResponse<User>> UpdateUserInformation(int userId, User user);
        public Task<APIResponse<User>> DeleteUser(int id);
        public Task<APIResponse<User>> UpdateUserImage(IFormFile file, int userId);
    }
}
