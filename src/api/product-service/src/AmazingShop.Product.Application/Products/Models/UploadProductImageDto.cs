namespace AmazingShop.Product.Application.Product.Dto
{
    public class UploadProductImageDto : BaseResponseDto<string>
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public static UploadProductImageDto Create(string name, string filePath, string contentType)
        {
            return new UploadProductImageDto() { Payload = filePath, Name = name, ContentType = contentType };
        }
    }
}