using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;

namespace VintageTimepieceService.IService
{
    public interface IRatingService
    {
        public Task<APIResponse<List<RatingsTimepiece>>> GetAllRatingOfTimepiece(int timepieceId);
        public Task<APIResponse<RatingsTimepiece>> GetRatingOfUser(int? userId, int? timepieceId);
        public Task<APIResponse<RatingsTimepiece>> MakeRating(RatingsTimepiece rating);
    }
}
