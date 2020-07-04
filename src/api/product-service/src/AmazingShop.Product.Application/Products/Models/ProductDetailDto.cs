using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AmazingShop.Product.Application.Product.Dto
{
    public class ProductDetailDto : BaseDto<int>
    {
        public IEnumerable<ProductImageDto> Images { get; set; }
        public static Expression<Func<Domain.Entity.Product, ProductDetailDto>> Projection
        {
            get
            {
                return dto => new ProductDetailDto
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Images = dto.Images.Select(ProductImageDto.Create)
                };
            }
        }

        public static ProductDetailDto Create(Domain.Entity.Product entity)
        {
            return Projection.Compile().Invoke(entity);
        }
    }
}