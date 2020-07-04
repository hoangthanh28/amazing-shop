using AmazingShop.Product.Application.Event.Abstraction;

namespace AmazingShop.Product.Application.Event
{
    public class ImageAddedEvent : IEvent
    {
        public string FileName { get; }
        public string ContentType { get; }
        public int Id { get; set; }
        public string Type { get; set; }
        public string FilePath { get; set; }
        public ImageAddedEvent(int id, string type, string fileName, string contentType, string filePath)
        {
            Id = id;
            Type = type;
            FileName = fileName;
            ContentType = contentType;
            FilePath = filePath;
        }
    }
}