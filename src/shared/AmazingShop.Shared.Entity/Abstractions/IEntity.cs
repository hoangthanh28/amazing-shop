namespace AmazingShop.Shared.Entity.Abstraction
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
