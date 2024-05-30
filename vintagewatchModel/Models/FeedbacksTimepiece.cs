using System;
using System.Collections.Generic;

#nullable disable

namespace vintagewatchModel.Models
{
    public partial class FeedbacksTimepiece
    {
        public int FeedbackId { get; set; }
        public int? UserId { get; set; }
        public int? TimepieceId { get; set; }
        public string Comment { get; set; }
        public DateTime? FeedbackDate { get; set; }

        public virtual Timepiece Timepiece { get; set; }
        public virtual User User { get; set; }
    }
}
