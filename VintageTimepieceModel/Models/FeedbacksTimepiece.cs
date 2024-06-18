using System;
using System.Collections.Generic;

namespace VintageTimepieceModel.Models;

public partial class FeedbacksTimepiece
{
    public int FeedbackId { get; set; }

    public string? Comment { get; set; }

    public DateTime? FeedbackDate { get; set; }

    public int? UserId { get; set; }

    public int? TimepieceId { get; set; }

    public bool? IsDel { get; set; }

    public virtual Timepiece? Timepiece { get; set; }

    public virtual User? User { get; set; }
}
