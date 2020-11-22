using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AmazingShop.Shared.Core.Model;

namespace AmazingShop.Order.Application.Order.Dto
{
    public class OrderDto : BaseDto<int>
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public IEnumerable<OrderDetailDto> Details { get; set; }
        public static Expression<Func<Domain.Entity.Order, OrderDto>> Projection
        {
            get
            {
                return dto => new OrderDto
                {
                    Id = dto.Id,
                    CustomerEmail = dto.CustomerEmail,
                    CustomerName = dto.CustomerName,
                    Details = dto.Details.Select(OrderDetailDto.Create)
                };
            }
        }

        public static OrderDto Create(Domain.Entity.Order entity)
        {
            if (entity == null)
            {
                return null;
            }
            return Projection.Compile().Invoke(entity);
        }
    }
}