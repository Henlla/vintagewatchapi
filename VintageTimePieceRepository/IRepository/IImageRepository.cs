using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageTimePieceRepository.IRepository
{
    public interface IImageRepository
    {
        public string UploadImage(IFormFile file, string folder);
    }
}
