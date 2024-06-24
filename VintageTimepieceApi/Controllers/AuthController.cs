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
            //var redirectUrl = Url.Action("CallBack", "Auth");
            //var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            //return Challenge(properties, GoogleDefaults.AuthenticationScheme);
            var redirectUrl = Url.Action(nameof(CallBack), "auth", null, Request.Scheme);
            //var clientId = _configuration["Google:clientId"];
            //var url = $"https://accounts.google.com/o/oauth2/v2/auth?client_id={clientId}&redirect_uri={redirectUrl}&response_type=code&scope=openid profile email";
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
            //return Redirect(url);
        }


        [AllowAnonymous]
        [HttpGet, Route("signin-google")]
        public async Task<IActionResult> CallBack()
        {
            APIResponse<User> responseResult = null;
            APIResponse<JwtSecurityToken> token;
            string accessToken = "";
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded)
            {
                return Redirect("http://localhost:5173?isAuthenticate=false");
                //return Ok(new APIResponse<string>
                //{
                //    Message = "Sign in with google fail",
                //    isSuccess = false
                //});
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
                return Redirect("http://localhost:5173?isAuthenticate=true");
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
            token = _jwtConfigService.GetAccessTokenFromUser(newUser.Data);
            accessToken = new JwtSecurityTokenHandler().WriteToken(token.Data);
            Response.Cookies.Append("access_token", accessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = token.Data?.ValidTo
            });
            return Redirect("http://localhost:5173?isAuthenticate=true");
        }
    }
}
