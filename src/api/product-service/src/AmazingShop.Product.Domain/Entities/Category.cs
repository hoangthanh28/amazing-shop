using System.Collections.Generic;
using AmazingShop.Shared.Entity.Abstraction;

namespace AmazingShop.Product.Domain.Entity
{
    public class Category : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Resource Resource { get; set; }
        public int ResourceId { get; set; }
        public ICollection<Product> Products { get; private set; }
        public Category()
        {
            Products = new List<Product>();
        }

    }
}