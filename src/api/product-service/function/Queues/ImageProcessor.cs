using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using AmazingShop.Function.Event;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.Threading.Tasks;

namespace AmazingShop.Function
{
    public class ImageProcessor
    {
        [FunctionName("ImageProcessor")]
        public async Task RunAsync([QueueTrigger("images", Connection = "StorageAccount:ConnectionString")] ImageAddedEvent queueEvent,
        [Blob("data/images/{Type}/{FileName}", FileAccess.Read, Connection = "StorageAccount:ConnectionString")] Stream originalImage,
        [Blob("data/images/{Type}/{Id}/low/{FileName}", FileAccess.Write, Connection = "StorageAccount:ConnectionString")] Stream lowImage,
        [Blob("data/images/{Type}/{Id}/medium/{FileName}", FileAccess.Write, Connection = "StorageAccount:ConnectionString")] Stream mediumImage,
        [Blob("data/images/{Type}/{Id}/high/{FileName}", FileAccess.Write, Connection = "StorageAccount:ConnectionString")] Stream highImage,
        [Queue("resize-completed", Connection = "StorageAccount:ConnectionString")] IAsyncCollector<ImageResizeCompleted> imageResizeCompletedQueueOutputs,
        ILogger log)
        {
            log.LogInformation(queueEvent.FileName);
            using (Image image = Image.Load(originalImage))
            {
                image.Save(lowImage, new JpegEncoder() { Quality = 20 });
                image.Save(mediumImage, new JpegEncoder() { Quality = 50 });
                image.Save(highImage, new JpegEncoder() { Quality = 90 });
            }
            log.LogInformation($"Insert into resize-completed queue");
            await imageResizeCompletedQueueOutputs.AddAsync(new ImageResizeCompleted()
            {
                ContentType = queueEvent.ContentType,
                Id = queueEvent.Id,
                Type = queueEvent.Type,
                FileName = queueEvent.FileName,
                FilePath = queueEvent.FilePath
            });
        }
    }
}
