﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;

namespace VintageTimepieceService.IService
{
    public interface IEvaluationService
    {
        public Task<APIResponse<Evaluation>> CreateEvaluation(Evaluation evaluate);    }
}
