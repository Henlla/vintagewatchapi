using System;
using System.Collections.Generic;

#nullable disable

namespace vintagewatchModel.Models
{
    public partial class TimepieceImage
    {
        public TimepieceImage()
        {
            Timepieces = new HashSet<Timepiece>();
        }

        public int TimepieceImageId { get; set; }
        public string ImageUrl { get; set; }

        public virtual ICollection<Timepiece> Timepieces { get; set; }
    }
}
