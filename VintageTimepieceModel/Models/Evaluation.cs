using System;
using System.Collections.Generic;

namespace VintageTimepieceModel.Models;

public partial class Evaluation
{
    public int EvaluationId { get; set; }

    public int? EvaluatorId { get; set; }

    public string? Comments { get; set; }

    public decimal? ValueExtimated { get; set; }

    public bool? IsDel { get; set; }

    public virtual User? Evaluator { get; set; }

    public virtual ICollection<TimepieceEvaluation> TimepieceEvaluations { get; set; } = new List<TimepieceEvaluation>();
}
