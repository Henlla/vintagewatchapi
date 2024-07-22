using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimePieceRepository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace VintageTimePieceRepository.Repository
{
    public class RatingRepository : BaseRepository<RatingsTimepiece>, IRatingRepository
    {
        public RatingRepository(VintagedbContext context) : base(context)
        {
        }

        public async Task<List<RatingsTimepiece>> GetAllRatingOfTimepiece(int timepiceId)
        {
            var result = await _context.RatingsTimepieces
                                        .Where(r => r.IsDel == false && r.TimepieceId == timepiceId)
                                        .ToListAsync();
            return result;
        }

        public async Task<RatingsTimepiece?> GetRatingOfUser(int? userId, int? timepieceId)
        {
            var result = await FindAll()
                .Where(r => r.UserId == userId && r.TimepieceId == timepieceId && r.IsDel == false)
                .SingleOrDefaultAsync();
            return result;
        }

        public async Task<RatingsTimepiece> MakeRating(RatingsTimepiece rating)
        {
            var result = await Add(rating);
            return result;
        }
    }
}
