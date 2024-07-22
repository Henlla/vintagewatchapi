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
    public class TimepieceEvaluateService : ITimepieceEvaluateService
    {
        private readonly ITimepieceEvaluateRepository _timepieceEvaluateRepository;
        public TimepieceEvaluateService(ITimepieceEvaluateRepository timepieceEvaluateRepository)
        {
            _timepieceEvaluateRepository = timepieceEvaluateRepository;
        }
        public async Task<APIResponse<TimepieceEvaluation>> CreateTimepieceEvaluate(TimepieceEvaluation timepieceEvaluation)
        {
            var result = await _timepieceEvaluateRepository.Add(timepieceEvaluation);
            bool isSuccess = false;
            if (result != null)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Create timepiece evaluate success" : "Create timepiece evaluate fail";

            return new APIResponse<TimepieceEvaluation>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }
    }
}
