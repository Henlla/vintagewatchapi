using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VintageTimepieceService.IService;

namespace VintageTimepieceApi.Controllers
{
    [Route("transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private ITransactionService _transactionService;
        private IJwtConfigService _jwtConfigService;
        private IOrderService _orderService;
        public TransactionController(ITransactionService transactionService,
                                    IJwtConfigService jwtConfigService,
                                    IOrderService orderService)
        {
            _transactionService = transactionService;
            _jwtConfigService = jwtConfigService;
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _transactionService.GetAllTransactions();
            return Ok(result);
        }

        [HttpGet, Route("GetTransactionOfUser")]
        public async Task<IActionResult> GetTransactionOfUser()
        {
            HttpContext.Request.Cookies.TryGetValue("access_token", out var token);
            var user = _jwtConfigService.GetUserFromAccessToken(token);
            var result = await _transactionService.GetAllTransactionsOfUsers(user.Data);
            return Ok(result);
        }

        [HttpGet, Route("GetTransactionOfOrder")]
        public async Task<IActionResult> GetTransactionOfOrder([FromQuery] int orderId)
        {
            var order = await _orderService.GetOrderById(orderId);
            var result = await _transactionService.GetTransactionOfOrder(orderId);
            return Ok(result);
        }
    }
}
