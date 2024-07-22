using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageTimepieceModel.Models.Shared
{
    public class OrderViewModel
    {
        public Order? order {  get; set; }
        public List<OrdersDetail>? orderDetail { get; set; }
    }
}
