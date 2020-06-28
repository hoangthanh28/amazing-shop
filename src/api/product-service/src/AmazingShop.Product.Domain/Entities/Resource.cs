using AmazingShop.Shared.Entity.Abstraction;

namespace AmazingShop.Product.Domain.Entity
{
    public class Resource : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}