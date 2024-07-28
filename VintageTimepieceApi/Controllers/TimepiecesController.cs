using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;
using VintageTimepieceService.IService;
using VintageTimepieceService.Service;

namespace VintageTimepieceApi.Controllers
{
    [Route("timepiece")]
    [ApiController]
    public class TimepiecesController : ControllerBase
    {
        private ITimepiecesService _timepieceService { get; }
        private IJwtConfigService _jwtConfigService { get; }
        private IImageService _imageService { get; }
        private IOrderService _orderService { get; }
        private IOrderDetailService _orderDetailService { get; }

        public TimepiecesController(ITimepiecesService timepiecesService,
            IJwtConfigService jwtConfigService, IImageService imageService,
            IOrderService orderService, IOrderDetailService orderDetailService)
        {
            _timepieceService = timepiecesService;
            _jwtConfigService = jwtConfigService;
            _imageService = imageService;
            _orderService = orderService;
            _orderDetailService = orderDetailService;
        }

        [HttpGet, Route("GetPageListProduct")]
        public async Task<IActionResult> GetPageListProduct([FromQuery] PagingModel pagingModel)
        {
            HttpContext.Request.Cookies.TryGetValue("access_token", out var token);
            var user = _jwtConfigService.GetUserFromAccessToken(token);
            var result = await _timepieceService.GetAllTimepieceWithPageList(pagingModel, user.Data);
            return Ok(result);
        }

        [HttpGet, Route("GetProductByCategory")]
        public async Task<IActionResult> GetProductByCategory([FromQuery] PagingModel pagingModel, [FromQuery] string categoryName)
        {
            HttpContext.Request.Cookies.TryGetValue("access_token", out var token);
            var user = _jwtConfigService.GetUserFromAccessToken(token);
            var result = await _timepieceService.GetAllTimepieceByCategoryNameWithPaging(categoryName, user.Data, pagingModel);
            return Ok(result);
        }

        [HttpGet, Route("GetProductByNameAndCategory")]
        public async Task<IActionResult> GetProductByNameAndCategory([FromQuery] string? name, [FromQuery] int categoryId)
        {
            HttpContext.Request.Cookies.TryGetValue("access_token", out var token);
            var user = _jwtConfigService.GetUserFromAccessToken(token);
            var result = await _timepieceService.GetProductByNameAndCategory(name, categoryId, user.Data);
            return Ok(result);
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

        [HttpGet, Route("GetAllTimepieceNotEvaluate")]
        public async Task<IActionResult> GetAllTimepieceNotEvaluate([FromQuery] string? keyword)
        {
            var result = await _timepieceService.GetAllTimepieceNotEvaluate(keyword);
            return Ok(result);
        }

        [HttpGet, Route("GetAllProductByName")]
        public async Task<IActionResult> GetAllProductByName([FromQuery] string? name)
        {
            HttpContext.Request.Cookies.TryGetValue("access_token", out var token);
            var user = _jwtConfigService.GetUserFromAccessToken(token);
            var result = await _timepieceService.GetAllTimepieceByName(name, user.Data);
            return Ok(result);
        }

        [HttpGet, Route("GetProductById/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _timepieceService.GetOneTimepiece(id);
            return Ok(result);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "USERS")]
        [HttpGet, Route("GetEvaluationTimepiece")]
        public async Task<IActionResult> GetEvaluationTimepiece()
        {
            HttpContext.Request.Cookies.TryGetValue("access_token", out var token);
            if (token == null)
            {
                return Unauthorized();
            }
            var user = _jwtConfigService.GetUserFromAccessToken(token);
            var result = await _timepieceService.GetTimepieceHasEvaluate(user.Data);
            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "USERS")]
        [HttpPost, Route("requestEvaluation")]
        public async Task<IActionResult> Post([FromForm] List<IFormFile> files, [FromForm] string timepiece)
        {

            HttpContext.Request.Cookies.TryGetValue("access_token", out var access_token);
            var user = _jwtConfigService.GetUserFromAccessToken(access_token);
            var data = JsonConvert.DeserializeObject<Timepiece>(timepiece);
            data.DatePost = DateTime.Now;
            data.UserId = user.Data.UserId;
            var resultTimepiece = await _timepieceService.UploadNewTimepiece(data);
            if (!resultTimepiece.isSuccess)
            {
                return BadRequest(resultTimepiece);
            }

            foreach (var file in files)
            {
                var resultUploadImage = await _imageService.uploadImage(file, "product");
                if (!resultUploadImage.isSuccess)
                {
                    return BadRequest(resultUploadImage);
                }
                TimepieceImage timepieceImage = new TimepieceImage();
                timepieceImage.TimepieceId = resultTimepiece.Data?.TimepieceId;
                timepieceImage.ImageName = resultTimepiece.Data?.TimepieceName;
                timepieceImage.ImageUrl = resultUploadImage.Data;
                var resultTimepieceImage = await _imageService.CreateNewTimepieceImage(timepieceImage);
                if (!resultTimepieceImage.isSuccess)
                {
                    return BadRequest(resultTimepieceImage);
                }
            }
            return Ok(resultTimepiece);
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

        [HttpPut, Route("UpdateTimepiecePrice")]
        public async Task<IActionResult> UpdateTimepiecePrice([FromForm] int timepieceId, [FromForm] int price)
        {
            var result = await _timepieceService.UpdateTimepiecePrice(timepieceId, price);
            if (result.isSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete, Route("DeleteTimepiece/{id}")]
        public async Task<IActionResult> DeleteTimepiece(int id)
        {
            var result = await _timepieceService.DeleteTimepiece(id);
            if (result.isSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
