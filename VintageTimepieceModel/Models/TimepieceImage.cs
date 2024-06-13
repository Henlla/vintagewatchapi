using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VintageTimepieceModel.Models;

public partial class TimepieceImage
{
    public int TimepieceImageId { get; set; }
    public string? ImageName { get; set; }

    public string? ImageUrl { get; set; }

    public bool? IsDel { get; set; }

    public int? TimepieceId { get; set; }
    public virtual Timepiece? Timepiece { get; set; }
}
