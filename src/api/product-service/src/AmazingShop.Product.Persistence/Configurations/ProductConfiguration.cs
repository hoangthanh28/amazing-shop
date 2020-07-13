using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmazingShop.Product.Persistence.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Domain.Entity.Product>
    {
        public void Configure(EntityTypeBuilder<Domain.Entity.Product> builder)
        {
            // configure the model.
            builder.ToTable("Products");
            builder.HasQueryFilter(x => !x.Deleted);
            builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId);
        }
    }
}
