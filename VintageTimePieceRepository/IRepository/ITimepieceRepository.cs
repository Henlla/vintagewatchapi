using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;

namespace VintageTimePieceRepository.IRepository
{
    public interface ITimepieceRepository : IBaseRepository<Timepiece>
    {
        // R
        public List<TimepieceViewModel> GetAllTimepiece();
        public List<TimepieceViewModel> GetAllTimepieceExceptUser(User user);
        public PageList<TimepieceViewModel> GetAllTimepieceWithPaging(PagingModel paginModel);
        public PageList<TimepieceViewModel> GetAllTimepieceWithPagingExceptUser(User user, PagingModel pagingModel);
        public TimepieceViewModel? GetTimepieceById(int id);
        public List<TimepieceViewModel> GetTimepieceByName(string name);
        public List<TimepieceViewModel> GetTimepieceByNameExceptUser(string name, User user);
        public List<TimepieceViewModel> GetTimepieceByCategory(int categoryId);

        // CUD
        public Timepiece UploadNewTimepiece(Timepiece timepiece);
    }
}
