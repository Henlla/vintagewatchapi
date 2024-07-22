using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;
using VintageTimePieceRepository.IRepository;
using VintageTimepieceService.IService;

namespace VintageTimepieceService.Service
{
    public class EvaluationService : IEvaluationService
    {
        private readonly IEvaluationRepository _evaluationRepository;
        public EvaluationService(IEvaluationRepository evaluationRepository)
        {
            _evaluationRepository = evaluationRepository;
        }

        public async Task<APIResponse<Evaluation>> CreateEvaluation(Evaluation evaluate)
        {
            var result = await _evaluationRepository.CreateEvaluation(evaluate);
            bool isSuccess = false;
            if (result != null)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Create evaluate success" : "Create evaluate fail";

            return new APIResponse<Evaluation>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }
    }
}
