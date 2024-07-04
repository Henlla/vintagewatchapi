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
            var result = await Task.FromResult(_evaluationRepository.CreateEvaluation(evaluate));
            if (result != null)
                return new APIResponse<Evaluation>
                {
                    Message = "Create evaluate success",
                    isSuccess = true,
                    Data = result
                };
            return new APIResponse<Evaluation>
            {
                Message = "Create evaluate fail",
                isSuccess = false,
                Data = result
            };
        }
    }
}
