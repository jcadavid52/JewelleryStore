using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JewelleryStore.Modules.Catalog.Domain.Exceptions;

namespace JewelleryStore.Modules.Catalog.Infrastructure.EntryPointAdapters.Rest.Middlewares;

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
            await HandleAsync(context, exception);
        }
    }

    private async Task HandleAsync(HttpContext context, Exception exception)
    {
        var (statusCode, logLevel) = ResolveStatus(exception);

        if (logLevel == LogLevel.Error)
            _logger.LogError(exception, "Ocurrió un error no controlado al procesar la solicitud");
        else
            _logger.LogWarning(exception, "Se controló una excepción de dominio durante la solicitud");

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
    }

    private static (int StatusCode, LogLevel LogLevel) ResolveStatus(Exception exception)
    {
        return exception switch
        {
            DomainException domainException => (domainException.StatusCode, LogLevel.Warning),
            ArgumentException => (400, LogLevel.Warning),
            _ => (500, LogLevel.Error)
        };
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