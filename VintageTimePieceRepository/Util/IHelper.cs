using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageTimePieceRepository.Util
{
    public interface IHelper
    {
        public Task<string> UploadImageToFirebase(string imageBase64, string folder);
        public Task<string> UploadReportToFirebase(string imageBase64, string folder);
        public Task<string> ConvertFileToBase64(IFormFile files);
        public Task DeleteImageFromFireBase(string? imageUrl);
    }
}
