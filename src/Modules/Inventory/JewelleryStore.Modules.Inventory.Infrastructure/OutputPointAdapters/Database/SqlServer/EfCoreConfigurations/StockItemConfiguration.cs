using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JewelleryStore.Modules.Inventory.Domain.Entities;
using JewelleryStore.Modules.Inventory.Domain.ValueObjects;

namespace JewelleryStore.Modules.Inventory.Infrastructure.OutputPointAdapters.Database.SqlServer.EfCoreConfigurations;

public class StockItemConfiguration : IEntityTypeConfiguration<StockItem>
{
    public void Configure(EntityTypeBuilder<StockItem> builder)
    {
        builder.ToTable("StockItems");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProductId)
            .IsRequired();

        builder.Property(x => x.Available)
            .IsRequired()
            .HasConversion(
                v => v.Value,
                v => new Quantity(v));

        builder.Property(x => x.Reserved)
            .IsRequired()
            .HasConversion(
                v => v.Value,
                v => new Quantity(v));

        builder.Ignore(x => x.DomainEvents);

        builder.HasIndex(x => x.ProductId)
            .IsUnique()
            .HasDatabaseName("IX_StockItems_ProductId");
    }
}
