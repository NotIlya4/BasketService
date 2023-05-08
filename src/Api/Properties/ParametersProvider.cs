using Api.Extensions;
using Infrastructure.Repositories;

namespace Api.Properties;

public class ParametersProvider
{
    private readonly IConfiguration _config;

    public ParametersProvider(IConfiguration config)
    {
        _config = config;
    }

    public string Redis => _config.GetRedisConnectionString("RedisConnectionString");

    public BasketRepositoryOptions BasketRepositoryOptions =>
        new (TimeSpan.FromDays(_config.GetRequiredValue<float>("BasketExpiresInDays")));

    public string Seq => _config.GetRequiredValue("SeqUrl");
    }