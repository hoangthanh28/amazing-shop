using AmazingShop.Order.Persistence.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AmazingShop.Order.Application.Repository.Abstraction;
using AmazingShop.Order.Persistence.Repository;
using AmazingShop.Shared.Core.Service.MultiTenancy.Abstraction;
using System.Data.SqlClient;
using AmazingShop.Shared.Core.Model;
using System.Text;
using Microsoft.Azure.SqlDatabase.ElasticScale.ShardManagement;
using Microsoft.Extensions.Hosting;
using AmazingShop.Shared.Core.Extension;

namespace AmazingShop.Order.Persistence.Extension
{
    public static class PersistenceExtensions
    {
        public static void AddPersistenceServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSharding();
            serviceCollection.AddDbContext<OrderDbContext>((service, option) =>
            {
                var configuration = service.GetService<IConfiguration>();
                // this service name can config in the configuration file 
                var serviceName = "order-service";
                var environment = service.GetService<IHostingEnvironment>().EnvironmentName;

                var sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
                {
                    UserID = configuration["Sharding:ShardId"],
                    Password = configuration["Sharding:ShardPassword"]
                };
                var tenantContext = service.GetService(typeof(ITenantContext)) as ITenantContext;
                var tenantId = tenantContext.TenantId;
                // build the key of shardmap
                // it should be a range or key depend on the stragegy, ref: https://docs.microsoft.com/en-us/azure/sql-database/sql-database-elastic-scale-shard-map-management
                var key = $"{tenantId}_{serviceName}_{environment}".ToLower();
                var shard = service.GetService(typeof(Sharding)) as Sharding;

                // consider implement the memory cache for connection to improve performance
                var connection = shard.ShardMap.OpenConnectionForKey<byte[]>(Encoding.UTF8.GetBytes(key), sqlConnectionStringBuilder.ConnectionString, ConnectionOptions.Validate);
                // // let EF handle the open and close connection
                // if (connection.State == ConnectionState.Open)
                // {
                //     connection.Close();
                // }
                option.UseSqlServer(connection);

            });
            serviceCollection.AddScoped<IOrderRepository, OrderRepository>();
        }
    }
}