namespace JewelleryStore.Modules.Orders.Infrastructure.OutputPointAdapters.Database.SeedData;

public static class OrdersSeedData
{
    public static readonly (Guid CustomerId, string Address, string City, string PostalCode, string Phone, (Guid ProductId, int Quantity, decimal UnitPrice)[] Items)[] Orders =
    {
        (
            Guid.Parse("A3B7D1E2-4F5C-4E6A-9B8C-1D2E3F4A5B6C"),
            "Calle 123 # 45-67",
            "Bogotá",
            "110010",
            "3001234567",
            new[]
            {
                (Guid.Parse("2C0C98E2-EC55-4A79-9B9E-EF76900F8040"), 1, 25.90m)
            }
        ),
        (
            Guid.Parse("B4C8E2F3-506D-4F7B-A9CD-2E3F4A5B6C7D"),
            "Avenida 8 # 12-34",
            "Medellín",
            "050001",
            "3012345678",
            new[]
            {
                (Guid.Parse("31B1A797-778D-4D46-AC8E-AD77129E3D2B"), 2, 45.50m)
            }
        )
    };
}