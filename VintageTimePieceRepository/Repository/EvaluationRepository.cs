using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimePieceRepository.IRepository;

namespace VintageTimePieceRepository.Repository
{
    public class EvaluationRepository : BaseRepository<Evaluation>, IEvaluationRepository
    {
        public EvaluationRepository(VintagedbContext context) : base(context)
        {
        }

        public IEnumerable<Evaluation> GetAllEvaluation()
        {
            var result = FindAll().Where(eva => eva.IsDel == false).AsEnumerable();
            return result;
        }

        public Evaluation GetOneEvaluation(int id)
        {
            var result = FindAll().Where(eva => eva.IsDel == false && eva.EvaluationId == id).AsEnumerable().Single();
            return result;
        }

        public Evaluation CreateEvaluation(Evaluation evaluation)
        {
            return Add(evaluation);
        }

        public Evaluation UpdateEvaluation(Evaluation evaluation)
        {
            return Update(evaluation);
        }
    }
}
