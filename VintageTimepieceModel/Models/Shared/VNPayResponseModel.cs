using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageTimepieceModel.Models.Shared
{
    public class VNPayResponseModel
    {
        public string TransactionId { get; set; }
        public string OrderId { get; set; }
        public string ResponseCode { get; set; }
        public decimal Amount { get; set; }
        public string OrderDescription { get; set; }
        public string BankCode { get; set; }
        public string CardType { get; set; }
        public DateTime PayDate { get; set; }
        public string TransactionStatus { get; set; }
        public string PaymentMethod { get; set; }
        public string Token { get; set; }
        public bool Success { get; set; }
    }
}
