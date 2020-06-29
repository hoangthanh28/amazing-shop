using AmazingShop.Product.Application.Event.Abstraction;

namespace AmazingShop.Product.Application.Event
{
    public class ImageAddedEvent : IEvent
    {
        public string FileName { get; }
        public string FilePath { get; }
        public string ContentType { get; }
        public int Id { get; set; }
        public string Type { get; set; }
        public ImageAddedEvent(int id, string type, string fileName, string filePath, string contentType)
        {
            Id = id;
            Type = type;
            FileName = fileName;
            FilePath = filePath;
            ContentType = contentType;
        }
    }
}