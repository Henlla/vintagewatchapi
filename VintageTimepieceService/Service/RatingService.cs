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
            var result = await Task.FromResult(_ratingRepository.GetAllRatingOfTimepiece(timepieceId));
            if (result.Count > 0)
            {
                return new APIResponse<List<RatingsTimepiece>>
                {
                    Message = "Get rating success",
                    isSuccess = true,
                    Data = result
                };
            }
            return new APIResponse<List<RatingsTimepiece>>
            {
                Message = "Don't have rating",
                isSuccess = false,
                Data = result
            };
        }

        public async Task<APIResponse<List<RatingsTimepiece>>> GetAllRatingOfUser(int userId)
        {
            var result = await Task.FromResult(_ratingRepository.GetAllRatingOfUser(userId));
            if (result.Count > 0)
                return new APIResponse<List<RatingsTimepiece>>
                {
                    Message = "Get rating of user success",
                    isSuccess = true,
                    Data = result
                };
            return new APIResponse<List<RatingsTimepiece>>
            {
                Message = "Don't have any rating",
                isSuccess = false,
                Data = result
            };
        }

        public async Task<APIResponse<RatingsTimepiece>> MakeRating(RatingsTimepiece rating)
        {
            var result = await Task.FromResult(_ratingRepository.MakeRating(rating));
            if (result != null)
                return new APIResponse<RatingsTimepiece>
                {
                    Message = "Make rating success",
                    isSuccess = true,
                    Data = result
                };
            return new APIResponse<RatingsTimepiece>
            {
                Message = "Make rating fail",
                isSuccess = false,
                Data = result
            };
        }
    }
}
