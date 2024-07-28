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
            var message = "";
            var orderStatus = "";

            if (response == null || response.ResponseCode != "00")
            {
                switch (response.ResponseCode)
                {
                    case "05":
                        orderStatus = "fail";
                        message = "Payment fail! Your balance not enough to pay this";
                        break;
                    case "06":
                        orderStatus = "fail";
                        message = "Payment fail! You enter the wrong OTP";
                        break;
                    case "09":
                        orderStatus = "fail";
                        message = "Payment fail! Your account not register in the system";
                        break;
                    case "10":
                        orderStatus = "fail";
                        message = "Payment fail! You have wrong information more 3 times";
                        break;
                    case "11":
                        orderStatus = "fail";
                        message = "Payment fail! Payment is timeout";
                        break;
                    case "12":
                        orderStatus = "fail";
                        message = "Payment fail! Your card is lock";
                        break;
                    case "24":
                        orderStatus = "cancled";
                        message = "Payment fail! You have canceled this payment";
                        break;
                    case "79":
                        orderStatus = "fail";
                        message = "Payment fail! You enter password wrong too much times";
                        break;
                    case "65":
                        orderStatus = "fail";
                        message = "Payment fail! Your account is exceed daily transaction limit";
                        break;
                    case "75":
                        orderStatus = "fail";
                        message = "Payment fail! Bank is on maintence";
                        break;
                    default:
                        orderStatus = "fail";
                        message = "Payment fail! Something wrong with payment contact the system admin";
                        break;
                }
                result.isSuccess = false;
                result.Message = message;
                if (!string.IsNullOrEmpty(response?.OrderId))
                {
                    await _orderService.UpdateOrderStatus(int.Parse(response.OrderId), orderStatus);
                }
                return BadRequest(result);
            }
            else
            {
                orderStatus = "success";
                message = "Payment successful! Thank you for buying and choosing us system";
            }

            var transData = new Transaction
            {
                OrderId = int.Parse(response.OrderId),
                BankCode = response.BankCode,
                PaymentMethod = response.PaymentMethod,
                TransactionDate = response.PayDate,
                Amount = response.Amount / 100,
                TransactionStatus = orderStatus,
                Description = response.OrderDescription,
            };

            var resultTransaction = await _transactionService.CreateTransaction(transData);
            if (resultTransaction.isSuccess)
            {
                result.isSuccess = true;
                result.Message = "Payment success";
                result.Data = resultTransaction.Data;

                await _orderService.UpdateOrderStatus(int.Parse(response.OrderId), orderStatus);
                var orderDetails = await _orderDetailService.GetAllOrderDetailOfOrder(int.Parse(response.OrderId));
                await _timepiecesService.UpdateTimepieceOrder(orderDetails.Data, true);
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
