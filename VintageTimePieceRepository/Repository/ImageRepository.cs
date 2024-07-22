using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimePieceRepository.IRepository;
using VintageTimePieceRepository.Util;

namespace VintageTimePieceRepository.Repository
{
    public class ImageRepository : BaseRepository<TimepieceImage>, IImageRepository
    {
        private IHelper _helper { get; }
        public ImageRepository(VintagedbContext context, IHelper helper) : base(context)
        {
            _helper = helper;
        }

        public string UploadImage(IFormFile file, string folder)
        {
            string base64String = _helper.ConvertFileToBase64(file).Result;
            if (base64String == string.Empty)
            {
                return base64String;
            }
            var url = _helper.UploadImageToFirebase(base64String, folder).Result;
            return url;
        }

        public async Task<TimepieceImage> CreateTimepieceImage(TimepieceImage timepieceImage)
        {
            return await Add(timepieceImage);
        }
    }
}
