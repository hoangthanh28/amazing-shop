using System.Collections.Generic;
using AmazingShop.Shared.Entity.Abstraction;

namespace AmazingShop.Product.Domain.Entity
{
    public class Product : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
        public System.DateTime CreatedUtc { get; set; }
        public System.DateTime UpdatedUtc { get; set; }
        public ICollection<ProductImage> Images { get; private set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public Product()
        {
            Images = new List<ProductImage>();
        }
    }
}