using System;
using System.Linq.Expressions;

namespace AmazingShop.Product.Application.Product.Dto
{
    public class UpdateProductDto : BaseDto<int>
    {
        public static Expression<Func<Domain.Entity.Product, UpdateProductDto>> Projection
        {
            get
            {
                return dto => new UpdateProductDto
                {
                    Id = dto.Id,
                    Name = dto.Name
                };
            }
        }

        public static UpdateProductDto Create(Domain.Entity.Product entity)
        {
            return Projection.Compile().Invoke(entity);
        }
    }
}