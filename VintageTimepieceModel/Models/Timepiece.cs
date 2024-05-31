using System;
using System.Collections.Generic;

namespace VintageTimepieceModel.Models;

public partial class Timepiece
{
    public int TimepieceId { get; set; }

    public int? UserId { get; set; }

    public int? ImageId { get; set; }

    public string TimepieceName { get; set; }

    public string Brand { get; set; }

    public string Model { get; set; }

    public string Description { get; set; }

    public DateTime? DatePost { get; set; }

    public decimal? Price { get; set; }

    public bool? IsDel { get; set; }

    public virtual ICollection<FeedbacksTimepiece> FeedbacksTimepieces { get; set; } = new List<FeedbacksTimepiece>();

    public virtual TimepieceImage Image { get; set; }

    public virtual ICollection<OrdersDetail> OrdersDetails { get; set; } = new List<OrdersDetail>();

    public virtual ICollection<RatingsTimepiece> RatingsTimepieces { get; set; } = new List<RatingsTimepiece>();

    public virtual ICollection<TimepieceCategory> TimepieceCategories { get; set; } = new List<TimepieceCategory>();

    public virtual ICollection<TimepieceEvaluation> TimepieceEvaluations { get; set; } = new List<TimepieceEvaluation>();

    public virtual User User { get; set; }
}
