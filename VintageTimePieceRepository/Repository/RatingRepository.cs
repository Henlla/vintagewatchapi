using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimePieceRepository.IRepository;

namespace VintageTimePieceRepository.Repository
{
    public class RatingRepository : BaseRepository<RatingsTimepiece>, IRatingRepository
    {
        public RatingRepository(VintagedbContext context) : base(context)
        {
        }

        public List<RatingsTimepiece> GetAllRatingOfTimepiece(int timepiceId)
        {
            var result = FindAll().Where(r => r.IsDel == false && r.TimepieceId == timepiceId).ToList();
            return result;
        }

        public List<RatingsTimepiece> GetAllRatingOfUser(int userId)
        {
            var result = FindAll().Where(r => r.UserId == userId && r.IsDel == false).ToList();
            return result;
        }

        public RatingsTimepiece MakeRating(RatingsTimepiece rating)
        {
            var result = Add(rating);
            return result;
        }
    }
}
