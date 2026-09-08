using JewelleryStore.Modules.Catalog.Application.Injections;
using JewelleryStore.Modules.Catalog.Infrastructure.Injections;
using JewelleryStore.Runner.Configuration;

var builder = WebApplication.CreateBuilder(args);

var dbOptions = builder.Configuration
    .GetSection(DatabaseOptions.SectionName)
    .Get<DatabaseOptions>() ?? new DatabaseOptions();

var connectionString = builder.Configuration.GetConnectionString("Connection")
    ?? throw new InvalidOperationException("Connection string 'Connection' no configurada.");

builder.Services.AddCatalogApplication();
builder.Services.AddCatalogInfrastructure();
builder.Services.AddCatalogOpenApi();
builder.Services.AddCatalogPersistence(
    connectionString,
    applyMigrations: dbOptions.AutomaticMigrations,
    seedDataOnStartup: dbOptions.AutomaticSeedData);

var app = builder.Build();

app.UseCatalogSecurity();
app.UseCatalogExceptionHandling();
app.MapCatalogOpenApi();
app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();