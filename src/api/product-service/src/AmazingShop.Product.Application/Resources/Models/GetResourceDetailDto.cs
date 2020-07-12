using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AmazingShop.Product.Application.Resource.Dto
{
    public class GetResourceDetailDto : BaseDto<int>
    {
        public IEnumerable<GetCategoryDetailDto> Categories { get; set; }
        public static Expression<Func<Domain.Entity.Resource, GetResourceDetailDto>> Projection
        {
            get
            {
                return resource => new GetResourceDetailDto
                {
                    Id = resource.Id,
                    Name = resource.Name,
                    Categories = resource.Categories.Select(GetCategoryDetailDto.Create)
                };
            }
        }

        public static GetResourceDetailDto Create(Domain.Entity.Resource member)
        {
            return Projection.Compile().Invoke(member);
        }
    }
}