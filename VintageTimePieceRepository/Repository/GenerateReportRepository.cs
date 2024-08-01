using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
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
        private readonly IConfiguration _configuration;
        public GenerateReportRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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

        public async Task SendMail(byte[] fileContent, string fileName, string recipientEmail)
        {
            var from = _configuration["EMailKit:SMTP_FROM"];
            var name = _configuration["EMailKit:SMTP_NAME"];
            var server = _configuration["EMailKit:SMTP_SERVER"];
            var port = int.Parse(_configuration["EMailKit:SMTP_PORT"].ToString());
            var authEmail = _configuration["EMailKit:SMTP_EMAIL"];
            var authPassword = _configuration["EMailKit:SMTP_PASSWORD"];

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(name, from));
            message.To.Add(new MailboxAddress("Recipient Name", "henllaramen@gmail.com"));
            message.Subject = "We have receive your watch - VINTAGE WATCH";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = "Please find the attached report.";

            // Attach the generated report
            bodyBuilder.Attachments.Add(fileName, fileContent);
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(server, port, false);
                await client.AuthenticateAsync(authEmail, authPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
