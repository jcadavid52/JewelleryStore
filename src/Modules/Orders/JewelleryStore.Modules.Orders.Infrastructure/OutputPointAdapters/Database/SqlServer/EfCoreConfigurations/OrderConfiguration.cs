using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JewelleryStore.Modules.Orders.Domain.Entities;

namespace JewelleryStore.Modules.Orders.Infrastructure.OutputPointAdapters.Database.SqlServer.EfCoreConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CustomerId)
            .IsRequired();

        builder.OwnsOne(x => x.ShippingAddress, address =>
        {
            address.Property(a => a.Address)
                .HasColumnName("ShippingAddress")
                .IsRequired()
                .HasMaxLength(200);

            address.Property(a => a.City)
                .HasColumnName("ShippingCity")
                .IsRequired()
                .HasMaxLength(100);

            address.Property(a => a.PostalCode)
                .HasColumnName("ShippingPostalCode")
                .IsRequired()
                .HasMaxLength(20);

            address.Property(a => a.Phone)
                .HasColumnName("ShippingPhone")
                .IsRequired()
                .HasMaxLength(20);
        });

        builder.Property(x => x.OrderStatus)
            .IsRequired()
            .HasMaxLength(20)
            .HasConversion<string>();

        builder.Ignore(x => x.Total);

        builder.Ignore(x => x.DomainEvents);

        builder.HasMany<OrderItem>("_orderItems")
            .WithOne(x => x.Order)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.CustomerId);
        builder.HasIndex(x => x.OrderStatus);
    }
}