using Firebase.Storage;
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
