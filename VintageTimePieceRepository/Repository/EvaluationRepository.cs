using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Evaluation>> GetAllEvaluation()
        {
            var result = await FindAll().Where(eva => eva.IsDel == false).ToListAsync();
            return result;
        }

        public async Task<Evaluation> GetOneEvaluation(int id)
        {
            var result = await FindAll().Where(eva => eva.IsDel == false && eva.EvaluationId == id).SingleAsync();
            return result;
        }

        public async Task<Evaluation> CreateEvaluation(Evaluation evaluation)
        {
            return await Add(evaluation);
        }

        public async Task<Evaluation> UpdateEvaluation(Evaluation evaluation)
        {
            return await Update(evaluation);
        }
    }
}
