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
        public Task<APIResponse<PageList<Timepiece>>> GetAllTimepieceWithPaging(PagingModel pageModel);
        public Task<APIResponse<List<Timepiece>>> GetAllTimepiece();
        public Task<APIResponse<Timepiece>> CreateNewTimepiece(Timepiece timepiece);
        public Task<APIResponse<Timepiece>> DeleteTimepiece(int id);
        public Task<APIResponse<Timepiece>> GetOneTimepiece(int id);
        public Task<APIResponse<Timepiece>> UpdateTimepiece(int id, Timepiece timepiece);

    }
}
