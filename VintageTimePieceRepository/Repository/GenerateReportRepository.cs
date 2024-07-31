using PdfSharpCore;
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using VintageTimepieceModel.Models.Shared;
using VintageTimePieceRepository.IRepository;

namespace VintageTimePieceRepository.Repository
{
    public class GenerateReportRepository : IGenerateReportRepository
    {
        public ReportModel GenerateReport(int invoiceId, string htmlContent)
        {
            var document = new PdfDocument();
            PdfGenerator.AddPdfPages(document, htmlContent, PageSize.A4);
            byte[]? response = null;
            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                response = ms.ToArray();
            }
            string fileName = $"Invoice_{invoiceId}.pdf";
            return new ReportModel
            {
                Response = response,
                FileName = fileName,
                Type = "application/pdf"
            };
        }
    }
}
