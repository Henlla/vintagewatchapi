using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;
using VintageTimepieceService.IService;

namespace VintageTimepieceApi.Controllers
{
    [Route("timepiece")]
    [ApiController]
    public class TimepiecesController : ControllerBase
    {
        private readonly ITimepiecesService _timepieceService;
        private readonly IJwtConfigService _jwtConfigService;
        public TimepiecesController(ITimepiecesService timepiecesService, IJwtConfigService jwtConfigService)
        {
            _timepieceService = timepiecesService;
            _jwtConfigService = jwtConfigService;
        }

        [HttpGet, Route("GetAllProductExeptUser")]
        public async Task<IActionResult> Get([FromQuery] string token)
        {
            var result = _jwtConfigService.GetUserFromAccessToken(token);
            if (result.isSuccess)
            {
                var timepieces = await _timepieceService.GetAllTimepieceExceptUser(result.Data);
                if (timepieces.isSuccess)
                {
                    return Ok(timepieces);
                }
                else
                {
                    return BadRequest(timepieces);
                }
            }
            return BadRequest(result);
        }

        [HttpGet, Route("GetAllProductExceptUserWithPaging")]
        public async Task<IActionResult> Get([FromQuery] string token, [FromQuery] PagingModel pagingModel)
        {
            var user = _jwtConfigService.GetUserFromAccessToken(token);
            var result = new APIResponse<PageList<Timepiece>>();
            if (user.isSuccess)
            {
                result = await _timepieceService.GetAllTimepieceWithPagingExceptUser(user.Data, pagingModel);
                if (result.isSuccess)
                    return Ok(result);
                return BadRequest(result);
            }
            else
            {
                return BadRequest(user);
            }
        }

        [HttpGet, Route("GetAllProductWithPaging")]
        public async Task<IActionResult> Get([FromQuery] PagingModel pagingModel)
        {
            var result = await _timepieceService.GetAllTimepieceWithPaging(pagingModel);
            if (!result.isSuccess)
                return NotFound(result);
            return Ok(result);
        }

        [HttpGet, Route("GetAllProduct")]
        public async Task<IActionResult> Get()
        {
            var result = await _timepieceService.GetAllTimepiece();
            if (!result.isSuccess)
                return NotFound(result);
            return Ok(result);
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _timepieceService.GetOneTimepiece(id);
            if (!result.isSuccess)
                return NotFound(result);
            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "USERS")]
        [HttpPost]
        public async Task<IActionResult> Post(string token, [FromBody] TimepieceImageModel timepiece)
        {
            var result = await _timepieceService.UploadNewTimepiece(timepiece.timepiece);
            if (!result.isSuccess)
                return BadRequest(result);
            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "USERS")]
        [HttpPut, Route("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Timepiece value)
        {
            var result = await _timepieceService.UpdateTimepiece(id, value);
            if (!result.isSuccess)
                return BadRequest(result);
            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "USERS")]
        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _timepieceService.DeleteTimepiece(id);
            if (!result.isSuccess)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
