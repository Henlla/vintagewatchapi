using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;

namespace VintageTimepieceService.IService
{
    public interface IImageService
    {
        public Task<APIResponse<TimepieceImage>> GetFirstImage(int timepieceId);
        public Task<APIResponse<List<TimepieceImage>>> GetAllImage();
        public Task<APIResponse<List<TimepieceImage>>> GetAllImageExceptFirst(int imageId);
    }
}
