using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;

namespace VintageTimePieceRepository.IRepository
{
    public interface ITimepieceRepository : IBaseRepository<Timepiece>
    {
        public Task<Timepiece> GetTimepieceById(int id);
        public Task<List<Timepiece>> GetTimepieceByName(string name);
        public Task<PageList<Timepiece>> GetAllTimepieceWithPaging(PagingModel paginModel);
        public Task<List<Timepiece>> GetAllTimepiece();
    }
}
