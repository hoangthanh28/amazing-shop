using System.Threading.Tasks;
using AmazingShop.Product.Application.Event.Abstraction;
using AmazingShop.Product.Application.Service.Dispatcher.Abstraction;
using Azure.Storage.Queues;
using Newtonsoft.Json;

namespace AmazingShop.Product.Infrastructure.Service
{
    public class AzureQueueDispatcher : IDomainDispatcher
    {
        private readonly QueueClient _queueClient;

        public AzureQueueDispatcher(QueueClient queue)
        {
            _queueClient = queue;
        }
        public async Task DispatchEventAsync(IEvent evt)
        {
            await _queueClient.CreateIfNotExistsAsync();
            await _queueClient.SendMessageAsync(JsonConvert.SerializeObject(evt));
        }
    }
}