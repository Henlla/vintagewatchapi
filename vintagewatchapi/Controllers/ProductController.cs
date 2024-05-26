using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vintagewatchModel;
using vintagewatchService.IService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace vintagewatchapi.Controllers
{
    [ApiController]
    [Route("/")]
    public class ProductController : ControllerBase
    {
        private IProductService _service;

        public ProductController(IProductService service)
        {
            this._service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetAllProducts();
            if (result.Count > 0)
                return Ok(result);
            return NotFound();
        }

        [HttpGet("{content}")]
        public async Task<IActionResult> GetString(string content)
        {
            var result = content;
            return Ok(result);
        }
    }
}
