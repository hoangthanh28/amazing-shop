using AmazingShop.Product.Application.Repository.Abstraction;
using AmazingShop.Product.Application.Service.Dispatcher.Abstraction;
using AmazingShop.Product.Infrastructure.Service;
using AmazingShop.Product.Persistence.Repository;
using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AmazingShop.Product.Infrastructure.Extension
{
    public static class InfrastructureExtension
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IStorageService, AzureBlobStorageService>();
            serviceCollection.AddSingleton<IDomainDispatcher, AzureQueueDispatcher>();
            serviceCollection.AddSingleton<BlobContainerClient>(service =>
            {
                var configuration = service.GetRequiredService<IConfiguration>();
                var connectionString = configuration["StorageAccount:ConnectionString"];
                // Create a BlobServiceClient object which will be used to create a container client
                BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
                var containerName = configuration["StorageAccount:ContainerName"];
                return blobServiceClient.GetBlobContainerClient(containerName);
            });
            serviceCollection.AddSingleton<QueueClient>(service =>
            {
                var configuration = service.GetRequiredService<IConfiguration>();
                var connectionString = configuration["StorageAccount:ConnectionString"];
                // Create a BlobServiceClient object which will be used to create a container client
                //BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
                var queueName = configuration["StorageAccount:ImageQueueName"];
                QueueClient queueClient = new QueueClient(connectionString, queueName);
                return queueClient;
            });
        }
    }
}