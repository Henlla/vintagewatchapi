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
        public List<RatingsTimepiece> GetAllRatingOfTimepiece(int timepiceId);
        public RatingsTimepiece? GetRatingOfUser(int? userId, int? timepieceId);
        public RatingsTimepiece MakeRating(RatingsTimepiece rating);
    }
}
