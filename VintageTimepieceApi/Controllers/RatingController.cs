using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VintageTimepieceService.IService;

namespace VintageTimepieceApi.Controllers
{
    [Route("rating")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private IRatingService _ratingService;
        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet("{timepieceId}")]
        public async Task<IActionResult> Get(int timepieceId)
        {
            var result = await _ratingService.GetAllRatingOfTimepiece(timepieceId);
            return Ok(result);
        }
    }
}
