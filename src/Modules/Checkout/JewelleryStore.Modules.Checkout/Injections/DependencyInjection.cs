using FluentValidation;
using JewelleryStore.Modules.Checkout.OutputPointAdapters.InProcess;
using JewelleryStore.Modules.Checkout.Services;
using JewelleryStore.Modules.Checkout.UseCases;
using JewelleryStore.Modules.Checkout.UseCases.Checkout;
using JewelleryStore.Modules.Checkout.UseCases.CreatePaymentIntent;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stripe;

namespace JewelleryStore.Modules.Checkout.Injections;

public static class DependencyInjection
{
    public static IServiceCollection AddCheckout(
        this IServiceCollection services,
        IConfiguration configuration,
        string secretKey)
    {
        services.AddScoped<ICheckoutUseCase, CheckoutHandler>();
        services.AddScoped<ICreatePaymentIntentUseCase, CreatePaymentIntentHandler>();
        services.AddScoped<IOrderService, OrderServiceAdapter>();
        services.AddScoped<IPaymentGateway, PaymentGatewayStripeAdapter>();

        services.AddValidatorsFromAssemblyContaining<CheckoutRequestDtoValidator>();

        ApplyStripeConfiguration(secretKey);

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

    private static void ApplyStripeConfiguration(string secretKey)
    {
        StripeConfiguration.ApiKey = secretKey;
    }
}