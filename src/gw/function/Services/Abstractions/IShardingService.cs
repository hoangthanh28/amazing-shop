using System.Threading.Tasks;
using Function.Service.Model;
using Microsoft.Extensions.Logging;

namespace Function.Service.Abstraction
{
    public interface IShardingService
    {
        Task RegisterAsync(ShardingRequestModel request, ILogger log);
    }
}