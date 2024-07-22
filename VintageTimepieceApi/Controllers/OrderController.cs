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
        private ITimepiecesService _timepieceService;
        public OrderController(IOrderService orderService,
            IJwtConfigService jwtConfigService, ITimepiecesService timepieceService)
        {
            _orderService = orderService;
            _jwtConfigService = jwtConfigService;
            _timepieceService = timepieceService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            HttpContext.Request.Cookies.TryGetValue("access_token", out var token);
            var user = _jwtConfigService.GetUserFromAccessToken(token);
            var result = await _orderService.GetOrderOfUser(user.Data.UserId);
            return Ok(result);
        }
        [HttpGet, Route("GetAllOrder")]
        public async Task<IActionResult> GetAllOrder()
        {
            var result = await _orderService.GetAllOrder();
            return Ok(result);
        }

        [HttpPut, Route("UpdateOrderStatus")]
        public async Task<IActionResult> UpdateOrderStatus([FromForm] int orderId, [FromForm] string status)
        {
            var result = await _orderService.UpdateOrderStatus(orderId, status);
            if (result.isSuccess)
            {
                if (status == "cancled")
                {
                    await _timepieceService.UpdateTimepieceOrder(result.Data.OrdersDetails.ToList(), false);
                }
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
