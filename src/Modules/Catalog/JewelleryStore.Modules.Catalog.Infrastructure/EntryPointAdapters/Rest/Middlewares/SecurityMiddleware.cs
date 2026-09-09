using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace JewelleryStore.Modules.Catalog.Infrastructure.EntryPointAdapters.Rest.Middlewares;

public class SecurityMiddleware
{
    private static readonly HashSet<string> ForbiddenRequestHeaders = new(StringComparer.OrdinalIgnoreCase)
    {
        "X-Forwarded-For",
        "X-Forwarded-Proto",
        "X-Real-IP",
        "X-Original-URL"
    };

    private readonly RequestDelegate _next;

    public SecurityMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (await ValidateRequestAsync(context))
        {
            if (!IsDocumentationRequest(context.Request.Path))
                AddSecurityHeaders(context);
            await _next(context);
        }
    }

    private static bool IsDocumentationRequest(PathString path) =>
        path.StartsWithSegments("/scalar") || path.StartsWithSegments("/openapi");

    private static async Task<bool> ValidateRequestAsync(HttpContext context)
    {
        if (!context.Request.Path.StartsWithSegments("/api"))
            return true;

        if (IsBodyMethod(context.Request.Method))
        {
            var contentType = context.Request.ContentType;
            if (string.IsNullOrEmpty(contentType) ||
                !contentType.StartsWith("application/json", StringComparison.OrdinalIgnoreCase))
            {
                await WriteProblemDetailsAsync(context, StatusCodes.Status415UnsupportedMediaType,
                    "Unsupported Media Type", "El encabezado 'Content-Type' debe ser 'application/json'.");
                return false;
            }
        }

        foreach (var headerName in ForbiddenRequestHeaders)
        {
            if (context.Request.Headers.ContainsKey(headerName))
            {
                await WriteProblemDetailsAsync(context, StatusCodes.Status400BadRequest,
                    "Bad Request", $"El encabezado '{headerName}' no está permitido.");
                return false;
            }
        }

        return true;
    }

    private static void AddSecurityHeaders(HttpContext context)
    {
        var response = context.Response;

        if (!response.Headers.ContainsKey("X-Content-Type-Options"))
            response.Headers["X-Content-Type-Options"] = "nosniff";

        if (!response.Headers.ContainsKey("X-Frame-Options"))
            response.Headers["X-Frame-Options"] = "DENY";

        if (!response.Headers.ContainsKey("Referrer-Policy"))
            response.Headers["Referrer-Policy"] = "no-referrer";

        if (!response.Headers.ContainsKey("Content-Security-Policy"))
            response.Headers["Content-Security-Policy"] = "default-src 'none'";

        if (!response.Headers.ContainsKey("Permissions-Policy"))
            response.Headers["Permissions-Policy"] = "geolocation=(), microphone=(), camera=()";
    }

    private static bool IsBodyMethod(string method) =>
        method is "POST" or "PUT" or "PATCH";

    private static async Task WriteProblemDetailsAsync(
        HttpContext context,
        int statusCode,
        string title,
        string detail)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/problem+json";

        var problemDetails = new
        {
            type = $"https://httpstatuses.com/{statusCode}",
            title,
            status = statusCode,
            detail,
            instance = context.Request.Path.ToString()
        };

        var json = JsonSerializer.Serialize(problemDetails);

        await context.Response.WriteAsync(json);
    }
}