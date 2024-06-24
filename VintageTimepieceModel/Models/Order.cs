using System;
using System.Collections.Generic;

namespace VintageTimepieceModel.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal? TotalPrice { get; set; }

    public int? UserId { get; set; }

    public bool? IsDel { get; set; }

    public virtual ICollection<OrdersDetail> OrdersDetails { get; set; } = new List<OrdersDetail>();

    public virtual User? User { get; set; }
}
