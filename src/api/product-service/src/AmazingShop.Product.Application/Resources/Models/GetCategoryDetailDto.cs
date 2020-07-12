using System;
using System.Linq.Expressions;

namespace AmazingShop.Product.Application.Resource.Dto
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
            return Projection.Compile().Invoke(entity);
        }
    }
}