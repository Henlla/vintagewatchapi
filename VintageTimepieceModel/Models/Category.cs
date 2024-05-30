using System;
using System.Collections.Generic;

namespace VintageTimepieceModel.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; }

    public virtual ICollection<TimepieceCategory> TimepieceCategories { get; set; } = new List<TimepieceCategory>();
}
