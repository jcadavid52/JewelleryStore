using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using JewelleryStore.Modules.Catalog.Application.EntryPorts;
using JewelleryStore.Modules.Catalog.Application.UseCases.CreateCategory;
using JewelleryStore.Modules.Catalog.Application.UseCases.UpdateCategory;
using JewelleryStore.Modules.Catalog.Application.UseCases.CreateProduct;
using JewelleryStore.Modules.Catalog.Application.UseCases.UpdateProduct;
using JewelleryStore.Modules.Catalog.Application.UseCases.GetAllCatalog;
using JewelleryStore.Modules.Catalog.Application.UseCases.GetAllCategory;
using JewelleryStore.Modules.Catalog.Application.UseCases.GetProductById;
using JewelleryStore.Modules.Catalog.Application.UseCases.GetCategoryById;

namespace JewelleryStore.Modules.Catalog.Application.Injections;

public static class DependencyInjection
{
    public static IServiceCollection AddCatalogApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateCategoryUseCase, CreateCategoryHandler>();
        services.AddScoped<IUpdateCategoryUseCase, UpdateCategoryHandler>();
        services.AddScoped<IGetAllCategoryUseCase, GetAllCategoryHandler>();
        services.AddScoped<IGetCategoryByIdUseCase, GetCategoryByIdHandler>();
        services.AddScoped<ICreateProductUseCase, CreateProductHandler>();
        services.AddScoped<IUpdateProductUseCase, UpdateProductHandler>();
        services.AddScoped<IGetAllCatalogUseCase, GetAllCatalogHandler>();
        services.AddScoped<IGetProductByIdUseCase, GetProductByIdHandler>();

        services.AddValidatorsFromAssemblyContaining<CreateProductRequestDtoValidator>();

        return services;
    }
}
