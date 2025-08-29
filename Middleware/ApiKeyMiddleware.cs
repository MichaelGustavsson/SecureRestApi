using System.Net;
using productsApi.Shared;
using productsApi.Utilities;

namespace productsApi.Middleware;

public class ApiKeyMiddleware(RequestDelegate next, IApiKeyValidator validator)
{
    private readonly RequestDelegate _next = next;
    private readonly IApiKeyValidator _validator = validator;

    public async Task InvokeAsync(HttpContext context)
    {
        var apiKey = context.Request.Headers[Constants.HeaderName];

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return;
        }

        if (!_validator.IsValidKey(apiKey!))
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return;
        }

        await _next(context);
    }
}
