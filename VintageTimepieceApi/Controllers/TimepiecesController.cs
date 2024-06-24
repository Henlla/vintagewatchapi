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
        private ITimepiecesService _timepieceService { get; }
        private IJwtConfigService _jwtConfigService { get; }
        private IImageService _imageService { get; }

        public TimepiecesController(ITimepiecesService timepiecesService,
            IJwtConfigService jwtConfigService, IImageService imageService)
        {
            _timepieceService = timepiecesService;
            _jwtConfigService = jwtConfigService;
            _imageService = imageService;
        }

        [HttpGet, Route("GetAllProduct")]
        public async Task<IActionResult> GetAllProduct()
        {
            var result = new APIResponse<List<TimepieceViewModel>>();
            HttpContext.Request.Cookies.TryGetValue("access_token", out var token);
            if (token == null)
            {
                result = await _timepieceService.GetAllTimepiece();
                return Ok(result);
            }
            var user = _jwtConfigService.GetUserFromAccessToken(token);
            if (user.isSuccess)
            {
                result = await _timepieceService.GetAllTimepieceExceptUser(user.Data);
                return Ok(result);
            }
            return BadRequest(user);
        }



        //[HttpGet, Route("GetAllProductExeptUser")]
        //public async Task<IActionResult> GetAllProductExeptUser()
        //{
        //    var user = _jwtConfigService.GetUserFromAccessToken(token);
        //    if (user.isSuccess)
        //    {
        //        var timepieces = await _timepieceService.GetAllTimepieceExceptUser(user.Data);
        //        if (timepieces.isSuccess)
        //        {
        //            return Ok(timepieces);
        //        }
        //        else
        //        {
        //            return BadRequest(timepieces);
        //        }
        //    }
        //    return BadRequest(user);
        //}



        [HttpGet, Route("GetAllProductWithPaging")]
        public async Task<IActionResult> GetAllProductWithPaging([FromQuery] PagingModel pagingModel)
        {
            var result = await _timepieceService.GetAllTimepieceWithPaging(pagingModel);
            return Ok(result);
        }



        [HttpGet, Route("GetAllProductExceptUserWithPaging")]
        public async Task<IActionResult> GetAllProductExceptUserWithPaging([FromQuery] string token, [FromQuery] PagingModel pagingModel)
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



        [HttpGet, Route("GetAllProductByName")]
        public async Task<IActionResult> GetAllProductByName([FromQuery] string name)
        {
            var result = await _timepieceService.GetTimepieceByName(name);
            return Ok(result);
        }

        [HttpGet, Route("GetAllProductByNameExceptUser")]
        public async Task<IActionResult> GetAllProductByNameExceptUser([FromQuery] string name, [FromQuery] string token)
        {
            var user = _jwtConfigService.GetUserFromAccessToken(token);
            if (user.isSuccess)
            {
                var result = await _timepieceService.GetTimepieceByNameExceptUser(name, user.Data);
                if (result.isSuccess)
                    return Ok(result);
                return BadRequest(result);
            }
            return BadRequest(user);
        }


        [HttpGet, Route("GetProductById/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _timepieceService.GetOneTimepiece(id);
            return Ok(result);
        }




        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "USERS")]
        [HttpPost, Route("uploadTimepiece")]
        public async Task<IActionResult> Post([FromForm] List<IFormFile> files)
        {
            foreach (var file in files)
            {
                var result = await Task.FromResult(_imageService.uploadImage(file, "report"));
            }

            //foreach (var file in files)
            //{
            //    var result = await Task.FromResult(_imageService.uploadImage(file, "product"));
            //}
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
