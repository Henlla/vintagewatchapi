using System;
using System.Collections.Generic;

namespace VintageTimepieceModel.Models;

public partial class TimepieceCategory
{
    public int TimepieceCategoryId { get; set; }

    public int? TimepieceId { get; set; }

    public int? CategoryId { get; set; }

    public bool? IsDel { get; set; } = false;

    public virtual Category? Category { get; set; }

    public virtual Timepiece? Timepiece { get; set; }
}
