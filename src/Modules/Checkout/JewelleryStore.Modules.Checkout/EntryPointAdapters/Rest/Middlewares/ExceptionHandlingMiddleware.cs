using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JewelleryStore.Modules.Checkout.Exceptions;

namespace JewelleryStore.Modules.Checkout.EntryPointAdapters.Rest.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            if (!await TryHandleAsync(context, exception))
                throw;
        }
    }

    private async Task<bool> TryHandleAsync(HttpContext context, Exception exception)
    {
        if (exception is OperationCanceledException && context.RequestAborted.IsCancellationRequested)
            return true;

        if (exception is not CheckoutException)
            return false;

        _logger.LogWarning(exception, "Se controlo una excepci�n de checkout durante la solicitud");

        var statusCode = exception is CheckoutException checkoutException
            ? checkoutException.StatusCode
            : StatusCodes.Status400BadRequest;

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/problem+json";

        var problemDetails = new ProblemDetails
        {
            Type = $"https://httpstatuses.com/{statusCode}",
            Title = GetTitle(statusCode),
            Status = statusCode,
            Detail = exception.Message,
            Instance = context.Request.Path
        };

        if (exception is CheckoutRequestValidationException validationException)
            problemDetails.Extensions["errors"] = validationException.Errors;

        var json = JsonSerializer.Serialize(
            problemDetails,
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });

        await context.Response.WriteAsync(json);

        return true;
    }

    private static string GetTitle(int statusCode)
    {
        return statusCode switch
        {
            400 => "Bad Request",
            404 => "Not Found",
            409 => "Conflict",
            422 => "Unprocessable Entity",
            _ => "Internal Server Error"
        };
    }
}