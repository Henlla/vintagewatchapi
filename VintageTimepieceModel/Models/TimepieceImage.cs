﻿using System;
using System.Collections.Generic;

namespace VintageTimepieceModel.Models;

public partial class TimepieceImage
{
    public int TimepieceImageId { get; set; }

    public string? ImageUrl { get; set; }

    public bool? IsDel { get; set; }

    public int? TimpieceId { get; set; }

    public virtual Timepiece? Timpiece { get; set; }
}
