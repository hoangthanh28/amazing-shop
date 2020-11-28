using System;
using System.Linq.Expressions;
using AmazingShop.Shared.Core.Model;

namespace AmazingShop.Order.Application.Order.Dto
{
    public class OrderDetailDto : BaseDto<int>
    {
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public static Expression<Func<Domain.Entity.OrderDetail, OrderDetailDto>> Projection
        {
            get
            {
                return dto => new OrderDetailDto
                {
                    Id = dto.Id,
                    ProductId = dto.ProductId,
                    ProductName = dto.ProductName,
                    Qty = dto.Qty,
                    UnitPrice = dto.UnitPrice
                };
            }
        }

        public static OrderDetailDto Create(Domain.Entity.OrderDetail entity)
        {
            if (entity == null)
            {
                return null;
            }
            return Projection.Compile().Invoke(entity);
        }
    }
}