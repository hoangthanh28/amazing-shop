using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Function.Service.Abstraction;
using Microsoft.Extensions.Logging;
using Function.Service.Model;

namespace Function.Trigger.Http
{
    public class ShardmapManager
    {
        private readonly IConfiguration _configuration;
        private readonly IShardingService _shardingService;

        public ShardmapManager(IConfiguration configuration, IShardingService shardingService)
        {
            _configuration = configuration;
            _shardingService = shardingService;
        }

        [FunctionName("RegisterShard")]
        public async Task<IActionResult> AddAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "gw/shardmaps/register")] HttpRequestMessage req, ILogger log)
        {
            if (await Function.Helper.SecurityHelper.ValidateTokenAsync(req.Headers.Authorization) == null)
            {
                return new UnauthorizedResult();
            }
            var body = await req.Content.ReadAsStringAsync();
            ShardingRequestModel model = JsonConvert.DeserializeObject<ShardingRequestModel>(body);
            await _shardingService.RegisterAsync(model, log);
            return new OkObjectResult(new { IsSuccess = true });
        }
    }
}