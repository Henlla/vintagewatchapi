using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageTimePieceRepository.Util
{
    public class Helper : IHelper
    {
        private readonly IConfiguration _configuration;
        public Helper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<string> ConvertFileToBase64(IFormFile files)
        {
            string base64String = "";
            if (files == null || files.Length == 0)
            {
                return Task.FromResult(base64String);
            }
            using (var memoryStream = new MemoryStream())
            {
                files.CopyTo(memoryStream);
                var fileByte = memoryStream.ToArray();
                base64String = Convert.ToBase64String(fileByte);
            }
            return Task.FromResult(base64String);
        }

        public async Task<string> UploadImageToFirebase(string images)
        {
            byte[] imageData = Convert.FromBase64String(images);
            var storage = new FirebaseStorage(_configuration["Firebase:bucket"]);
            var imageUrl = await storage.Child("images")
                .Child($"{Guid.NewGuid()}_{DateTime.Now.Ticks}.jpg")
                .PutAsync(new MemoryStream(imageData));
            return imageUrl;
        }
    }
}
