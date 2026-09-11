using Microsoft.AspNetCore.RateLimiting;
using JewelleryStore.Modules.Catalog.Application.Injections;
using JewelleryStore.Modules.Catalog.Infrastructure.Injections;
using JewelleryStore.Runner.Configuration;

var builder = WebApplication.CreateBuilder(args);

var dbOptions = builder.Configuration
    .GetSection(DatabaseOptions.SectionName)
    .Get<DatabaseOptions>() ?? new DatabaseOptions();

var rateLimitingOptions = builder.Configuration
    .GetSection(RateLimitingOptions.SectionName)
    .Get<RateLimitingOptions>() ?? new RateLimitingOptions();

var connectionString = builder.Configuration.GetConnectionString("Connection")
    ?? throw new InvalidOperationException("Connection string 'Connection' no configurada.");

builder.Services.AddCatalogApplication();
builder.Services.AddCatalogInfrastructure();
builder.Services.AddCatalogOpenApi();
builder.Services.AddCatalogRateLimiting(
    rateLimitingOptions.Enabled,
    rateLimitingOptions.PermitLimit,
    rateLimitingOptions.WindowSeconds,
    rateLimitingOptions.QueueLimit);
builder.Services.AddCatalogPersistence(
    connectionString,
    applyMigrations: dbOptions.AutomaticMigrations,
    seedDataOnStartup: dbOptions.AutomaticSeedData);

var app = builder.Build();

app.UseCatalogSecurity();
app.UseCatalogExceptionHandling();
app.UseCatalogRateLimiting();
app.MapCatalogOpenApi();
app.MapControllers().RequireRateLimiting("ApiRateLimit");

app.MapGet("/", () => "Hello World!");

app.Run();