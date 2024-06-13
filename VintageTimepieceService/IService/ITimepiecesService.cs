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
        public Task<APIResponse<List<TimepieceModel>>> GetAllTimepiece();
        public Task<APIResponse<List<TimepieceModel>>> GetAllTimepieceExceptUser(User user);
        public Task<APIResponse<PageList<TimepieceModel>>> GetAllTimepieceWithPaging(PagingModel pageModel);
        public Task<APIResponse<PageList<TimepieceModel>>> GetAllTimepieceWithPagingExceptUser(User user, PagingModel pagingModel);
        public Task<APIResponse<TimepieceModel>> GetOneTimepiece(int id);
        public Task<APIResponse<List<TimepieceModel>>> GetTimepieceByName(string name);
        public Task<APIResponse<TimepieceModel>> DeleteTimepiece(int id);
        public Task<APIResponse<TimepieceModel>> UpdateTimepiece(int id, TimepieceModel timepiece);

    }
}
