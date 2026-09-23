using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JewelleryStore.Modules.Orders.Domain.Exceptions;

namespace JewelleryStore.Modules.Orders.Infrastructure.EntryPointAdapters.Rest.Middlewares;

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

        if (exception is not DomainException)
            return false;

        _logger.LogWarning(exception, "Se controló una excepción de dominio durante la solicitud");

        var statusCode = exception is DomainException domainException ? domainException.StatusCode : StatusCodes.Status400BadRequest;

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

        if (exception is RequestValidationException validationException)
            problemDetails.Extensions["errors"] = validationException.Errors;

        var json = JsonSerializer.Serialize(
            problemDetails,
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
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
            _ => "Internal Server Error"
        };
    }
}