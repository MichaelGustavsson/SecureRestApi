using Microsoft.AspNetCore.Mvc;
using productsApi.Utilities.Filters;

namespace productsApi.Utilities.Attributes;

public class ApiKeyAttribute : ServiceFilterAttribute
{
    public ApiKeyAttribute() : base(typeof(ApiKeyAuthFilter)) { }
}
