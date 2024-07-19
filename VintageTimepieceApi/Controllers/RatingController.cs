using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VintageTimepieceModel.Models;
using VintageTimepieceService.IService;

namespace VintageTimepieceApi.Controllers
{
    [Route("rating")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private IRatingService _ratingService;
        private IJwtConfigService _jwtConfigService;
        public RatingController(IRatingService ratingService, IJwtConfigService jwtConfigService)
        {
            _ratingService = ratingService;
            _jwtConfigService = jwtConfigService;
        }

        [HttpGet("{timepieceId}")]
        public async Task<IActionResult> Get(int timepieceId)
        {
            var result = await _ratingService.GetAllRatingOfTimepiece(timepieceId);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "USERS")]
        public async Task<IActionResult> Post([FromForm] string ratingString)
        {
            HttpContext.Request.Cookies.TryGetValue("access_token", out var token);
            if (token == null)
            {
                return Unauthorized();
            }
            var user = _jwtConfigService.GetUserFromAccessToken(token);
            var ratingData = JsonConvert.DeserializeObject<RatingsTimepiece>(ratingString);
            ratingData.UserId = user.Data.UserId;
            ratingData.RatingDate = DateTime.Now;
            var existsRating = await _ratingService.GetRatingOfUser(ratingData.UserId, ratingData.TimepieceId);
            if (!existsRating.isSuccess)
            {
                return BadRequest(existsRating);
            }
            var result = await _ratingService.MakeRating(ratingData);
            if (result.isSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
