using VintageTimepieceModel.Models;

namespace VintageTimePieceRepository.IRepository
{
    public interface IUsersRepository : IGeneric<User>
    {
        public Task<User> GetUserByUsernameAndPassword(string username, string password);
    }
}
