using System.Collections.Generic;
using IdentityServer.Application.Repository.Abstraction;
using IdentityServer.Domain.Entity;
using IdentityServer.Model;
using IdentityServer.Persistence.Context;
using IdentityServer.Persistence.Repository;
using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IdentityServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        //public static readonly ILogger _logger = LoggerFactory.Create(builder => { builder.AddConsole(); }).CreateLogger("Startup");
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization();
            services.AddControllersWithViews();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddDbContext<UserDbContext>((service, option) =>
            {
                var configuration = service.GetService<IConfiguration>();
                var connectionString = Configuration["ConnectionStrings:Idp"];
                option.UseSqlServer(connectionString);
            });

            var connectionString = Configuration["ConnectionStrings:Idp"];
            services.AddIdentityServer(option =>
            {
                option.IssuerUri = "idp";
            })
                // add credentials
                .AddDeveloperSigningCredential()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString);
                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString);
                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 3600; // interval in seconds (default is 3600)
                });
            var externalProviders = new List<ExternalProviderConfig>();

            Configuration.GetSection("Providers").Bind(externalProviders);

            var builder = services.AddAuthentication();
            foreach (var provider in externalProviders)
            {
                switch (provider.Name.ToLowerInvariant())
                {
                    case "aad":
                        builder.AddOpenIdConnect(provider.Name, provider.Description, options =>
                                 {
                                     options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                                     options.SignOutScheme = IdentityServerConstants.SignoutScheme;
                                     options.ClientId = provider.ClientId;
                                     options.Authority = provider.Authority;
                                     options.ClientSecret = provider.ClientSecret;
                                     options.ResponseType = "code";
                                     options.RequireHttpsMetadata = true;
                                     options.CallbackPath = $"/signin-{provider.Name}";
                                     options.SignedOutCallbackPath = $"/signout-callback-{provider.Name}";
                                     options.RemoteSignOutPath = $"/signout-{provider.Name}";
                                 });
                        break;
                    case "google":
                        builder.AddGoogle(options =>
                        {
                            options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                            options.ClientId = provider.ClientId;
                            options.ClientSecret = provider.ClientSecret;
                        });
                        break;
                    case "facebook":
                        builder.AddFacebook(options =>
                        {
                            options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                            options.ClientId = provider.ClientId;
                            options.ClientSecret = provider.ClientSecret;
                        });
                        break;
                }
            }
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
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseIdentityServer();
            app.UseEndpoints(endpoints =>
           {
               endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");
           });
        }
    }
}
