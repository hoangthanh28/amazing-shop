namespace AmazingShop.Product.Application
{
    public class BaseDto<T>
    {
        public bool IsSuccess { get; set; }
        public T Payload { get; set; }
    }
}