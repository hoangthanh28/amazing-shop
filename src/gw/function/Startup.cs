using AmazingShop.Shared.Core.Extension;
using Function.Service;
using Function.Service.Abstraction;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(AmazingShop.Function.Startup))]

namespace AmazingShop.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // DI goes here
            builder.Services.AddSharding();
            builder.Services.AddSingleton<IShardingService, ShardingService>();
        }
    }
}