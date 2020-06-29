using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using AmazingShop.Function.Event;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace AmazingShop.Function
{
    public class ImageProcessor
    {
        [FunctionName("ImageProcessor")]
        public void Run([QueueTrigger("images", Connection = "StorageAccount:ConnectionString")] ImageAddedEvent queueEvent,
        [Blob("data/images/{Type}/{Id}/{FileName}", FileAccess.Read, Connection = "StorageAccount:ConnectionString")] Stream originalImage,
        [Blob("data/images/{Type}/{Id}/low/{FileName}", FileAccess.Write, Connection = "StorageAccount:ConnectionString")] Stream lowImage,
        [Blob("data/images/{Type}/{Id}/medium/{FileName}", FileAccess.Write, Connection = "StorageAccount:ConnectionString")] Stream mediumImage,
        [Blob("data/images/{Type}/{Id}/high/{FileName}", FileAccess.Write, Connection = "StorageAccount:ConnectionString")] Stream highImage,
        ILogger log)
        {
            log.LogInformation(queueEvent.FileName);
            using (Image image = Image.Load(originalImage))
            {
                // Resize the image in place and return it for chaining.
                // 'x' signifies the current image processing context.
                image.Save(lowImage, new JpegEncoder() { Quality = 20 });
                image.Save(mediumImage, new JpegEncoder() { Quality = 50 });
                image.Save(highImage, new JpegEncoder() { Quality = 90 });
            }
        }
    }
}
