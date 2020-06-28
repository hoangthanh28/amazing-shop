namespace AmazingShop.Product.Application
{
    public class BaseResponseDto<T>
    {
        public bool IsSuccess { get; set; }
        public T Payload { get; set; }
    }
}