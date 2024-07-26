using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;
using VintageTimePieceRepository.Util;
using VintageTimepieceService.IService;

namespace VintageTimepieceApi.Controllers
{
    [Route("payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private ITransactionService _transactionService;
        private ITimepiecesService _timepiecesService;
        private IOrderService _orderService;
        private IOrderDetailService _orderDetailService;
        private IConfiguration _configuration;
        private VintagedbContext _dbContext;
        private IVNPayService _vnpayService;
        private IHelper _helper;

        public PaymentController(ITransactionService transactionService,
            ITimepiecesService timepiecesService, VintagedbContext dbContext,
            IOrderService orderService, IOrderDetailService orderDetailService,
            IConfiguration configuration, IHelper helper,
            IVNPayService vnpayService)
        {
            _transactionService = transactionService;
            _timepiecesService = timepiecesService;
            _orderService = orderService;
            _orderDetailService = orderDetailService;
            _configuration = configuration;
            _dbContext = dbContext;
            _helper = helper;
            _vnpayService = vnpayService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _transactionService.GetAllTransactions();
            if (result.isSuccess)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
        [HttpPost, Route("RequestPayment")]
        public async Task<IActionResult> Payment([FromBody] PaymentInformation paymentInformation)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var resultTimepiece = await _timepiecesService.GetOneTimepiece(paymentInformation.TimepieceId);
                    if (!resultTimepiece.isSuccess)
                    {
                        return BadRequest(resultTimepiece);
                    }

                    var order = new Order
                    {
                        OrderDate = DateTime.UtcNow,
                        TotalPrice = resultTimepiece?.Data?.timepiece?.Price,
                        Status = "pending",
                        UserId = paymentInformation.UserId,
                    };
                    // Create Order
                    var orderResult = await _orderService.CreateOrder(order);
                    if (!orderResult.isSuccess)
                    {
                        return BadRequest(orderResult);
                    }

                    var orderDetail = new OrdersDetail
                    {
                        TimepieceId = paymentInformation.TimepieceId,
                        OrderId = orderResult?.Data?.OrderId,
                        UnitPrice = resultTimepiece?.Data?.timepiece?.Price,
                        Quantity = 1
                    };
                    // Create Order Detail
                    var orderDetailResult = await _orderDetailService.CreateOrderDetail(orderDetail);
                    if (!orderDetailResult.isSuccess)
                    {
                        await transaction.RollbackAsync();
                        return BadRequest(orderDetailResult);
                    }

                    await transaction.CommitAsync();

                    var requestPayment = new VNPayRequestModel
                    {
                        OrderId = orderResult.Data.OrderId,
                        OrderDate = orderResult.Data.OrderDate.Value,
                        Amount = Math.Round(resultTimepiece.Data.timepiece.Price.Value),
                        Description = paymentInformation.Description,
                        FirstName = paymentInformation.FirstName,
                        LastName = paymentInformation.LastName,
                        PhoneNumber = paymentInformation.PhoneNumber,
                        Email = paymentInformation.Email,
                        Address = paymentInformation.Address,
                    };

                    var paymentUrl = _vnpayService.CreatePaymentUrl(HttpContext, requestPayment);
                    return Ok(paymentUrl);
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(500, "Internal server");
                }
            }
        }

        [HttpPost, Route("callBackPayment")]
        public async Task<IActionResult> CallbackPayment([FromBody] Dictionary<string, string> queryParams)
        {
            var response = _vnpayService.PaymentExcute(queryParams);
            var result = new APIResponse<Transaction>();

            if (response == null || response.ResponseCode != "00")
            {
                result.isSuccess = false;
                result.Message = "Payment fail";
                if (!string.IsNullOrEmpty(response?.OrderId))
                {
                    await _orderService.UpdateOrderStatus(int.Parse(response.OrderId), "fail");
                }
                return BadRequest(result);
            }

            var transData = new Transaction
            {
                OrderId = int.Parse(response.OrderId),
                BankCode = response.BankCode,
                PaymentMethod = response.PaymentMethod,
                TransactionDate = response.PayDate,
                Amount = response.Amount / 100,
                TransactionStatus = response.TransactionStatus == "00" ? "Success" : "Fail",
                Description = response.OrderDescription,
            };

            var resultTransaction = await _transactionService.CreateTransaction(transData);
            if (resultTransaction.isSuccess)
            {
                result.isSuccess = true;
                result.Message = "Payment success";
                result.Data = resultTransaction.Data;

                await _orderService.UpdateOrderStatus(int.Parse(response.OrderId), "success");
                var orderDetails = await _orderDetailService.GetAllOrderDetailOfOrder(int.Parse(response.OrderId));
                await _timepiecesService.UpdateTimepieceOrder(orderDetails.Data, true);
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
