using System;
using System.Collections.Generic;

#nullable disable

namespace vintagewatchModel.Models
{
    public partial class Category
    {
        public Category()
        {
            TimepieceCategories = new HashSet<TimepieceCategory>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<TimepieceCategory> TimepieceCategories { get; set; }
    }
}
