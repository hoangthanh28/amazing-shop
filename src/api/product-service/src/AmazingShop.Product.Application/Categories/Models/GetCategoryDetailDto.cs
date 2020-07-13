using System;
using System.Linq.Expressions;

namespace AmazingShop.Product.Application.Category.Dto
{
    public class GetCategoryDetailDto : BaseDto<int>
    {
        public static Expression<Func<Domain.Entity.Category, GetCategoryDetailDto>> Projection
        {
            get
            {
                return model => new GetCategoryDetailDto
                {
                    Id = model.Id,
                    Name = model.Name
                };
            }
        }

        public static GetCategoryDetailDto Create(Domain.Entity.Category entity)
        {
            if (entity == null)
            {
                return null;
            }
            return Projection.Compile().Invoke(entity);
        }
    }
}