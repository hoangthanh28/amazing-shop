using System.Collections.Generic;
using AmazingShop.Order.Application.Extension;
using AmazingShop.Order.Persistence.Extension;
using AmazingShop.Shared.Core.Service.MultiTenancy.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AmazingShop.Order
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApplicationServices();
            services.AddPersistenceServices();
            services.AddAuthentication().AddIdentityServerAuthentication("oidc", options =>
             {
                 options.Authority = Configuration["Idp:Authority"];
                 options.RequireHttpsMetadata = options.Authority.Contains("https") ? true : false;
             });
            services.AddCors(o => o.AddPolicy("CorsPolicy", cor =>
         {
             var hosts = new List<string>();
             Configuration.GetSection("AllowedHosts").Bind(hosts);
             cor.WithOrigins(hosts.ToArray())
             .AllowAnyMethod()
             .AllowAnyHeader()
             .AllowCredentials();
         }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ApiTenancyMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
