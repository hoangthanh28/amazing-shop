using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyShop.HttpHandler;
using MyShop.Middleware;
using MyShop.Service;
using MyShop.Service.Abstraction;
using Polly;

namespace MyShop
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
            services.AddControllersWithViews();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            }).AddCookie("Cookies")
           .AddOpenIdConnect("oidc", options =>
           {
               options.Authority = Configuration["Idp:Authority"];
               options.RequireHttpsMetadata = options.Authority.Contains("https") ? true : false;
               options.ClientId = Configuration["Idp:ClientId"];
               options.SaveTokens = true;
               options.ResponseType = "code";
           });
            services.AddScoped<IUserContext, UserContext>();
            services.AddTransient<AuthenticationDelegatingHandler>();
            services.AddHttpContextAccessor();
            services.AddHttpClient("product-service", (service, client) =>
            {
                client.BaseAddress = new System.Uri(Configuration["Api:ProductService"]);
            }).AddHttpMessageHandler<AuthenticationDelegatingHandler>()
            .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(2, _ => System.TimeSpan.FromMilliseconds(600)))
            .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, System.TimeSpan.FromSeconds(30)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<AuthorizationMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
