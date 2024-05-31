using System;
using System.Collections.Generic;

namespace VintageTimepieceModel.Models;

public partial class TimepieceImage
{
    public int TimepieceImageId { get; set; }

    public string ImageUrl { get; set; }

    public virtual ICollection<Timepiece> Timepieces { get; set; } = new List<Timepiece>();
}
