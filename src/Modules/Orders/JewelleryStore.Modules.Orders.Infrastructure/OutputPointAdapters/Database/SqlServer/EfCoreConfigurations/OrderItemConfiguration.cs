using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JewelleryStore.Modules.Orders.Domain.Entities;

namespace JewelleryStore.Modules.Orders.Infrastructure.OutputPointAdapters.Database.SqlServer.EfCoreConfigurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProductId)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .IsRequired();

        builder.Property(x => x.UnitPrice)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Ignore(x => x.SubTotal);

        builder.HasIndex(x => x.OrderId);
        builder.HasIndex(x => x.ProductId);
    }
}