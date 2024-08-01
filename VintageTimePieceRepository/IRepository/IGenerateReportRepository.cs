using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models.Shared;

namespace VintageTimePieceRepository.IRepository
{
    public interface IGenerateReportRepository
    {
        public ReportModel GenerateReport(int invoiceId, string htmlContent);
        public Task SendMail(byte[] fileContent, string fileName, string recipientEmail);
    }
}
