using System;
using System.Collections.Generic;

namespace VintageTimepieceModel.Models;

public partial class OrdersDetail
{
    public int OrderDetailId { get; set; }

    public int? TimepieceId { get; set; }

    public int? OrderId { get; set; }

    public int? Quantity { get; set; }

    public decimal? UnitPrice { get; set; }

    public bool? IsDel { get; set; } = false;

    public virtual Order? Order { get; set; }

    public virtual Timepiece? Timepiece { get; set; }
}
