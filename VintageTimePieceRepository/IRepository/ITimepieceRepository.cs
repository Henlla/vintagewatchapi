using Microsoft.AspNetCore.Http;
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
        public Task<List<Timepiece>> GetAllTimepieceExceptUser(User user);
        public Task<PageList<Timepiece>> GetAllTimepieceWithPagingExceptUser(User user, PagingModel pagingModel);
        public Task<List<TimepieceImageModel>> GetAllTimePieceWithImage();
        public Task<List<TimepieceImageModel>> GetAllTimePieceWithImageExeptUser();

    }
}
