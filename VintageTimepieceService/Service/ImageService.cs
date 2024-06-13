using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;
using VintageTimePieceRepository.IRepository;
using VintageTimepieceService.IService;

namespace VintageTimepieceService.Service
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        public async Task<APIResponse<List<TimepieceImage>>> GetAllImage()
        {
            var result = await _imageRepository.GetAllProductImage();
            if (result.Count > 0)
            {
                return new APIResponse<List<TimepieceImage>>
                {
                    Message = "Get all image success",
                    isSuccess = true,
                    Data = result
                };
            }
            return new APIResponse<List<TimepieceImage>>
            {
                Message = "Don't have any image",
                isSuccess = false,
                Data = result
            };
        }

        public async Task<APIResponse<List<TimepieceImage>>> GetAllImageExceptFirst(int imageId)
        {
            var result = await _imageRepository.GetAllImageWithoutFirstImage(imageId);
            if (result.Count > 0)
            {
                return new APIResponse<List<TimepieceImage>>
                {
                    Message = "Get the image success",
                    isSuccess = true,
                    Data = result
                };
            }
            return new APIResponse<List<TimepieceImage>>
            {
                Message = "Don't have any Image",
                isSuccess = false,
                Data = result
            };
        }

        public async Task<APIResponse<TimepieceImage>> GetFirstImage(int timepieceId)
        {
            var result = await _imageRepository.GetFirstImageOfProduct(timepieceId);
            if (result != null)
            {
                return new APIResponse<TimepieceImage>
                {
                    Message = "Get the image success",
                    isSuccess = true,
                    Data = result
                };
            }
            return new APIResponse<TimepieceImage>
            {
                Message = "Don't find any image",
                isSuccess = false,
                Data = result
            };
        }
    }
}
