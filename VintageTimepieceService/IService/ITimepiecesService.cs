using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;

namespace VintageTimepieceService.IService
{
    public interface ITimepiecesService
    {
        // R
        public Task<APIResponse<List<TimepieceViewModel>>> GetAllTimepiece();
        public Task<APIResponse<List<TimepieceViewModel>>> GetAllTimepieceExceptUser(User user);
        public Task<APIResponse<PageList<TimepieceViewModel>>> GetAllTimepieceWithPaging(PagingModel pageModel);
        public Task<APIResponse<PageList<TimepieceViewModel>>> GetAllTimepieceWithPagingExceptUser(User user, PagingModel pagingModel);
        public Task<APIResponse<TimepieceViewModel>> GetOneTimepiece(int id);
        public Task<APIResponse<List<TimepieceViewModel>>> GetTimepieceByName(string name);
        public Task<APIResponse<List<TimepieceViewModel>>> GetTimepieceByNameExceptUser(string name, User user);
        public Task<APIResponse<TimepieceViewModel>> DeleteTimepiece(int id);
        public Task<APIResponse<List<TimepieceViewModel>>> GetTimepieceByCategory(int categoryId);
        public Task<APIResponse<TimepieceViewModel>> UpdateTimepiece(int id, TimepieceViewModel timepiece);

    }
}
