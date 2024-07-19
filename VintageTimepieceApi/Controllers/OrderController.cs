using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VintageTimepieceService.IService;

namespace VintageTimepieceApi.Controllers
{
    [Route("order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;
        private IJwtConfigService _jwtConfigService;
        public OrderController(IOrderService orderService,
            IJwtConfigService jwtConfigService)
        {
            _orderService = orderService;
            _jwtConfigService = jwtConfigService;

        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            HttpContext.Request.Cookies.TryGetValue("access_token", out var token);
            var user = _jwtConfigService.GetUserFromAccessToken(token);
            var result = await _orderService.GetOrderOfUser(user.Data.UserId);
            return Ok(result);
        }
    }
}
