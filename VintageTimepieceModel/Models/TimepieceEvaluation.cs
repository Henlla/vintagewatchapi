using System;
using System.Collections.Generic;

namespace VintageTimepieceModel.Models;

public partial class TimepieceEvaluation
{
    public int TimepieceEvaluationId { get; set; }

    public int? TimepieceId { get; set; }

    public int? EvaluationId { get; set; }

    public DateTime? EvaluationDate { get; set; }

    public bool? IsDel { get; set; }

    public virtual Evaluation? Evaluation { get; set; }

    public virtual Timepiece? Timepiece { get; set; }
}
