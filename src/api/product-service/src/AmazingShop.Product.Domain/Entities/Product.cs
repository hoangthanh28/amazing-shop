using System.Collections.Generic;
using AmazingShop.Shared.Entity.Abstraction;

namespace AmazingShop.Product.Domain.Entity
{
    public class Product : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductImage> Images { get; private set; }
        public Product()
        {
            Images = new List<ProductImage>();
        }
    }
}