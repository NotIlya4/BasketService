using System.Text;

namespace Api.Extensions;

public static class ConfigurationConnectionExtensions
{
    public static string GetRedisConnectionString(this IConfiguration config, string? key = null)
    {
        if (key is not null)
        {
            config = config.GetSection(key);
        }
        
        string server = config.GetRequiredValue("Server");
        string? defaultDatabase = config["DefaultDatabase"];

        StringBuilder connectionString = new($"{server}");

        if (defaultDatabase is not null)
        {
            connectionString.Append($",defaultDatabase={defaultDatabase}");
        }

        return connectionString.ToString();
    }
    
    public static string GetRequiredValue(this IConfiguration config, string key)
    {
        return config.GetRequiredValue<string>(key);
    }
    
    public static T GetRequiredValue<T>(this IConfiguration config, string key)
    {
        T? value = config.GetValue<T>(key);
        if (value is null)
        {
            throw new InvalidOperationException(key);
        }
        return value;
    }
}