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
        private readonly ITimepiecesService _timepieceService;

        public EvaluationController(IEvaluationService evaluationService,
            ITimepieceEvaluateService timepieceEvaluateService,
            IJwtConfigService jwtConfigService, IGenerateReportsService generateReportsService,
            ITimepiecesService timepieceService)
        {
            _evaluationService = evaluationService;
            _timepieceEvaluateService = timepieceEvaluateService;
            _jwtConfigService = jwtConfigService;
            _generateReportsService = generateReportsService;
            _timepieceService = timepieceService;
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


        [HttpGet, Route("SendReportToMail")]
        public async Task<IActionResult> SendReportToMail([FromQuery] int timepieceId)
        {
            //string htmlContent = "<div>";
            //htmlContent += "  <h2 style='margin: 0; padding: 0'>Account</h2>";
            //htmlContent += "  <h2 style='margin: 0; padding: 0'>Receivable Invoice</h2>";
            //htmlContent += "  <table style='width: 100%; margin-bottom: 30px'>";
            //htmlContent += "    <tr>";
            //htmlContent += "      <td style='width: 50%'></td>";
            //htmlContent += "      <td style='width: 50%'>";
            //htmlContent += "        <div style='display: flex'>";
            //htmlContent += "          <span style='padding-right: 30px'>Invoice No.</span>";
            //htmlContent += "          <span>______________________</span>";
            //htmlContent += "        </div>";
            //htmlContent += "        <div style='display: flex; padding-top: 12px'>";
            //htmlContent += "          <span style='padding-right: 75px'>Date</span>";
            //htmlContent += "          <span>______________________</span>";
            //htmlContent += "        </div>";
            //htmlContent += "      </td>";
            //htmlContent += "    </tr>";
            //htmlContent += "  </table>";

            //htmlContent += "  <table style='width: 100%'>";
            //htmlContent += "    <tr>";
            //htmlContent += "      <td style='width: 50%;'>";
            //htmlContent += "        <h2 style='margin: 0; padding: 0 0 24px 0'>Vintage Watch</h2>";
            //htmlContent += "        <div style='padding-bottom: 18px'>";
            //htmlContent += "          <span style='padding-right: 40px'>Name</span>";
            //htmlContent += "          <span>Vintage Watch</span>";
            //htmlContent += "        </div>";
            //htmlContent += "        <div style='padding-bottom: 18px'>";
            //htmlContent += "          <span style='padding-right: 39px'>Phone</span>";
            //htmlContent += "          <span>+8409082738712</span>";
            //htmlContent += "        </div>";
            //htmlContent += "        <div style='padding-bottom: 18px'>";
            //htmlContent += "          <span style='padding-right: 40px'>Email</span>";
            //htmlContent += "          <span>vintagewatch@gmail.com</span>";
            //htmlContent += "        </div>";
            //htmlContent += "        <div style='padding-bottom: 18px'>";
            //htmlContent += "          <span style='padding-right: 25px'>Address</span>";
            //htmlContent += "          <span>123 Le Loi Street</span>";
            //htmlContent += "        </div>";
            //htmlContent += "      </td>";

            //htmlContent += "      <td style='width: 50%;'>";
            //htmlContent += "        <h2 style='margin: 0; padding: 0 0 30px 0'>Customer</h2>";
            //htmlContent += "        <div style='padding-bottom: 18px'>";
            //htmlContent += "          <span style='padding-right: 40px'>Name</span>";
            //htmlContent += "          <span>_________________________</span>";
            //htmlContent += "        </div>";
            //htmlContent += "        <div style='padding-bottom: 18px'>";
            //htmlContent += "          <span style='padding-right: 39px'>Phone</span>";
            //htmlContent += "          <span>_________________________</span>";
            //htmlContent += "        </div>";
            //htmlContent += "        <div style='padding-bottom: 18px'>";
            //htmlContent += "          <span style='padding-right: 40px'>Email</span>";
            //htmlContent += "          <span>_________________________</span>";
            //htmlContent += "        </div>";
            //htmlContent += "        <div style='padding-bottom: 18px'>";
            //htmlContent += "          <span style='padding-right: 25px'>Address</span>";
            //htmlContent += "          <span>_________________________</span>";
            //htmlContent += "        </div>";
            //htmlContent += "      </td>";
            //htmlContent += "    </tr>";
            //htmlContent += "  </table>";

            //htmlContent += "  <table border='1' style='width: 100%; margin-top: 36px; border: 1px solid #000; border-collapse: collapse;'>";
            //htmlContent += "    <tr>";
            //htmlContent += "      <td style='width: 22%;padding: 5px 0 5px 5px;'><span>No</span></td>";
            //htmlContent += "      <td style='width: 44%;padding: 5px 0 5px 5px;'><span>Product Name</span></td>";
            //htmlContent += "      <td style='width: 33%;padding: 5px 0 5px 5px;'><span>Amount</span></td>";
            //htmlContent += "    </tr>";

            //htmlContent += "    <tr>";
            //htmlContent += "      <td style='width: 22%;padding: 5px 0 5px 5px;'><span>No</span></td>";
            //htmlContent += "      <td style='width: 44%;padding: 5px 0 5px 5px;'><span>Product Name</span></td>";
            //htmlContent += "      <td style='width: 33%;padding: 5px 0 5px 5px;'><span>Amount</span></td>";
            //htmlContent += "    </tr>";

            //htmlContent += "  </table>";

            //htmlContent += "  <table style='width: 100%; margin-top: 36px'>";
            //htmlContent += "    <tr>";
            //htmlContent += "      <td style='width: 5%'><span></span></td>";
            //htmlContent += "      <td style='width: 5%'><span></span></td>";
            //htmlContent += "      <td style='width: 5%'><span>Subtotal</span></td>";
            //htmlContent += "      <td style='width: 5%;padding-bottom: 15px;'><span>_______________</span></td>";
            //htmlContent += "    </tr>";
            //htmlContent += "    <tr>";
            //htmlContent += "      <td style='width: 30%'><span></span></td>";
            //htmlContent += "      <td style='width: 20%'><span></span></td>";
            //htmlContent += "      <td style='width: 40%'><span>Discount</span></td>";
            //htmlContent += "      <td style='width: 30%;padding-bottom: 15px;'><span>_______________</span></td>";
            //htmlContent += "    </tr>";
            //htmlContent += "    <tr>";
            //htmlContent += "      <td style='width: 30%'><span></span></td>";
            //htmlContent += "      <td style='width: 20%'><span></span></td>";
            //htmlContent += "      <td style='width: 40%'><span>Address</span></td>";
            //htmlContent += "      <td style='width: 30%;padding-bottom: 15px;'><span>_______________</span></td>";
            //htmlContent += "    </tr>";
            //htmlContent += "  </table>";
            //htmlContent += "</div>";
            var timepieceData = await _timepieceService.GetTimepieceById(timepieceId);
            string htmlContent = "<div style='width: 100%'>";
            htmlContent += "  <div>";
            htmlContent += "    <p>";
            htmlContent += "      Website Name: <span style='font-weight: 600'>Vintage Watch</span>";
            htmlContent += "    </p>";
            htmlContent += "    <p>";
            htmlContent += "      Sender: <span style='font-weight: bolder'>phamkiet2234@gmail.com</span>";
            htmlContent += "    </p>";
            htmlContent += "  </div>";
            htmlContent += "  <h1 style='margin: 0; padding: 0; text-align: center'>Accept Request</h1>";
            htmlContent += "  <table style='width: 100%; margin: 20px'>";
            htmlContent += "    <tr>";
            htmlContent += $"      <td style='width: 48%; padding-bottom: 10px'>Watch Name: <span style='font-weight: bolder'>{timepieceData.Data.TimepieceName}</span></td>";
            htmlContent += $"      <td style='width: 48%; padding-bottom: 10px'>Movement: <span style='font-weight: bolder'>{timepieceData.Data.Movement}</span></td>";
            htmlContent += "    </tr>";
            htmlContent += "    <tr>";
            htmlContent += $"      <td style='width: 48%; padding-bottom: 10px'>Case Material: <span style='font-weight: bolder'>{timepieceData.Data.CaseMaterial}</span></td>";
            htmlContent += $"      <td style='width: 48%; padding-bottom: 10px'>Case Diameter: <span style='font-weight: bolder'>{timepieceData.Data.CaseDiameter}</span></td>";
            htmlContent += "    </tr>";
            htmlContent += "    <tr>";
            htmlContent += $"      <td style='width: 48%; padding-bottom: 10px'>Case Thickness: <span style='font-weight: bolder'>{timepieceData.Data.CaseThickness}</span></td>";
            htmlContent += $"      <td style='width: 48%; padding-bottom: 10px'>Crystal: <span style='font-weight: bolder'>{timepieceData.Data.Crystal}</span></td>";
            htmlContent += "    </tr>";
            htmlContent += "    <tr>";
            htmlContent += $"      <td style='width: 48%; padding-bottom: 10px'>Water Resistance: <span style='font-weight: bolder'>{timepieceData.Data.WaterResistance}</span></td>";
            htmlContent += $"      <td style='width: 48%; padding-bottom: 10px'>Strap Material: <span style='font-weight: bolder'>{timepieceData.Data.StrapMaterial}</span></td>";
            htmlContent += "    </tr>";
            htmlContent += "    <tr>";
            htmlContent += $"      <td style='width: 48%; padding-bottom: 10px'>Strap Width: <span style='font-weight: bolder'>{timepieceData.Data.StrapWidth}</span></td>";
            htmlContent += $"      <td style='width: 48%; padding-bottom: 10px'>Style: <span style='font-weight: bolder'>{timepieceData.Data.Style}</span></td>";
            htmlContent += "    </tr>";
            htmlContent += "    <tr>";
            htmlContent += $"      <td style='width: 48%; padding-bottom: 10px'>Description: <span style='font-weight: bolder'>{timepieceData.Data.Description}</span></td>";
            htmlContent += $"      <td style='width: 48%; padding-bottom: 10px'>Date Request: <span style='font-weight: bolder'>{timepieceData.Data.DatePost.Value.ToString("dd/MM/yyyy")}</span></td>";
            htmlContent += "    </tr>";
            htmlContent += "    <tr>";
            htmlContent += "      <td colspan='2' style='width: 48%; padding-top: 40px'>Comment: <span style='font-weight: bolder'>We have received your watch. Please wait for us to appraise your watch.</span></td>";
            htmlContent += "    </tr>";
            htmlContent += "    <tr>";
            htmlContent += "      <td style='width: 48%; padding-top: 100px'>Customer Sign</td>";
            htmlContent += "      <td style='width: 48%; padding-top: 100px'>Appraiser Sign</td>";
            htmlContent += "    </tr>";
            htmlContent += "  </table>";
            htmlContent += "</div>";

            var result = await _generateReportsService.GenerateReport(timepieceId, htmlContent);
            var emailResult = await _generateReportsService.SendReportToMail(result.Data.Response, result.Data.FileName, "henllaramen@gmail.com");
            await _timepieceService.UpdateTimepieceReceive(timepieceId, true);
            return Ok(emailResult);
        }
    }
}
