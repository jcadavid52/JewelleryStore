namespace JewelleryStore.Runner.Configuration;

public class DatabaseOptions
{
    public const string SectionName = "Database";

    public bool AutomaticMigrations { get; set; }
    public bool AutomaticSeedData { get; set; }
}