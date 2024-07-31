using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageTimepieceModel.Models.Shared
{
    public class VNPayRefundResponseModel
    {
        public string vnp_ResponseId { get; set; }
        public string vnp_Command { get; set; }
        public string vnp_TmnCode { get; set; }
        public string vnp_TxnRef { get; set; }
        public string vnp_Amount { get; set; }
        public string vnp_OrderInfo { get; set; }
        public string vnP_ResponseCode { get; set; }
        public string vnp_Message { get; set; }
        public string vnp_BankCode { get; set; }
        public string vnp_SecureHash { get; set; }
    }
}
