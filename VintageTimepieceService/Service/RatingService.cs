using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;
using VintageTimePieceRepository.IRepository;
using VintageTimepieceService.IService;

namespace VintageTimepieceService.Service
{
    public class RatingService : IRatingService
    {
        private IRatingRepository _ratingRepository { get; }
        public RatingService(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }
        public async Task<APIResponse<List<RatingsTimepiece>>> GetAllRatingOfTimepiece(int timepieceId)
        {
            var result = await _ratingRepository.GetAllRatingOfTimepiece(timepieceId);
            bool isSuccess = false;
            if (result.Count > 0)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Get rating success" : "No rating availables";
            return new APIResponse<List<RatingsTimepiece>>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }

        public async Task<APIResponse<RatingsTimepiece>> GetRatingOfUser(int? userId, int? timepieceId)
        {
            var result = await _ratingRepository.GetRatingOfUser(userId, timepieceId);
            bool isSuccess = false;
            if (result == null)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Rating successesful" : "You have rating this product";

            return new APIResponse<RatingsTimepiece>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }

        public async Task<APIResponse<RatingsTimepiece>> MakeRating(RatingsTimepiece rating)
        {
            var result = await _ratingRepository.MakeRating(rating);
            bool isSuccess = false;
            if (result != null)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Rating product success" : "Rating product fail";

            return new APIResponse<RatingsTimepiece>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }
    }
}
