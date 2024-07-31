using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageTimepieceModel.Models.Shared
{
    public class OrderViewModel
    {
        public Order? Order {  get; set; }
        public Transactions? Transaction { get; set; }
        public bool TimeRemining { get; set; }
        public RefundTransaction? RefundTransaction { get; set; }
        public List<OrdersDetail>? OrderDetail { get; set; }
    }
}
