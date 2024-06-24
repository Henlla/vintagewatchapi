using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageTimePieceRepository.Util
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;
        public TokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Cookies.TryGetValue("access_token", out var token))
            {
                if (context.Request.Headers.ContainsKey("Authorization"))
                {
                    context.Response.Headers["Authorization"] = $"Bearer {token}";
                }
                else
                {
                    context.Request.Headers.Add("Authorization", $"Bearer {token}");
                }
            }
            else
            {
                if (context.Request.Cookies.TryGetValue("JWT-SESSION", out var googleToken))
                {
                    if (context.Request.Headers.ContainsKey("Authorization"))
                    {
                        context.Response.Headers["Authorization"] = $"Bearer {googleToken}";
                    }
                    else
                    {
                        context.Request.Headers.Add("Authorization", $"Bearer {googleToken}");
                    }
                }
            }
            await _next(context);
        }

    }
    public static class TokenMiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TokenMiddleware>();
        }
    }
}
