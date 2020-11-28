using System;
using System.Net;
using System.Threading.Tasks;
using AmazingShop.Shared.Core.Service.MultiTenancy.Abstraction;
using Microsoft.AspNetCore.Http;
namespace AmazingShop.Shared.Core.Service.MultiTenancy.Middleware
{
    public class ApiTenancyMiddleware
    {
        private readonly RequestDelegate _next;
        public ApiTenancyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // gather all information that necessary for tenant context
            var tenantContext = context.RequestServices.GetService(typeof(ITenantContext)) as ITenantContext;
            context.Request.Headers.TryGetValue("x-tenant-id", out var tenantIdInHeader);

            // add on exclude rule for get all tenants request.
            // the exclude url include: GET /tenants or POST: /tenants/search -> advanced search
            if (!context.Request.Path.ToString().EndsWith("/tenants") && !context.Request.Path.ToString().EndsWith("/tenants/search"))
            {
                if (string.IsNullOrEmpty(tenantIdInHeader))
                {
                    context.Response.Headers.Add("Content-Type", "application/json");
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await context.Response.WriteAsync("{ \"isSuccess\" : false, \"message\" : \"x-tenant-id is required in the header\" }");
                    return;
                }
                // Set tenant identifier into context
                try
                {
                    tenantContext = tenantContext.SetTenantIdentifier(Guid.Parse(tenantIdInHeader.ToString()));
                }
                catch (Exception exc)
                {
                    context.Response.Headers.Add("Content-Type", "application/json");
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await context.Response.WriteAsync("{ \"isSuccess\" : false, \"message\" : \"" + exc.Message + "\" }");
                    return;
                }
            }
            await _next(context);
        }
    }
}