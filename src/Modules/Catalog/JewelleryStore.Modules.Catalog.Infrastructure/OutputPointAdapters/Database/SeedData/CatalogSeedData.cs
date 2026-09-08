namespace JewelleryStore.Modules.Catalog.Infrastructure.OutputPointAdapters.Database.SeedData;

public static class CatalogSeedData
{
    public static readonly (string Name, string Description)[] Categories =
    {
        ("Anillos", "Anillos de oro, plata y fantasía para toda ocasión"),
        ("Collares", "Collares y gargantillas para complementar tu look")
    };

    public static readonly (string Name, string Description, string Code, string Care, decimal Price, string CategoryName) Product =
        ("Anillo Clásico de Plata 925",
         "Anillo sencillo elaborado en plata 925, ideal para uso diario.",
         "ANR-0001",
         "Evitar contacto con agua y productos químicos.",
         25.90m,
         "Anillos");
}