using AmazingShop.Shared.Entity.Abstraction;

namespace AmazingShop.Product.Domain.Entity
{
    public class Product : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}