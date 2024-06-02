using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;
using VintageTimepieceService.IService;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace VintageTimepieceApi.Controllers
{
    [Route("/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticateService _service;
        public AuthController(IAuthenticateService service)
        {
            _service = service;
        }

        [HttpPost, Route("signin")]
        public async Task<IActionResult> Login([FromBody] LoginModel user)
        {
            var loginUser = await _service.CheckLogin(user);
            if (user == null)
                return BadRequest(new APIResponse<string>
                {
                    Message = "Invalid client request",
                    isSuccess = false
                });
            if (loginUser == null)
            {
                return BadRequest(new APIResponse<string>
                {
                    Message = "Invalid username/password",
                    isSuccess = false
                });
            }
            var token = await _service.GetAccessToken(loginUser);
            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = await _service.GetRefreshToken();
            var refreshTokenData = new RefreshToken
            {
                JwtId = token.Id,
                Token = refreshToken,
                UserId = loginUser.UserId,
                IsUsed = false,
                IsRevoke = false,
                IssueAt = DateTime.Now,
                ExpiredAt = DateTime.Now.AddDays(30),
            };

            await _service.SaveRefreshToken(refreshTokenData);

            return Ok(new APIResponse<TokenModel>
            {
                Message = "Authorize success",
                isSuccess = true,
                Data = new TokenModel
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                }
            });
        }



        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenModel model)
        {
            var result = await _service.ReNewtoken(model);
            if (result.isSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("signup")]
        public async Task<IActionResult> RegisterAccount([FromBody] RegisterModel registerModel)
        {
            var result = await _service.RegisterAccount(registerModel);
            if (!result.isSuccess)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("signinWithGoogle")]
        [Authorize]
        public async Task<IActionResult> SignInWithGoogle()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded)
            {
                return BadRequest(new APIResponse<string>
                {
                    Message = "Sign in with google fail",
                    isSuccess = false
                });
            }

            var token = result.Principal.FindFirst(ClaimTypes.Email).Value;
            return Ok(token);
        }
    }
}
