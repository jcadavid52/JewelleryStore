using JewelleryStore.Modules.Inventory.Contracts;
using JewelleryStore.Modules.Inventory.Infrastructure.EntryPointAdapters.InProcess;
using Microsoft.Extensions.DependencyInjection;

namespace JewelleryStore.Modules.Inventory.Infrastructure.Injections
{
    public static class EntryPointDependencyInjection
    {
        public static IServiceCollection AddInventoryEntryPoint(this IServiceCollection services)
        {
            services.AddScoped<IReserveStockItemService, ReserveStockItemServiceAdapter>();
            services.AddScoped<IReleaseStockItemService, ReleaseStockItemServiceAdapter>();

            return services;
        }
    }
}
