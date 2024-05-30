using System;
using System.Collections.Generic;

#nullable disable

namespace vintagewatchModel.Models
{
    public partial class Timepiece
    {
        public Timepiece()
        {
            FeedbacksTimepieces = new HashSet<FeedbacksTimepiece>();
            OrdersDetails = new HashSet<OrdersDetail>();
            RatingsTimepieces = new HashSet<RatingsTimepiece>();
            TimepieceCategories = new HashSet<TimepieceCategory>();
            TimepieceEvaluations = new HashSet<TimepieceEvaluation>();
        }

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

        public virtual TimepieceImage Image { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<FeedbacksTimepiece> FeedbacksTimepieces { get; set; }
        public virtual ICollection<OrdersDetail> OrdersDetails { get; set; }
        public virtual ICollection<RatingsTimepiece> RatingsTimepieces { get; set; }
        public virtual ICollection<TimepieceCategory> TimepieceCategories { get; set; }
        public virtual ICollection<TimepieceEvaluation> TimepieceEvaluations { get; set; }
    }
}
