using Microsoft.AspNetCore.Http;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;

namespace VintageTimePieceRepository.IRepository
{
    public interface ITimepieceRepository : IBaseRepository<Timepiece>
    {
        // R
        public Task<List<TimepieceModel>> GetAllTimepiece();
        public Task<List<TimepieceModel>> GetAllTimepieceExceptUser(User user);
        public Task<PageList<TimepieceModel>> GetAllTimepieceWithPaging(PagingModel paginModel);
        public Task<PageList<TimepieceModel>> GetAllTimepieceWithPagingExceptUser(User user, PagingModel pagingModel);
        public Task<TimepieceModel?> GetTimepieceById(int id);
        public Task<List<TimepieceModel>> GetTimepieceByName(string name);
        public Task<List<TimepieceModel>> GetTimepieceByNameExceptUser(string name, User user);
        public Task<List<TimepieceModel>> GetTimepieceByCategory(string categoryName);

        public Task<string>? UploadImage(IFormFile files);
    }
}
