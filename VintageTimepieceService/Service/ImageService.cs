using Microsoft.AspNetCore.Http;
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
        private IImageRepository _imageRepository { get; }
        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        public async Task<APIResponse<string>> uploadImage(IFormFile file, string folder)
        {
            var result = await Task.FromResult(_imageRepository.UploadImage(file, folder));
            if (result == string.Empty)
            {
                return new APIResponse<string>
                {
                    Message = "Upload image fail",
                    isSuccess = false,
                    Data = result
                };
            }
            return new APIResponse<string>
            {
                Message = "Upload file success",
                isSuccess = true,
                Data = result
            };
        }

        public async Task<APIResponse<TimepieceImage>> CreateNewTimepieceImage(TimepieceImage timepieceImage)
        {
            var result = await _imageRepository.CreateTimepieceImage(timepieceImage);
            bool isSuccess = false;
            if (result != null)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Create timepiece image success" : "Create timepiece image fail";

            return new APIResponse<TimepieceImage>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }
    }
}
