using System;
using System.Text;
using System.Threading.Tasks;
using AmazingShop.Product.Application.Event.Abstraction;
using AmazingShop.Product.Application.Service.Dispatcher.Abstraction;
using Azure.Storage.Queues;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AmazingShop.Product.Infrastructure.Service
{
    public class AzureQueueDispatcher : IDomainDispatcher
    {
        private readonly QueueClient _queueClient;
        private readonly ILogger<AzureQueueDispatcher> _logger;
        public AzureQueueDispatcher(QueueClient queue, ILogger<AzureQueueDispatcher> logger)
        {
            _queueClient = queue;
            _logger = logger;
        }
        public async Task DispatchEventAsync(IEvent evt)
        {
            await _queueClient.CreateIfNotExistsAsync();
            var json = JsonConvert.SerializeObject(evt);
            _logger.LogInformation($"Dispatch message: {json}");
            // IMPORTANCE: need to convert the body of the message into base64 when using library with version v12.
            await _queueClient.SendMessageAsync(Convert.ToBase64String(Encoding.UTF8.GetBytes(json)));
        }
    }
}