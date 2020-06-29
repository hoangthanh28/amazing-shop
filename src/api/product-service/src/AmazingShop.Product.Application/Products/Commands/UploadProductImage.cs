using AmazingShop.Product.Application.Product.Dto;
using MediatR;

namespace AmazingShop.Product.Application.Product.Command
{
    public class UploadProductImage : IRequest<UploadProductImageDto>
    {
        public string FileName { get; }
        public string OriginalFileName { get; }
        public byte[] Content { get; }
        public string ContentType { get; }
        public int Id { get; set; }
        public string Type { get; set; } = "products";
        public UploadProductImage(int productId, string fileName, byte[] content, string contentType)
        {
            OriginalFileName = fileName;
            FileName = $"images/{Type}/{productId}/{fileName}";
            Content = content;
            ContentType = contentType;
            Id = productId;
        }
    }
}