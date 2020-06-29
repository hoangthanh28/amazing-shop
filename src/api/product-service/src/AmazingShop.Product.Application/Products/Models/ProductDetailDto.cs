using System;
using System.Linq.Expressions;

namespace AmazingShop.Product.Application.Product.Dto
{
    public class ProductDetailDto : BaseDto<int>
    {
        public static Expression<Func<Domain.Entity.Product, ProductDetailDto>> Projection
        {
            get
            {
                return dto => new ProductDetailDto
                {
                    Id = dto.Id,
                    Name = dto.Name
                };
            }
        }

        public static ProductDetailDto Create(Domain.Entity.Product entity)
        {
            return Projection.Compile().Invoke(entity);
        }
    }
}