using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;

namespace VintageTimePieceRepository.IRepository
{
    public interface IEvaluationRepository : IBaseRepository<Evaluation>
    {
        public IEnumerable<Evaluation> GetAllEvaluation();
        public Evaluation CreateEvaluation(Evaluation evaluation);
        public Evaluation UpdateEvaluation(Evaluation evaluation);
        public Evaluation GetOneEvaluation(int id);
    }
}
