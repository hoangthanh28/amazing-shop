using System.Collections.Generic;
using AmazingShop.Shared.Entity.Abstraction;

namespace AmazingShop.Product.Domain.Entity
{
    public class Resource : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Category> Categories { get; private set; }
        public Resource()
        {
            Categories = new List<Category>();
        }

    }
}