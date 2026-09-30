using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using JewelleryStore.Modules.Checkout.OutputPointAdapters.InProcess;
using JewelleryStore.Modules.Checkout.Services;
using JewelleryStore.Modules.Checkout.UseCases;
using JewelleryStore.Modules.Checkout.UseCases.Checkout;

namespace JewelleryStore.Modules.Checkout.Injections;

public static class DependencyInjection
{
    public static IServiceCollection AddCheckout(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<PaymentOptions>(configuration.GetSection(PaymentOptions.SectionName));

        services.AddScoped<ICheckoutUseCase, CheckoutHandler>();
        services.AddScoped<IPaymentGateway, PaymentGatewayStub>();

        services.AddValidatorsFromAssemblyContaining<CheckoutRequestDtoValidator>();

        return services;
    }

    public static IServiceCollection AddCheckoutInfrastructure(this IServiceCollection services)
    {
        services.AddControllers()
            .AddApplicationPart(typeof(DependencyInjection).Assembly);

        return services;
    }

    public static IApplicationBuilder UseCheckoutExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<EntryPointAdapters.Rest.Middlewares.ExceptionHandlingMiddleware>();
    }
}