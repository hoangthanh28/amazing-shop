using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmazingShop.Product.Persistence.Configuration
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<Domain.Entity.ProductImage>
    {
        public void Configure(EntityTypeBuilder<Domain.Entity.ProductImage> builder)
        {
            // configure the model.
            builder.ToTable("ProductImages");
            builder.HasQueryFilter(x => !x.Deleted);
        }
    }
}
