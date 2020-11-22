using System.Data.SqlClient;
using AmazingShop.Shared.Core.Model;
using AmazingShop.Shared.Core.Service.MultiTenancy;
using AmazingShop.Shared.Core.Service.MultiTenancy.Abstraction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AmazingShop.Shared.Core.Extension
{
    public static class ServiceExtension
    {
        public static void AddSharding(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<Sharding>(service =>
            {
                var configuration = service.GetRequiredService(typeof(IConfiguration)) as IConfiguration;
                var shardingSection = configuration["Sharding:MapManagerId"];
                if (!string.IsNullOrEmpty(shardingSection))
                {
                    var connectionStringBuilder = new SqlConnectionStringBuilder()
                    {
                        UserID = configuration["Sharding:MapManagerId"],
                        Password = configuration["Sharding:MapManagerPassword"],
                        InitialCatalog = configuration["Sharding:MapManagerDatabase"],
                        DataSource = configuration["Sharding:MapManagerServer"],
                        MultipleActiveResultSets = false,
                        TrustServerCertificate = true
                    };
                    return new Sharding(configuration["Sharding:MapManagerDatabase"], connectionStringBuilder.ConnectionString);
                }
                return null;
            });
            serviceCollection.AddHttpContextAccessor();
            serviceCollection.AddScoped<ITenantContext, TenantContext>();
        }
    }
}