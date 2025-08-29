using productsApi.Shared;

namespace productsApi.Utilities;

public interface IApiKeyValidator
{
    bool IsValidKey(string appId);
}
public class ApiKeyValidator(IConfiguration configuration) : IApiKeyValidator
{
    private readonly IConfiguration _config = configuration;
    public bool IsValidKey(string appId)
    {
        if (string.IsNullOrWhiteSpace(appId)) return false;

        string? apiKey = _config.GetValue<string>(Constants.KeyName);

        if (apiKey is null || apiKey != appId) return false;

        return true;
    }
}
