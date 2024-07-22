using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;

namespace VintageTimePieceRepository.IRepository
{
    public interface ITimepieceRepository : IBaseRepository<Timepiece>
    {
        // R
        public Task<List<TimepieceViewModel>> GetAllTimepiece();
        public Task<List<TimepieceViewModel>> GetAllTimepieceNotEvaluate();
        public Task<List<TimepieceViewModel>> GetAllTimepieceExceptUser(User user);
        public Task<TimepieceViewModel?> GetTimepieceById(int id);
        public Task<Timepiece?> GetOneTimepiece(int id);

        public Task<List<TimepieceViewModel>> GetTimepieceByName(string name);
        public Task<List<TimepieceViewModel>> GetTimepieceByNameExceptUser(string name, User user);
        public Task<List<TimepieceViewModel>> GetTimepieceByCategory(int categoryId);
        public Task<List<TimepieceViewModel>> GetAllTimepieceHasEvaluate(User user);
        // CUD
        public Task<Timepiece> UploadNewTimepiece(Timepiece timepiece);
        public Task<Timepiece> UpdateTimepiecePrice(int timepieceId, int price);
        public Task UpdateTimepieceIsOrder(List<OrdersDetail> ordersDetails, bool isOrder);
    }
}
