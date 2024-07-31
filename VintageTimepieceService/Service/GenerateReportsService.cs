using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models.Shared;
using VintageTimePieceRepository.IRepository;
using VintageTimepieceService.IService;

namespace VintageTimepieceService.Service
{
    public class GenerateReportsService : IGenerateReportsService
    {
        private IGenerateReportRepository _generateReportRepository;
        public GenerateReportsService(IGenerateReportRepository generateReportRepository)
        {
            _generateReportRepository = generateReportRepository;
        }
        public async Task<APIResponse<ReportModel>> GenerateReport(int invoiceId, string htmlContent)
        {
            var result = _generateReportRepository.GenerateReport(invoiceId, htmlContent);
            bool isSuccess = true;
            if (result == null)
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Generate success" : "Generate fail!";
            return await Task.FromResult(new APIResponse<ReportModel>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            });
        }
    }
}
