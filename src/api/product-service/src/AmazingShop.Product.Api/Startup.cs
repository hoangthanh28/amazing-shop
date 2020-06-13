using System.Collections.Generic;
using AmazingShop.Product.Application.Extension;
using AmazingShop.Product.Infrastructure.Extension;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AmazingShop.Product
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
            services.AddInfrastructureServices();
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

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
