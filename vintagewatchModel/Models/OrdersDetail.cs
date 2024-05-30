using System;
using System.Collections.Generic;

#nullable disable

namespace vintagewatchModel.Models
{
    public partial class OrdersDetail
    {
        public int OrderDetailId { get; set; }
        public int? TimepieceId { get; set; }
        public int? OrderId { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }

        public virtual Order Order { get; set; }
        public virtual Timepiece Timepiece { get; set; }
    }
}
