using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VintageTimepieceModel.Models;

public partial class Brand
{
    public int BrandId { get; set; }

    public string? BrandName { get; set; }

    public string? BrandDescription { get; set; }

    public bool? IsDel { get; set; }
    [JsonIgnore]
    public virtual ICollection<Timepiece> Timepieces { get; set; } = new List<Timepiece>();
}
