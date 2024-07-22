using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VintageTimepieceModel.Models;

public partial class Timepiece
{
    public int TimepieceId { get; set; }

    public string? TimepieceName { get; set; }

    public string? Movement { get; set; }

    public string? CaseMaterial { get; set; }

    public string? CaseDiameter { get; set; }

    public string? CaseThickness { get; set; }

    public string? Crystal { get; set; }

    public string? WaterResistance { get; set; }

    public string? StrapMaterial { get; set; }

    public string? StrapWidth { get; set; }

    public string? Style { get; set; }

    public string? Description { get; set; }

    public DateTime? DatePost { get; set; }

    public decimal? Price { get; set; }

    public int? UserId { get; set; }

    public int? BrandId { get; set; }

    public bool? IsDel { get; set; }

    public bool? IsBuy { get; set; }

    public virtual Brand? Brand { get; set; }
    [JsonIgnore]
    public virtual ICollection<OrdersDetail> OrdersDetails { get; set; } = new List<OrdersDetail>();
    [JsonIgnore]
    public virtual ICollection<RatingsTimepiece> RatingsTimepieces { get; set; } = new List<RatingsTimepiece>();
    [JsonIgnore]
    public virtual ICollection<TimepieceCategory> TimepieceCategories { get; set; } = new List<TimepieceCategory>();
    [JsonIgnore]
    public virtual ICollection<TimepieceEvaluation> TimepieceEvaluations { get; set; } = new List<TimepieceEvaluation>();
    [JsonIgnore]
    public virtual ICollection<TimepieceImage> TimepieceImages { get; set; } = new List<TimepieceImage>();

    public virtual User? User { get; set; }
}
