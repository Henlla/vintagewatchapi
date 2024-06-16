using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;
using VintageTimePieceRepository.IRepository;
using VintageTimepieceService.IService;

namespace VintageTimepieceApi.Controllers
{
    [Route("timepiece")]
    [ApiController]
    public class TimepiecesController : ControllerBase
    {
        private readonly ITimepiecesService _timepieceService;
        private readonly IJwtConfigService _jwtConfigService;
        private readonly ITimepieceRepository _timepieceRepository;
        public TimepiecesController(ITimepiecesService timepiecesService, IJwtConfigService jwtConfigService, ITimepieceRepository timepieceRepository)
        {
            _timepieceService = timepiecesService;
            _jwtConfigService = jwtConfigService;
            _timepieceRepository = timepieceRepository;
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
            if (user.isSuccess)
            {
                var result = await _timepieceService.GetAllTimepieceWithPagingExceptUser(user.Data, pagingModel);
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
        [HttpPost, Route("uploadTimepiece")]
        public async Task<IActionResult> Post([FromForm] List<IFormFile> files, [FromForm] Timepiece timepiece)
        {

            foreach (var file in files)
            {
                var result = await Task.FromResult(_timepieceRepository.UploadImage(file));
            }
            return Ok("");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "USERS")]
        [HttpPut, Route("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TimepieceViewModel timepiece)
        {
            var result = await _timepieceService.UpdateTimepiece(id, timepiece);
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
