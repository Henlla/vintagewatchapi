using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;
using VintageTimePieceRepository.Util;
using VintageTimepieceService.IService;

namespace VintageTimepieceService.Service
{
    public class VNPayService : IVNPayService
    {
        private IConfiguration _configuration;
        public VNPayService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreatePaymentUrl(HttpContext context, VNPayRequestModel requestModel)
        {
            // Config vnpay info
            string vnp_Url = _configuration["VNPAY:vnp_Url"].ToString();
            string vnp_ReturnUrl = _configuration["VNPAY:vnp_ReturnUrl"].ToString();
            string vnp_HashSecret = _configuration["VNPAY:vnp_HashSecret"].ToString();
            string vnp_TmnCode = _configuration["VNPAY:vnp_TmnCode"].ToString();
            string vnp_Version = _configuration["VNPAY:vnp_Version"].ToString();
            string vnp_Locale = _configuration["VNPAY:vnp_Locale"].ToString();
            string vnp_CurrCode = _configuration["VNPAY:vnp_CurrCode"].ToString();

            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", vnp_Version);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (requestModel.Amount * 100).ToString());
            //vnpay.AddRequestData("vnp_BankCode", "NCB");

            vnpay.AddRequestData("vnp_CreateDate", requestModel.OrderDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(context));
            vnpay.AddRequestData("vnp_Locale", vnp_Locale);
            vnpay.AddRequestData("vnp_OrderInfo", requestModel.Description);
            vnpay.AddRequestData("vnp_OrderType", "other");

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_ReturnUrl);
            vnpay.AddRequestData("vnp_TxnRef", requestModel.OrderId.ToString());

            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            return paymentUrl;
        }

        public VNPayResponseModel PaymentExcute(Dictionary<string, string> collection)
        {
            var vnpay = new VnPayLibrary();
            string vnp_HashSecret = _configuration["VNPAY:vnp_HashSecret"].ToString();

            foreach (var (key, value) in collection)
            {
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(key, value.ToString());
                }
            }
            var vnp_OrderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
            var vnp_TransactionId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
            var vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            var vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount"));
            var vnp_OrderInfo = vnpay.GetResponseData("vnp_OrderInfo");
            var vnp_BankCode = vnpay.GetResponseData("vnp_BankCode");
            var vnp_CardType = vnpay.GetResponseData("vnp_CardType");
            var vnp_PayDate = vnpay.GetResponseData("vnp_PayDate");
            var vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
            var vnp_SecureHash = collection.FirstOrDefault(p => p.Key == "vnp_SecureHash").Value;


            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
            if (!checkSignature)
            {
                return new VNPayResponseModel
                {
                    Success = false
                };
            }
            return new VNPayResponseModel
            {
                TransactionId = vnp_TransactionId.ToString(),
                OrderId = vnp_OrderId.ToString(),
                ResponseCode = vnp_ResponseCode.ToString(),
                Amount = vnp_Amount,
                OrderDescription = vnp_OrderInfo,
                BankCode = vnp_BankCode.ToString(),
                CardType = vnp_CardType.ToString(),
                PayDate = DateTime.ParseExact(vnp_PayDate, "yyyyMMddHHmmss", CultureInfo.CurrentCulture),
                TransactionStatus = vnp_TransactionStatus.ToString(),
                PaymentMethod = "VNPAY",
                Token = vnp_SecureHash.ToString(),
                Success = true,
            };
        }
    }
}
