using AmazingShop.Product.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmazingShop.Product.Persistence.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // configure the model.
            builder.ToTable("Categories");
            builder.HasOne(x => x.Resource).WithMany().HasForeignKey(x => x.ResourceId);
        }
    }
}
