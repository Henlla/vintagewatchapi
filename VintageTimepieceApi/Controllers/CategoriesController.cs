using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VintageTimepieceModel.Models;
using VintageTimepieceService.IService;

namespace VintageTimepieceApi.Controllers
{
    [Route("categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _categoryService.GetAllCategory();
            if (!result.isSuccess)
                return NotFound(result);
            return Ok(result);
        }
        [HttpGet("id")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _categoryService.GetCategoryById(id);
            if (!result.isSuccess)
                return NotFound(result);
            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMIN,APPRAISER")]
        [HttpPost]

        public async Task<IActionResult> Post(string token, [FromBody] Category category)
        {
            var result = await _categoryService.CreateNewCategory(category);
            if (!result.isSuccess)
                return BadRequest(result);
            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMIN,APPRAISER")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Category cate)
        {
            var result = await _categoryService.UpdateCategory(id, cate);
            if (!result.isSuccess)
                return BadRequest(result);
            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMIN,APPRAISER")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.DeleteCategory(id);
            if (!result.isSuccess)
                return BadRequest(result);
            return Ok(result);
        }


    }
}
