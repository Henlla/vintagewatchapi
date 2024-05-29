using Microsoft.AspNetCore.Mvc;
using vintagewatchService.IService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace vintagewatchapi.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetAllProducts();
            if (result.Count > 0)
                return Ok(result);
            return NotFound();
        }
    }
}
