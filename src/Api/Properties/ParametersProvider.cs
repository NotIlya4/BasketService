using Infrastructure.Repositories;

namespace Api.Properties;

public class ParametersProvider
{
    private readonly IConfiguration _config;

    public ParametersProvider(IConfiguration config)
    {
        _config = config;
    }
    
    public string GetRedisConnectionString()
    {
        string server = GetRequiredConfiguration("RedisConnectionString:Server");
        string databaseNumber = GetRequiredConfiguration("RedisConnectionString:DatabaseNumber");

        return $"{server},defaultDatabase={databaseNumber}";
    }

    public BasketRepositoryOptions GetBasketRepositoryOptions()
    {
        float days = GetRequiredValue<float>("BasketExpiresInDays");
        return new BasketRepositoryOptions(TimeSpan.FromDays(days));
    }

    private T GetRequiredValue<T>(string key)
    {
        T? value = _config.GetValue<T>(key);
        if (value is null)
        {
            throw new InvalidOperationException($"{key} configuration not found");
        }

        return value;
    }

    private string GetRequiredConfiguration(string path)
    {
        return _config.GetSection(path).Value ?? throw new ConfigurationNotFoundException(path);
    }
}