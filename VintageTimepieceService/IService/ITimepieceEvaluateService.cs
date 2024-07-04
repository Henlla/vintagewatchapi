﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;

namespace VintageTimepieceService.IService
{
    public interface ITimepieceEvaluateService
    {
        public Task<APIResponse<TimepieceEvaluation>> CreateTimepieceEvaluate(TimepieceEvaluation timepieceEvaluation);
    }
}
