using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyShop.Service.Abstraction;

namespace MyShop.HttpHandler
{
    public class AuthenticationDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<AuthenticationDelegatingHandler> _logger;
        public AuthenticationDelegatingHandler(IHttpContextAccessor contextAccessor, ILogger<AuthenticationDelegatingHandler> logger)
        {
            _contextAccessor = contextAccessor;
            _logger = logger;
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var userContext = _contextAccessor.HttpContext.RequestServices.GetRequiredService<IUserContext>();
            var accessToken = userContext.GetAccessToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return base.SendAsync(request, cancellationToken);
        }
    }
}