using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MyShop.Service.Abstraction;

namespace MyShop.Middleware
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuthorizationMiddleware> _logger;
        public AuthorizationMiddleware(RequestDelegate next, ILogger<AuthorizationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var userContext = context.RequestServices.GetService(typeof(IUserContext)) as IUserContext;
                var accessToken = await context.GetTokenAsync("access_token");
                userContext.SetAccessToken(accessToken);
            }
            await _next(context);
        }
    }
}