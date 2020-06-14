using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using AmazingShop.Function.Event;
using System.IO;

namespace AmazingShop.Function
{
    public class ImageProcessor
    {
        [FunctionName("ImageProcessor")]
        public async Task RunAsync([QueueTrigger("images", Connection = "StorageAccount")] ImageAddedEvent queueEvent,
        [Blob("data/images/{FileName}", FileAccess.Read, Connection = "StorageAccount")] Stream originalImage,
        [Blob("data/images/low/{FileName}", FileAccess.Write, Connection = "StorageAccount")] Stream lowImage,
        [Blob("data/images/medium/{FileName}", FileAccess.Write, Connection = "StorageAccount")] Stream mediumImage,
        [Blob("data/images/high/{FileName}", FileAccess.Write, Connection = "StorageAccount")] Stream highImage,
        ILogger log)
        {
            log.LogInformation(queueEvent.FileName);
            await originalImage.CopyToAsync(highImage);
            await originalImage.CopyToAsync(mediumImage);
            await originalImage.CopyToAsync(lowImage);
        }
    }
}
