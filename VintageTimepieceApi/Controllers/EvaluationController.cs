using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VintageTimepieceModel.Models;
using VintageTimepieceService.IService;

namespace VintageTimepieceApi.Controllers
{
    [Route("evaluation")]
    [ApiController]
    public class EvaluationController : ControllerBase
    {
        private readonly IEvaluationService _evaluationService;
        private readonly ITimepieceEvaluateService _timepieceEvaluateService;
        private readonly IJwtConfigService _jwtConfigService;

        public EvaluationController(IEvaluationService evaluationService,
            ITimepieceEvaluateService timepieceEvaluateService,
            IJwtConfigService jwtConfigService)
        {
            _evaluationService = evaluationService;
            _timepieceEvaluateService = timepieceEvaluateService;
            _jwtConfigService = jwtConfigService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "APPRAISER")]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] string evaluate, [FromForm] string timepieceId)
        {
            HttpContext.Request.Cookies.TryGetValue("access_token", out var access_token);
            if (access_token == null)
            {
                return Unauthorized();
            }
            var user = _jwtConfigService.GetUserFromAccessToken(access_token);
            var evaluateData = JsonConvert.DeserializeObject<Evaluation>(evaluate);
            evaluateData.EvaluatorId = user.Data.UserId;
            var resultEvaluate = await _evaluationService.CreateEvaluation(evaluateData);
            if (resultEvaluate == null)
            {
                return BadRequest(resultEvaluate);
            }
            var timepieceEvaluate = new TimepieceEvaluation();
            timepieceEvaluate.EvaluationId = resultEvaluate.Data.EvaluationId;
            timepieceEvaluate.EvaluationDate = DateTime.Now;
            timepieceEvaluate.TimepieceId = int.Parse(timepieceId);
            var resultTimepieceEvaluate = await _timepieceEvaluateService.CreateTimepieceEvaluate(timepieceEvaluate);
            if (resultTimepieceEvaluate == null)
            {
                return BadRequest(resultTimepieceEvaluate);
            }
            return Ok(resultEvaluate);
        }
    }
}
