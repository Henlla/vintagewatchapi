using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models.Shared;

namespace VintageTimepieceService.IService
{
    public interface IVNPayService
    {
        public string CreatePaymentUrl(HttpContext context, VNPayRequestModel requestModel);
        VNPayResponseModel PaymentExcute(Dictionary<string, string> collection);
        public VNPayRefundResponseModel CreatePaymentRefund(HttpContext context, VNPayRefundRequestModel requestModel);
    }
}
