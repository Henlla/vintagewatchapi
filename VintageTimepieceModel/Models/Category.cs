using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VintageTimepieceModel.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public bool? IsDel { get; set; } = false;
    [JsonIgnore]
    public virtual ICollection<TimepieceCategory> TimepieceCategories { get; set; } = new List<TimepieceCategory>();
}
