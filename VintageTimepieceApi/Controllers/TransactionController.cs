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
        public TransactionController(ITransactionService transactionService,
                                    IJwtConfigService jwtConfigService)
        {
            _transactionService = transactionService;
            _jwtConfigService = jwtConfigService;
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
    }
}
