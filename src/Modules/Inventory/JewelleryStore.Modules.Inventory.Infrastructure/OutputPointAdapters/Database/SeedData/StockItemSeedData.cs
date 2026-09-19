namespace JewelleryStore.Modules.Inventory.Infrastructure.OutputPointAdapters.Database.SeedData
{
    public static class StockItemSeedData
    {
        public static readonly (Guid ProductId, int Quantity)[] Items = new[]
        {
            (Guid.Parse("2C0C98E2-EC55-4A79-9B9E-EF76900F8040"), 10),
            (Guid.Parse("31B1A797-778D-4D46-AC8E-AD77129E3D2B"), 5)
        };
    }
}
