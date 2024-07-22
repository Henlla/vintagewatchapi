using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;

namespace VintageTimePieceRepository.IRepository
{
    public interface IRatingRepository
    {
        public Task<List<RatingsTimepiece>> GetAllRatingOfTimepiece(int timepiceId);
        public Task<RatingsTimepiece?> GetRatingOfUser(int? userId, int? timepieceId);
        public Task<RatingsTimepiece> MakeRating(RatingsTimepiece rating);
    }
}
