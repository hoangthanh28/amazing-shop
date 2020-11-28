using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using AmazingShop.Shared.Core.Model;
using Dapper;
using Function.Service.Abstraction;
using Function.Service.Model;
using Microsoft.Azure.SqlDatabase.ElasticScale.ShardManagement;
using Microsoft.Azure.SqlDatabase.ElasticScale.ShardManagement.Recovery;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Function.Service
{
    public class ShardingService : IShardingService
    {
        private readonly IConfiguration _configuration;
        private readonly Sharding _shardingProvider;
        public ShardingService(IConfiguration configuration, Sharding shardingProvider)
        {
            _configuration = configuration;
            _shardingProvider = shardingProvider;
        }

        public async Task RegisterAsync(ShardingRequestModel request, ILogger log)
        {
            var connectionBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = _configuration["Sharding:MapManagerServer"],
                InitialCatalog = "Tenant",
                UserID = _configuration["Sharding:MapManagerId"],
                Password = _configuration["Sharding:MapManagerPassword"],
                ApplicationIntent = ApplicationIntent.ReadOnly,
                ApplicationName = "idp-function"
            };
            var tenantConnection = new SqlConnection(connectionBuilder.ConnectionString);

            var query = "select * from shardings with(nolock) where 1 = 1";
            if (!string.IsNullOrEmpty(request.DatabaseName))
            {
                query += " and DatabaseName = @DatabaseName";
            }
            if (!string.IsNullOrEmpty(request.EnvironmentName))
            {
                query += " and EnvironmentName = @EnvironmentName";
            }
            if (!string.IsNullOrEmpty(request.ServiceName))
            {
                query += " and ServiceName = @ServiceName";
            }
            await tenantConnection.OpenAsync();
            var shardings = await tenantConnection.QueryAsync<ShardingModel>(query, request, commandTimeout: 500);
            await tenantConnection.CloseAsync();
            foreach (var sharding in shardings)
            {
                ShardLocation shardLocation = new ShardLocation(sharding.ServerName, sharding.DatabaseName, SqlProtocol.Tcp, 1433);
                RecoveryManager rm = _shardingProvider.ShardMapManager.GetRecoveryManager();
                rm.DetachShard(shardLocation);
                var key = $"{sharding.TenantId}_{sharding.ServiceName}_{sharding.EnvironmentName}".Replace("__", "_").ToLower();
                log?.LogInformation($"Register {key}");
                _shardingProvider.RegisterNewShard(Encoding.UTF8.GetBytes(key), sharding.ServerName, sharding.DatabaseName);
            }
        }
    }
}