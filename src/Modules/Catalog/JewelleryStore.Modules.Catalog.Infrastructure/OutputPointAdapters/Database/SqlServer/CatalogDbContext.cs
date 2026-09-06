using Microsoft.EntityFrameworkCore;
using JewelleryStore.Modules.Catalog.Domain.Entities;

namespace JewelleryStore.Modules.Catalog.Infrastructure.OutputPointAdapters.Database.SqlServer;

public class CatalogDbContext : DbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Catalog");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
    }
}