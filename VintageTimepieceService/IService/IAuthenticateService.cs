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
        public Task<APIResponse<User>> CheckLogin(LoginModel user);
        public Task<APIResponse<User>> GetUsersByEmail(string value);
        public Task<APIResponse<User>> RegisterAccount(RegisterModel registerAccount);
    }
}
