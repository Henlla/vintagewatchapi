using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;
using VintageTimepieceService.IService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using System.Security.Claims;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Identity;


namespace VintageTimepieceApi.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticateService _authService;
        private readonly IRoleService _roleService;
        private readonly IJwtConfigService _jwtConfigService;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthenticateService authService,
            IRoleService roleService,
            IJwtConfigService jwtConfigService,
            IConfiguration configuration)
        {
            _authService = authService;
            _roleService = roleService;
            _jwtConfigService = jwtConfigService;
            _configuration = configuration;
        }


        [HttpPost, Route("signin")]
        public async Task<IActionResult> Login([FromBody] LoginModel user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.CheckLogin(user);
            if (result.isSuccess == false)
            {
                return BadRequest(result);
            }
            var token = _jwtConfigService.GetAccessTokenFromUser(result.Data);
            var accessToken = new JwtSecurityTokenHandler().WriteToken(token.Data);

            return Ok(new APIResponse<User>
            {
                Message = "Login success",
                isSuccess = true,
                AccessToken = accessToken,
                Data = result.Data,
            });
        }



        [HttpPost, Route("signup")]
        public async Task<IActionResult> RegisterAccount([FromBody] RegisterModel registerModel)
        {
            var defaultRole = "USERS";
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var role = await _roleService.GetRoleByName(defaultRole);
            if (!role.isSuccess)
            {
                var newRole = new Role();
                newRole.RoleName = defaultRole;
                await _roleService.CreateNewRole(newRole);
            }
            var result = await _authService.RegisterAccount(registerModel);
            if (!result.isSuccess)
                return BadRequest(result);
            return Ok(result);
        }


        [HttpGet, Route("getUserFromToken")]
        public IActionResult GetUserFromToken([FromQuery] string token)
        {
            var result = _jwtConfigService.GetUserFromAccessToken(token);
            if (result.isSuccess)
                return Ok(result);
            return BadRequest(result);
        }


        [HttpPost, Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new APIResponse<string>
            {
                Message = "Log out success",
                isSuccess = true,
            });
        }


        [EnableCors("Cors")]
        [AllowAnonymous]
        [HttpGet, Route("signinWithGoogle")]

        public IActionResult SignInWithGoogle()
        {
            var redirectUrl = Url.Action(nameof(CallBack), "auth", null, Request.Scheme);
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            var data = Challenge(properties, GoogleDefaults.AuthenticationScheme);
            return data;
        }


        [EnableCors("Cors")]
        [AllowAnonymous]
        [HttpGet, Route("signin-google")]
        public async Task<IActionResult> CallBack()
        {
            APIResponse<User> responseResult = null;
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded)
            {
                return Ok(new APIResponse<string>
                {
                    Message = "Sign in with google fail",
                    isSuccess = false
                });
            }

            var email = result.Principal.FindFirst(ClaimTypes.Email).Value;
            var surname = result.Principal.FindFirst(ClaimTypes.Surname).Value;
            var givenName = result.Principal.FindFirst(ClaimTypes.GivenName).Value;

            var existsUser = await _authService.GetUsersByEmail(email);
            if (existsUser.Data != null)
            {
                var token = _jwtConfigService.GetAccessTokenFromUser(existsUser.Data);
                responseResult = new APIResponse<User>
                {
                    Message = "Login success",
                    isSuccess = true,
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token.Data),
                    Data = existsUser.Data
                };
                //return Redirect("http://localhost:5173?access_token=" + new JwtSecurityTokenHandler().WriteToken(token.Data));
                return Ok(responseResult);
            }

            var user = new RegisterModel();
            user.Email = email;
            user.FirstName = givenName;
            user.LastName = surname;
            user.JoinedDate = DateTime.Now;
            var registerResult = await _authService.RegisterAccount(user);
            if (!registerResult.isSuccess)
            {
                return Ok(new APIResponse<string>
                {
                    Message = "Login fail",
                    isSuccess = false,
                });
            }


            var newUser = await _authService.GetUsersByEmail(user.Email);
            responseResult = new APIResponse<User>
            {
                Message = "Login success",
                isSuccess = true,
                Data = newUser.Data
            };
            //return Redirect("http://localhost:5173?access_token=" + new JwtSecurityTokenHandler().WriteToken(token.Data));
            return Ok(responseResult);
        }
    }
}
