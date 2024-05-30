using System;
using System.Collections.Generic;

#nullable disable

namespace vintagewatchModel.Models
{
    public partial class TimepieceEvaluation
    {
        public int TimepieceEvaluationId { get; set; }
        public int? TimepieceId { get; set; }
        public int? EvaluationId { get; set; }
        public string Condition { get; set; }
        public DateTime? EvaluationDate { get; set; }

        public virtual Evaluation Evaluation { get; set; }
        public virtual Timepiece Timepiece { get; set; }
    }
}
