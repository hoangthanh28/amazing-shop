using System;
using System.Linq.Expressions;

namespace AmazingShop.Product.Application.Product.Dto
{
    public class ProductImageDto
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public string Url { get; set; }
        public bool IsEdit { get; set; } = true;
        public static Expression<Func<Domain.Entity.ProductImage, ProductImageDto>> Projection
        {
            get
            {
                return dto => new ProductImageDto
                {
                    Url = dto.Url,
                    ContentType = dto.ContentType,
                    Name = dto.Name,
                    IsEdit = true
                };
            }
        }
        public static ProductImageDto Create(Domain.Entity.ProductImage entity)
        {
            return Projection.Compile().Invoke(entity);
        }
    }
}