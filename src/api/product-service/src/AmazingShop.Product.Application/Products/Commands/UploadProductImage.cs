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
        public UploadProductImage(string fileName, byte[] content, string contentType)
        {
            OriginalFileName = fileName;
            FileName = $"images/{fileName}";
            Content = content;
            ContentType = contentType;
        }
    }
}