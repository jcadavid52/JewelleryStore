using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using JewelleryStore.Modules.Inventory.Application.EntryPorts;
using JewelleryStore.Modules.Inventory.Application.UseCases.CreateStockItem;
using JewelleryStore.Modules.Inventory.Application.UseCases.ReceiveStockItem;
using JewelleryStore.Modules.Inventory.Application.UseCases.ReserveStockItem;
using JewelleryStore.Modules.Inventory.Application.UseCases.GetAllStockItem;
using JewelleryStore.Modules.Inventory.Application.UseCases.GetStockItemById;
using JewelleryStore.Modules.Inventory.Application.UseCases.GetStockItemByProductId;

namespace JewelleryStore.Modules.Inventory.Application.Injections;

public static class DependencyInjection
{
    public static IServiceCollection AddInventoryApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateStockItemUseCase, CreateStockItemHandler>();
        services.AddScoped<IReceiveStockItemUseCase, ReceiveStockItemHandler>();
        services.AddScoped<IReserveStockItemUseCase, ReserveStockItemHandler>();
        services.AddScoped<IGetStockItemByIdUseCase, GetStockItemByIdHandler>();
        services.AddScoped<IGetStockItemByProductIdUseCase, GetStockItemByProductIdHandler>();
        services.AddScoped<IGetAllStockItemUseCase, GetAllStockItemHandler>();

        services.AddValidatorsFromAssemblyContaining<CreateStockItemRequestDtoValidator>();

        return services;
    }
}
