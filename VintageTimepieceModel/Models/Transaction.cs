using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace VintageTimepieceModel.Models
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public int OrderId { get; set; }
        public string? BankCode { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal? Amount { get; set; }
        public string? TransactionStatus { get; set; }
        public string? Description { get; set; }
        public bool IsDel { get; set; }
        public virtual Order? Order { get; set; }
    }
}
