namespace AmazingShop.Shared.Core.Model
{
    public class BaseDto<T>
    {
        public T Id { get; set; }
        public string Name { get; set; }
    }
}