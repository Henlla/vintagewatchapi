using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VintageTimepieceModel.Models;

public partial class Evaluation
{
    public int EvaluationId { get; set; }

    public string? MovementStatus { get; set; }

    public string? CaseDiameterStatus { get; set; }

    public string? CaseMaterialStatus { get; set; }

    public string? WaterResistanceStatus { get; set; }

    public string? CrystalTypeStatus { get; set; }

    public string? DialStatus { get; set; }

    public string? HandsStatus { get; set; }

    public string? BraceletStatus { get; set; }

    public string? BuckleStatus { get; set; }

    public string? AccuracyStatus { get; set; }

    public decimal? ValueExtimatedStatus { get; set; }

    public int? EvaluatorId { get; set; }

    public bool? IsDel { get; set; }

    public virtual User? Evaluator { get; set; }
    [JsonIgnore]
    public virtual ICollection<TimepieceEvaluation> TimepieceEvaluations { get; set; } = new List<TimepieceEvaluation>();
}
