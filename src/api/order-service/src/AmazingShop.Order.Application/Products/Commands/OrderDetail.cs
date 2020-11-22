using System;
using AmazingShop.Order.Application.Order.Dto;
using MediatR;

namespace AmazingShop.Order.Application.Order.Command
{
    public class OrderDetail : IRequest<OrderDetailDto>
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public OrderDetail()
        {
        }

        internal Domain.Entity.OrderDetail Create()
        {
            var detail = new Domain.Entity.OrderDetail();
            detail.ProductId = this.ProductId;
            detail.ProductName = this.ProductName;
            detail.Qty = this.Qty;
            detail.UnitPrice = this.UnitPrice;
            return detail;
        }
    }
}