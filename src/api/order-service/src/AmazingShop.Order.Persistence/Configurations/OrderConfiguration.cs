using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmazingShop.Order.Persistence.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Domain.Entity.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Entity.Order> builder)
        {
            // configure the model.
            builder.ToTable("Orders");
            builder.HasMany(x => x.Details).WithOne(x => x.Order).HasForeignKey(x => x.OrderId);
            builder.HasQueryFilter(x => !x.Deleted);
        }
    }
}
