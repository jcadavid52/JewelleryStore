using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using JewelleryStore.Modules.Catalog.Domain.OuputPorts;
using JewelleryStore.Modules.Catalog.Infrastructure.EntryPointAdapters.Rest.Middlewares;
using JewelleryStore.Modules.Catalog.Infrastructure.OutputPointAdapters.Database.SqlServer;
using JewelleryStore.Modules.Catalog.Infrastructure.OutputPointAdapters.Database.SqlServer.Repositories;

namespace JewelleryStore.Modules.Catalog.Infrastructure.Injections;

public static class DependencyInjection
{
    public static IServiceCollection AddCatalogInfrastructure(this IServiceCollection services)
    {
        services.AddControllers()
            .AddApplicationPart(typeof(DependencyInjection).Assembly);

        return services;
    }

    public static IServiceCollection AddCatalogPersistence(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<CatalogDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUnitOfWork, SqlServerUnitOfWork>();

        return services;
    }

    public static IApplicationBuilder UseCatalogExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
