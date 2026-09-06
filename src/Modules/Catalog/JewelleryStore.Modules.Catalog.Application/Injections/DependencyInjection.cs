using Microsoft.Extensions.DependencyInjection;
using JewelleryStore.Modules.Catalog.Application.EntryPorts;
using JewelleryStore.Modules.Catalog.Application.UseCases.CreateCategory;
using JewelleryStore.Modules.Catalog.Application.UseCases.UpdateCategory;
using JewelleryStore.Modules.Catalog.Application.UseCases.CreateProduct;
using JewelleryStore.Modules.Catalog.Application.UseCases.UpdateProduct;

namespace JewelleryStore.Modules.Catalog.Application.Injections;

public static class DependencyInjection
{
    public static IServiceCollection AddCatalogApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateCategoryUseCase, CreateCategoryHandler>();
        services.AddScoped<IUpdateCategoryUseCase, UpdateCategoryHandler>();
        services.AddScoped<ICreateProductUseCase, CreateProductHandler>();
        services.AddScoped<IUpdateProductUseCase, UpdateProductHandler>();

        return services;
    }
}
