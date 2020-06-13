using System;
using System.IO;
using System.Threading.Tasks;
using AmazingShop.Product.Application.Repository.Abstraction;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AmazingShop.Product.Persistence.Repository
{
    public class AzureBlobStorageService : IStorageService
    {
        private readonly BlobContainerClient _container;
        public AzureBlobStorageService(BlobContainerClient container)
        {
            _container = container;
        }
        public async Task<string> UploadAsync(string fileName, byte[] content, string contentType)
        {
            await _container.CreateIfNotExistsAsync();
            string path = string.Empty;
            using (var stream = new MemoryStream(content))
            {
                // Get a reference to a blob
                BlobClient blobClient = _container.GetBlobClient(fileName);

                // upload with allow override mode
                await blobClient.UploadAsync(stream, new BlobHttpHeaders() { ContentType = contentType });

                path = blobClient.Uri.AbsoluteUri;
            }
            return path;
        }
    }
}