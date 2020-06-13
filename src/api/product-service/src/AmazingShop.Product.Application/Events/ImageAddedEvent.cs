using AmazingShop.Product.Application.Event.Abstraction;

namespace AmazingShop.Product.Application.Event
{
    public class ImageAddedEvent : IEvent
    {
        public string FileName { get; }
        public string FilePath { get; }
        public string ContentType { get; }
        public ImageAddedEvent(string fileName, string filePath, string contentType)
        {
            FileName = fileName;
            FilePath = filePath;
            ContentType = contentType;
        }
    }
}