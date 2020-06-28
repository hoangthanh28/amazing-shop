using System;
using System.Linq.Expressions;

namespace AmazingShop.Product.Application.Resource.Dto
{
    public class GetAllResourceDto : BaseDto<int>
    {
        public static Expression<Func<Domain.Entity.Resource, GetAllResourceDto>> Projection
        {
            get
            {
                return resource => new GetAllResourceDto
                {
                    Id = resource.Id,
                    Name = resource.Name
                };
            }
        }

        public static GetAllResourceDto Create(Domain.Entity.Resource member)
        {
            return Projection.Compile().Invoke(member);
        }
    }
}