using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VintageTimepieceModel.Models;
using VintageTimepieceService.IService;

namespace VintageTimepieceApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMIN")]
    [Route("roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _roleService.GetAllRole();
            if (!result.isSuccess)
                return NotFound(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Role role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _roleService.CreateNewRole(role);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _roleService.DeleteRole(id);
            if (!result.isSuccess)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
