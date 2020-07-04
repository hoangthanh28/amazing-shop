using AmazingShop.Shared.Entity.Abstraction;

namespace AmazingShop.Product.Domain.Entity
{
    public class ProductImage : IEntity<int>
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string Url { get; set; }
        public string ContentType { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
        public System.DateTime CreatedUtc { get; set; }
        public System.DateTime UpdatedUtc { get; set; }
    }
}