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
            var result = await Task.FromResult(_timepieceEvaluateRepository.Add(timepieceEvaluation));
            if (result == null)
                return new APIResponse<TimepieceEvaluation>
                {
                    Message = "Create timepiece evaluate fail",
                    isSuccess = false,
                    Data = result
                };
            return new APIResponse<TimepieceEvaluation>
            {
                Message = "Create timepiece evaluate success",
                isSuccess = true,
                Data = result
            };
        }
    }
}
