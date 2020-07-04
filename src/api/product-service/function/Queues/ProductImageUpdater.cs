using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using AmazingShop.Function.Event;
using Dapper;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace AmazingShop.Function
{
    public class ProductImageUpdater
    {
        private readonly IConfiguration _configuration;
        public ProductImageUpdater(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [FunctionName("ProductImageUpdater")]
        public async Task RunAsync([QueueTrigger("resize-completed", Connection = "StorageAccount:ConnectionString")] ImageResizeCompleted queueEvent,
        ILogger log)
        {
            var productConnectionString = _configuration.GetConnectionStringOrSetting("Product");
            var productConnection = new SqlConnection(productConnectionString);
            var query = "update productimages set url = @newUrl where Url = @url and ProductId = @ProductId";
            var newUrl = queueEvent.FilePath.Replace(queueEvent.FileName, $"{queueEvent.Id}/medium/{queueEvent.FileName}");
            log.LogInformation($"New url {newUrl} for {queueEvent.FilePath}");
            _ = await productConnection.ExecuteAsync(query, new { newUrl = newUrl, url = queueEvent.FilePath, ProductId = queueEvent.Id });
        }
    }
}
