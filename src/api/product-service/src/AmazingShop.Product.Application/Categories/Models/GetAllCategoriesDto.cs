using System;
using System.Linq.Expressions;

namespace AmazingShop.Product.Application.Category.Dto
{
    public class GetAllCategoriesDto : BaseDto<int>
    {
        public static Expression<Func<Domain.Entity.Category, GetAllCategoriesDto>> Projection
        {
            get
            {
                return model => new GetAllCategoriesDto
                {
                    Id = model.Id,
                    Name = model.Name
                };
            }
        }

        public static GetAllCategoriesDto Create(Domain.Entity.Category entity)
        {
            return Projection.Compile().Invoke(entity);
        }
    }
}