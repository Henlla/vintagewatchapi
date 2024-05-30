using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.ResponseModel;
using VintageTimePieceRepository.IRepository;
using VintageTimepieceService.IService;

namespace VintageTimepieceApi.Controllers
{
    [Route("/")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsersService _userService;
        private readonly ITokenRepository _tokenRepository;
        public LoginController(IUsersService userService, ITokenRepository tokenRepository)
        {
            _userService = userService;
            _tokenRepository = tokenRepository;
        }
        [HttpPost("login")]
        public async Task<IActionResult> PostLogin(string username, string password)
        {
            if (!username.IsNullOrEmpty())
            {
                if (!password.IsNullOrEmpty())
                {
                    var user = await _userService.CheckLogin(username, password);
                    if (user == null)
                        return BadRequest(new APIResponse
                        {
                            Message = "Invalid Username/Password",
                            isSuccess = false,
                        });
                    return Ok(new APIResponse
                    {
                        Message = "Authenticate Success",
                        isSuccess = true,
                        Token = _tokenRepository.GenerateJwtToken(user),
                    });
                }
                else
                {
                    return BadRequest("Password is empty");
                }
            }
            else
            {
                return BadRequest("Username is empty");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            return Ok("Test");
        }
    }
}
