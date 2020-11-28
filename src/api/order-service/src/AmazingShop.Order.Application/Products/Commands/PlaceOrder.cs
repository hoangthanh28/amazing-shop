using System;
using System.Collections.Generic;
using System.Linq;
using AmazingShop.Order.Application.Order.Dto;
using AmazingShop.Shared.Core.Model;
using MediatR;

namespace AmazingShop.Order.Application.Order.Command
{
    public class PlaceOrder : IRequest<OrderDto>
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public IEnumerable<OrderDetail> Details { get; set; }
        public PlaceOrder()
        {
            Details = new List<OrderDetail>();
        }

        internal Domain.Entity.Order Create()
        {
            var entity = new Domain.Entity.Order();
            entity.CustomerEmail = this.CustomerEmail;
            entity.CustomerName = this.CustomerName;
            var details = this.Details.Select(d => d.Create());
            foreach (var detail in details)
            {
                entity.Details.Add(detail);
            }
            entity.TotalAmount = entity.Details.Sum(x => x.UnitPrice * x.Qty);
            return entity;
        }
    }
}