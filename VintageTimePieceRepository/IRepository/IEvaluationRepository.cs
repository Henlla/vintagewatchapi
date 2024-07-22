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
        public Task<List<Evaluation>> GetAllEvaluation();
        public Task<Evaluation> CreateEvaluation(Evaluation evaluation);
        public Task<Evaluation> UpdateEvaluation(Evaluation evaluation);
        public Task<Evaluation> GetOneEvaluation(int id);
    }
}
