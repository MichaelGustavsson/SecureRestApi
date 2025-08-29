using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using productsApi.Shared;

namespace productsApi.Utilities.Filters;

public class ApiKeyAuthFilter(IApiKeyValidator validator) : IAuthorizationFilter
{
    private readonly IApiKeyValidator _validator = validator;
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var apiKey = context.HttpContext.Request.Headers[Constants.HeaderName].ToString();

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            context.Result = new BadRequestResult();
            return;
        }

        if (!_validator.IsValidKey(apiKey))
        {
            context.Result = new UnauthorizedResult();
            return;
        }
    }
}
