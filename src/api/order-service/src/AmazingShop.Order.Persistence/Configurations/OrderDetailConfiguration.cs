using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmazingShop.Order.Persistence.Configuration
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<Domain.Entity.OrderDetail>
    {
        public void Configure(EntityTypeBuilder<Domain.Entity.OrderDetail> builder)
        {
            // configure the model.
            builder.ToTable("OrderDetails");
            builder.HasQueryFilter(x => !x.Deleted);
        }
    }
}
