using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing;
using System.Text;
using VintageTimepieceModel.Models;
using VintageTimepieceService.IService;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VintageTimepieceApi.Controllers
{
    [Route("evaluation")]
    [ApiController]
    public class EvaluationController : ControllerBase
    {
        private readonly IEvaluationService _evaluationService;
        private readonly ITimepieceEvaluateService _timepieceEvaluateService;
        private readonly IGenerateReportsService _generateReportsService;
        private readonly IJwtConfigService _jwtConfigService;

        public EvaluationController(IEvaluationService evaluationService,
            ITimepieceEvaluateService timepieceEvaluateService,
            IJwtConfigService jwtConfigService, IGenerateReportsService generateReportsService)
        {
            _evaluationService = evaluationService;
            _timepieceEvaluateService = timepieceEvaluateService;
            _jwtConfigService = jwtConfigService;
            _generateReportsService = generateReportsService;
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


        [HttpGet, Route("GenerateReport")]
        public async Task<IActionResult> GenerateRepor([FromQuery] int invoiceId)
        {
            string htmlContent = "<div";
            htmlContent += "  <h2 style='margin: 0; padding: 0'>Account</h2>";
            htmlContent += "  <h2 style='margin: 0; padding: 0'>Receivable Invoice</h2>";
            htmlContent += "  <table style='width: 100%; margin-bottom: 30px'>";
            htmlContent += "    <tr>";
            htmlContent += "      <td style='width: 50%'></td>";
            htmlContent += "      <td style='width: 50%'>";
            htmlContent += "        <div style='display: flex'>";
            htmlContent += "          <span style='padding-right: 30px'>Invoice No.</span>";
            htmlContent += "          <span>______________________</span>";
            htmlContent += "        </div>";
            htmlContent += "        <div style='display: flex; padding-top: 12px'>";
            htmlContent += "          <span style='padding-right: 75px'>Date</span>";
            htmlContent += "          <span>______________________</span>";
            htmlContent += "        </div>";
            htmlContent += "      </td>";
            htmlContent += "    </tr>";
            htmlContent += "  </table>";

            htmlContent += "  <table style='width: 100%'>";
            htmlContent += "    <tr>";
            htmlContent += "      <td style='width: 50%;'>";
            htmlContent += "        <h2 style='margin: 0; padding: 0 0 24px 0'>Vintage Watch</h2>";
            htmlContent += "        <div style='padding-bottom: 18px'>";
            htmlContent += "          <span style='padding-right: 40px'>Name</span>";
            htmlContent += "          <span>Vintage Watch</span>";
            htmlContent += "        </div>";
            htmlContent += "        <div style='padding-bottom: 18px'>";
            htmlContent += "          <span style='padding-right: 39px'>Phone</span>";
            htmlContent += "          <span>+8409082738712</span>";
            htmlContent += "        </div>";
            htmlContent += "        <div style='padding-bottom: 18px'>";
            htmlContent += "          <span style='padding-right: 40px'>Email</span>";
            htmlContent += "          <span>vintagewatch@gmail.com</span>";
            htmlContent += "        </div>";
            htmlContent += "        <div style='padding-bottom: 18px'>";
            htmlContent += "          <span style='padding-right: 25px'>Address</span>";
            htmlContent += "          <span>123 Le Loi Street</span>";
            htmlContent += "        </div>";
            htmlContent += "      </td>";

            htmlContent += "      <td style='width: 50%;'>";
            htmlContent += "        <h2 style='margin: 0; padding: 0 0 30px 0'>Customer</h2>";
            htmlContent += "        <div style='padding-bottom: 18px'>";
            htmlContent += "          <span style='padding-right: 40px'>Name</span>";
            htmlContent += "          <span>_________________________</span>";
            htmlContent += "        </div>";
            htmlContent += "        <div style='padding-bottom: 18px'>";
            htmlContent += "          <span style='padding-right: 39px'>Phone</span>";
            htmlContent += "          <span>_________________________</span>";
            htmlContent += "        </div>";
            htmlContent += "        <div style='padding-bottom: 18px'>";
            htmlContent += "          <span style='padding-right: 40px'>Email</span>";
            htmlContent += "          <span>_________________________</span>";
            htmlContent += "        </div>";
            htmlContent += "        <div style='padding-bottom: 18px'>";
            htmlContent += "          <span style='padding-right: 25px'>Address</span>";
            htmlContent += "          <span>_________________________</span>";
            htmlContent += "        </div>";
            htmlContent += "      </td>";
            htmlContent += "    </tr>";
            htmlContent += "  </table>";

            htmlContent += "  <table border='1' style='width: 100%; margin-top: 36px; border: 1px solid #000; border-collapse: collapse;'>";
            htmlContent += "    <tr>";
            htmlContent += "      <td style='width: 22%;padding: 5px 0 5px 5px;'><span>No</span></td>";
            htmlContent += "      <td style='width: 44%;padding: 5px 0 5px 5px;'><span>Product Name</span></td>";
            htmlContent += "      <td style='width: 33%;padding: 5px 0 5px 5px;'><span>Amount</span></td>";
            htmlContent += "    </tr>";

            htmlContent += "    <tr>";
            htmlContent += "      <td style='width: 22%;padding: 5px 0 5px 5px;'><span>No</span></td>";
            htmlContent += "      <td style='width: 44%;padding: 5px 0 5px 5px;'><span>Product Name</span></td>";
            htmlContent += "      <td style='width: 33%;padding: 5px 0 5px 5px;'><span>Amount</span></td>";
            htmlContent += "    </tr>";

            htmlContent += "  </table>";

            htmlContent += "  <table style='width: 100%; margin-top: 36px'>";
            htmlContent += "    <tr>";
            htmlContent += "      <td style='width: 5%'><span></span></td>";
            htmlContent += "      <td style='width: 5%'><span></span></td>";
            htmlContent += "      <td style='width: 5%'><span>Subtotal</span></td>";
            htmlContent += "      <td style='width: 5%;padding-bottom: 15px;'><span>_______________</span></td>";
            htmlContent += "    </tr>";
            htmlContent += "    <tr>";
            htmlContent += "      <td style='width: 30%'><span></span></td>";
            htmlContent += "      <td style='width: 20%'><span></span></td>";
            htmlContent += "      <td style='width: 40%'><span>Discount</span></td>";
            htmlContent += "      <td style='width: 30%;padding-bottom: 15px;'><span>_______________</span></td>";
            htmlContent += "    </tr>";
            htmlContent += "    <tr>";
            htmlContent += "      <td style='width: 30%'><span></span></td>";
            htmlContent += "      <td style='width: 20%'><span></span></td>";
            htmlContent += "      <td style='width: 40%'><span>Address</span></td>";
            htmlContent += "      <td style='width: 30%;padding-bottom: 15px;'><span>_______________</span></td>";
            htmlContent += "    </tr>";
            htmlContent += "  </table>";
            htmlContent += "</div>";

            var result = await _generateReportsService.GenerateReport(invoiceId, htmlContent);
            return File(result.Data.Response, result.Data.Type, result.Data.FileName);
        }
    }
}
