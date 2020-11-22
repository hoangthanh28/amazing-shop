using System.Collections.Generic;
using AmazingShop.Shared.Entity.Abstraction;

namespace AmazingShop.Order.Domain.Entity
{
    public class Order : IEntity<int>
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public decimal TotalAmount { get; set; }
        public bool Deleted { get; set; }
        public ICollection<OrderDetail> Details { get; private set; }
        public System.DateTime CreatedUtc { get; set; }
        public System.DateTime UpdatedUtc { get; set; }
        public Order()
        {
            Details = new List<OrderDetail>();
        }
    }
}