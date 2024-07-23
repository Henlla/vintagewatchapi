using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Security.Cryptography;
using System.Text;


namespace VintageTimePieceRepository.Util
{
    public class Helper : IHelper
    {
        private readonly IConfiguration _configuration;
        public Helper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // FIREBASE
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
        public async Task DeleteImageFromFireBase(string imageUrl)
        {
            Uri uri = new Uri(imageUrl);
            string imagePath = uri.LocalPath.Substring(uri.LocalPath.IndexOf("/o/") + 3);
            if (imagePath == "avatar/userdefault.png")
            {
                return;
            }
            bool isImageExists = await ImageExistsInFirebase(imageUrl);
            if (!isImageExists)
            {
                return;
            }
            var storage = new FirebaseStorage(_configuration["Firebase:bucket"]);
            await storage.Child(imagePath).DeleteAsync();
        }
        public async Task<bool> ImageExistsInFirebase(string imageUrl)
        {
            try
            {
                Uri uri = new Uri(imageUrl);
                string path = uri.LocalPath.Substring(uri.LocalPath.IndexOf("/o/") + 3);
                var storage = new FirebaseStorage(_configuration["Firebase:bucket"]);
                await storage.Child(path).GetDownloadUrlAsync();
                return true;
            }
            catch (FirebaseStorageException ex)
            {
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> UploadImageToFirebase(string images, string folder)
        {
            byte[] imageData = Convert.FromBase64String(images);
            var storage = new FirebaseStorage(_configuration["Firebase:bucket"]);
            var imageUrl = await storage.Child("images/" + folder)
                .Child($"{Guid.NewGuid()}_{DateTime.Now.Ticks}.jpg")
                .PutAsync(new MemoryStream(imageData));
            return imageUrl;
        }
        public async Task<string> UploadReportToFirebase(string file, string folder)
        {
            byte[] imageData = Convert.FromBase64String(file);
            var storage = new FirebaseStorage(_configuration["Firebase:bucket"]);
            var fileUrl = await storage.Child(folder)
                .Child($"{Guid.NewGuid()}_{DateTime.Now.Ticks}.xlsx")
                .PutAsync(new MemoryStream(imageData));
            return fileUrl;
        }

        //VNPAY
        public string CreatePaymentRequestUrl(string baseUrl, string vnp_HashSecret, string queryString)
        {
            baseUrl += "?" + queryString;
            String signData = queryString;
            string vnp_SecureHash = HmacSHA512(vnp_HashSecret, signData);
            baseUrl += "&vnp_SecureHash=" + vnp_SecureHash;
            return baseUrl;
        }

        public String HmacSHA512(string key, String inputData)
        {
            var hash = new StringBuilder();
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            return hash.ToString();
        }
    }
}
