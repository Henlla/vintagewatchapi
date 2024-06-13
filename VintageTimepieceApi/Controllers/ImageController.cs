using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VintageTimePieceRepository.IRepository;
using VintageTimePieceRepository.Repository;
using VintageTimepieceService.IService;

namespace VintageTimepieceApi.Controllers
{
    [Route("image")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly ITimepieceRepository _imageRepository;
        public ImageController(IImageService imageService, ITimepieceRepository imageRepository)
        {
            _imageService = imageService;
            _imageRepository = imageRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await _imageRepository.GetAllTimePieceWithImage();
            var result = await _imageService.GetAllImage();
            if (result.isSuccess)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

        [HttpGet, Route("GetFirstImage")]
        public async Task<IActionResult> GetFirstImage([FromQuery] int productId)
        {
            var result = await _imageService.GetFirstImage(productId);
            if (result.isSuccess)
            {
                return Ok(result);
            }
            return NotFound(result);

        }

        [HttpGet(), Route("GetAllImageExceptFirst")]
        public async Task<IActionResult> GetAllImageExceptFirst(int imageId)
        {
            var result = await _imageService.GetAllImageExceptFirst(imageId);
            if (result.isSuccess)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
