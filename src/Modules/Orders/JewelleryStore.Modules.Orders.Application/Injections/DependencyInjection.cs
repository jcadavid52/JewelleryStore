using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using JewelleryStore.Modules.Orders.Application.EntryPorts;
using JewelleryStore.Modules.Orders.Application.UseCases.CreateOrder;

namespace JewelleryStore.Modules.Orders.Application.Injections;

public static class DependencyInjection
{
    public static IServiceCollection AddOrdersApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateOrderUseCase, CreateOrderHandler>();

        services.AddValidatorsFromAssemblyContaining<CreateOrderRequestDtoValidator>();

        return services;
    }
}