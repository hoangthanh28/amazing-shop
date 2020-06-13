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
        public async Task RunAsync([QueueTrigger("images", Connection = "StorageAccount:ConnectionString")] ImageAddedEvent queueEvent,
        [Blob("data/images/{FileName}", FileAccess.Read)] Stream originalImage,
        // [Blob("data/images/low/{FileName}", FileAccess.Write)] Stream lowImage,
        // [Blob("data/images/medium/{FileName}", FileAccess.Write)] Stream mediumImage,
         [Blob("data/images/high_{FileName}", FileAccess.Write)] Stream highImage,
        ILogger log)
        {
            var stream = new MemoryStream();
            await originalImage.CopyToAsync(highImage);
        }
    }
}
