using System;
using System.Collections.Generic;

#nullable disable

namespace vintagewatchModel.Models
{
    public partial class RatingsTimepiece
    {
        public int RatingId { get; set; }
        public int? UserId { get; set; }
        public int? TimepieceId { get; set; }
        public int? RatingStar { get; set; }
        public DateTime? RatingDate { get; set; }

        public virtual Timepiece Timepiece { get; set; }
        public virtual User User { get; set; }
    }
}
