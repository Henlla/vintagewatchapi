using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageTimepieceModel.Models.Shared
{
    public class VNPayRefundRequestModel
    {
        public int? OrderId { get; set; }
        public string? OrderInfo { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string? CreateBy { get; set; }
    }
}
