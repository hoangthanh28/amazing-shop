namespace AmazingShop.Shared.Core.Model
{
    public class BaseResponseDto<T>
    {
        public bool IsSuccess { get; set; }
        public T Payload { get; set; }
    }
}