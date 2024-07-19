using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;
using VintageTimepieceService.IService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Azure.Core;
using Newtonsoft.Json;


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
            Response.Cookies.Append("access_token", accessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = token.Data?.ValidTo
            });
            return Ok(new APIResponse<User>
            {
                Message = "Login success",
                isSuccess = true,
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

        [HttpPost, Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Append("access_token", "", new CookieOptions
            {
                Expires = DateTime.Now.AddYears(-1),
                HttpOnly = true
            });
            return Ok(new APIResponse<string>
            {
                Message = "Log out success",
                isSuccess = true,
            });
        }

        [AllowAnonymous]
        [HttpGet, Route("signinWithGoogle")]

        public IActionResult SignInWithGoogle()
        {
            var redirectUrl = Url.Action(nameof(CallBack), "auth", null, Request.Scheme);
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [AllowAnonymous]
        [HttpGet, Route("signin-google")]
        public async Task<IActionResult> CallBack()
        {
            APIResponse<User> responseResult = null;
            APIResponse<JwtSecurityToken> token;
            string accessToken = "", jsonString = "";
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded)
            {
                return Redirect("http://localhost:5173/");
            }

            var email = result.Principal.FindFirst(ClaimTypes.Email).Value;
            var surname = result.Principal.FindFirst(ClaimTypes.Surname).Value;
            var givenName = result.Principal.FindFirst(ClaimTypes.GivenName).Value;

            var existsUser = await _authService.GetUsersByEmail(email);
            if (existsUser.Data != null)
            {
                token = _jwtConfigService.GetAccessTokenFromUser(existsUser.Data);
                accessToken = new JwtSecurityTokenHandler().WriteToken(token.Data);

                Response.Cookies.Append("access_token", accessToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = token.Data?.ValidTo
                });
                return Redirect("http://localhost:5173/");
            }

            var user = new RegisterModel();
            user.Email = email;
            user.FirstName = givenName;
            user.LastName = surname;
            user.JoinedDate = DateTime.Now;
            var registerResult = await _authService.RegisterAccount(user);
            if (!registerResult.isSuccess)
            {
                return Redirect("http://localhost:5173/");
            }


            var newUser = await _authService.GetUsersByEmail(user.Email);
            token = _jwtConfigService.GetAccessTokenFromUser(newUser.Data);
            accessToken = new JwtSecurityTokenHandler().WriteToken(token.Data);

            Response.Cookies.Append("access_token", accessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = token.Data?.ValidTo
            });
            return Redirect("http://localhost:5173/");
        }

        [HttpGet, Route("checkAuthenticate")]
        public IActionResult checkAuthenticate()
        {
            if (HttpContext.Request.Cookies.TryGetValue("access_token", out var access_token))
            {
                if (access_token == null)
                {
                    return Ok(new APIResponse<User>
                    {
                        Message = "Authenticate fail",
                        isSuccess = false,
                    });
                }
                var user = _jwtConfigService.GetUserFromAccessToken(access_token);
                return Ok(new APIResponse<User>
                {
                    Message = "Authenticate success",
                    isSuccess = true,
                    AccessToken = access_token,
                    Data = user.Data
                });
            }
            return Ok(new APIResponse<User>
            {
                Message = "Not authenticate",
                isSuccess = false
            });
        }

        [HttpGet, Route("getAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            var result = await _authService.GetAllUser();
            return Ok(result);
        }

        [HttpPut, Route("UpdateUserInformation")]
        public async Task<IActionResult> UpdateUserInformation([FromForm] string userId, [FromForm] string userData)
        {
            var user = JsonConvert.DeserializeObject<User>(userData);
            var result = await _authService.UpdateUserInformation(int.Parse(userId), user);
            if (result.isSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut, Route("UpdateUserImage")]
        public async Task<IActionResult> UpdateUserImage([FromForm] IFormFile file, [FromForm] int userId)
        {
            var result = await _authService.UpdateUserImage(file, userId);
            return Ok(result);
        }

        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _authService.DeleteUser(id);
            if (result.isSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
