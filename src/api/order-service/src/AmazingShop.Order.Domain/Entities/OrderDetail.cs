using System.Collections.Generic;
using AmazingShop.Shared.Entity.Abstraction;

namespace AmazingShop.Order.Domain.Entity
{
    public class OrderDetail : IEntity<int>
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public bool Deleted { get; set; }
        public System.DateTime CreatedUtc { get; set; }
        public System.DateTime UpdatedUtc { get; set; }
        public Order Order { get; set; }
    }
}