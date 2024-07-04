using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimePieceRepository.IRepository;

namespace VintageTimePieceRepository.Repository
{
    public class TimepieceEvaluateRepository : BaseRepository<TimepieceEvaluation>, ITimepieceEvaluateRepository
    {
        public TimepieceEvaluateRepository(VintagedbContext context) : base(context)
        {
        }
    }
}
