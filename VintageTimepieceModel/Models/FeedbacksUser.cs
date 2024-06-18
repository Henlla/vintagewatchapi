using System;
using System.Collections.Generic;

namespace VintageTimepieceModel.Models;

public partial class FeedbacksUser
{
    public int FeedbackUsersId { get; set; }

    public string? Comment { get; set; }

    public int? RatingStar { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? UserId { get; set; }

    public int? FeedbackTargetId { get; set; }

    public bool? IsDel { get; set; }

    public virtual User? FeedbackTarget { get; set; }

    public virtual User? User { get; set; }
}
