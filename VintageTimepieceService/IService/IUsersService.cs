using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.ResponseModel;

namespace VintageTimepieceService.IService
{
    public interface IUsersService
    {
        public Task<User> CheckLogin(string username, string password);
        public Task<List<User>> GetAllUsers();
    }
}
