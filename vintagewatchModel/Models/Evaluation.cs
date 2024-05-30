using System;
using System.Collections.Generic;

#nullable disable

namespace vintagewatchModel.Models
{
    public partial class Evaluation
    {
        public Evaluation()
        {
            TimepieceEvaluations = new HashSet<TimepieceEvaluation>();
        }

        public int EvaluationId { get; set; }
        public int? EvaluatorId { get; set; }
        public string Comments { get; set; }
        public decimal? ValueExtimated { get; set; }

        public virtual User Evaluator { get; set; }
        public virtual ICollection<TimepieceEvaluation> TimepieceEvaluations { get; set; }
    }
}
