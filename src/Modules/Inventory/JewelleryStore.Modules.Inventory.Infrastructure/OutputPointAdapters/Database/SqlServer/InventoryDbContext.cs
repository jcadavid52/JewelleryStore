using Microsoft.EntityFrameworkCore;
using JewelleryStore.Modules.Inventory.Domain.Entities;

namespace JewelleryStore.Modules.Inventory.Infrastructure.OutputPointAdapters.Database.SqlServer
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
            : base(options)
        {
        }

        public DbSet<StockItem> StockItems => Set<StockItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Inventory");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryDbContext).Assembly);
        }
    }
}
