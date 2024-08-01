using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models.Shared;

namespace VintageTimepieceService.IService
{
    public interface IGenerateReportsService
    {
        public Task<APIResponse<ReportModel>> GenerateReport(int invoiceId, string htmlContent);
        public Task<APIResponse<string>> SendReportToMail(byte[] fileContent, string fileName, string recipientEmail);
    }
}
