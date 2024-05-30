using System;
using System.Collections.Generic;

#nullable disable

namespace vintagewatchModel.Models
{
    public partial class TimepieceCategory
    {
        public int TimepieceCategoryId { get; set; }
        public int? TimepieceId { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Timepiece Timepiece { get; set; }
    }
}
