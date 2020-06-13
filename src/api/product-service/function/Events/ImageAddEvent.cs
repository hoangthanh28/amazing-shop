namespace AmazingShop.Function.Event
{
    public class ImageAddedEvent
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
    }
}