using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VintageTimepieceModel.Models
{
    public class RefundTransaction
    {
        public int RefundId { get; set; }
        public decimal? RefundAmount { get; set; }
        public string? RefundType { get; set; }
        public string? RefundInfo { get; set; }
        public string? RefundBankCode { get; set; }
        public DateTime? RefundDate { get; set; }
        [JsonIgnore]
        public virtual Transactions? Transaction { get; set; }
    }
}
