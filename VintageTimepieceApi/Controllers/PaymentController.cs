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
        private IHelper _helper;

        public PaymentController(ITransactionService transactionService,
            ITimepiecesService timepiecesService, VintagedbContext dbContext,
            IOrderService orderService, IOrderDetailService orderDetailService,
            IConfiguration configuration, IHelper helper)
        {
            _transactionService = transactionService;
            _timepiecesService = timepiecesService;
            _orderService = orderService;
            _orderDetailService = orderDetailService;
            _configuration = configuration;
            _dbContext = dbContext;
            _helper = helper;
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
        [HttpPost, Route("Checkout")]
        public async Task<IActionResult> MakePayment([FromForm] string paymentInformation)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var paymentData = JsonConvert.DeserializeObject<PaymentInformation>(paymentInformation);
                    var resultTimepiece = await _timepiecesService.GetOneTimepiece(paymentData.TimepieceId);
                    if (!resultTimepiece.isSuccess)
                    {
                        return BadRequest(resultTimepiece);
                    }

                    var order = new Order
                    {
                        OrderDate = DateTime.UtcNow,
                        TotalPrice = resultTimepiece?.Data?.timepiece?.Price,
                        Status = "pending",
                        UserId = paymentData.UserId,
                    };
                    // Create Order
                    var orderResult = await _orderService.CreateOrder(order);
                    if (!orderResult.isSuccess)
                    {
                        return BadRequest(orderResult);
                    }

                    var orderDetail = new OrdersDetail
                    {
                        TimepieceId = paymentData.TimepieceId,
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

                    var vnpayInfomation = new VNPayDataModel
                    {
                        vnp_TmnCode = _configuration["VNPAY:vnp_TmnCode"],
                        vnp_TxnRef = orderResult?.Data?.OrderId.ToString(),
                        vnp_OrderInfo = paymentData.Description,
                        vnp_Amount = (Math.Round(resultTimepiece.Data.timepiece.Price.Value) * 100).ToString(),
                        vnp_ReturnUrl = _configuration["VNPAY:vnp_ReturnUrl"],
                        vnp_CreateDate = DateTime.UtcNow.ToString("yyyyMMddHHmmss"),
                        vnp_Bill_Mobile = paymentData.PhoneNumber,
                        vnp_Bill_Email = paymentData.Email,
                        vnp_Bill_FirstName = paymentData.FirstName,
                        vnp_Bill_LastName = paymentData.LastName,
                        vnp_Bill_Address = paymentData.Address,
                        vnp_Inv_Phone = paymentData.PhoneNumber,
                        vnp_Inv_Email = paymentData.Email,
                        vnp_Inv_Customer = paymentData.FirstName + " " + paymentData.LastName,
                        vnp_Inv_Address = paymentData.Address,
                        vnp_Inv_Company = "Công ty VNPAY",
                    };

                    var queryString = vnpayInfomation.ToQueryString();
                    var vnp_Url = _configuration["VNPAY:vnp_Url"];
                    var vnp_HashSecret = _configuration["VNPAY:vnp_HashSecret"];
                    string paymentUrl = _helper.CreatePaymentRequestUrl(vnp_Url, vnp_HashSecret, queryString);
                    return Ok(paymentUrl);
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(500, "Internal server");
                }
            }
        }
    }
}
