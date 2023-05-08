using Domain.Entities;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using StackExchange.Redis;

namespace Infrastructure.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly BasketSerializer _serializer;
    private readonly ILogger<BasketRepository> _logger;
    private readonly IDatabase _redis;
    private readonly TimeSpan _expire;

    public BasketRepository(IConnectionMultiplexer redis, BasketSerializer serializer, BasketRepositoryOptions options, ILogger<BasketRepository> logger)
    {
        _serializer = serializer;
        _logger = logger;
        _redis = redis.GetDatabase();
        _expire = options.UserBasketExpireTime;
    }
    
    public async Task<Basket> Get(Guid userId)
    {
        string? data = await _redis.StringGetAsync(BuildKey(userId));

        if (data is null)
        {
            throw new BasketNotFoundException();
        }

        return _serializer.Deserialize(data);
    }

    public async Task Insert(Basket basket)
    {
        bool result = await _redis.StringSetAsync(
            BuildKey(basket.UserId), 
            _serializer.Serialize(basket),
            _expire);

        if (!result)
        {
            throw new InvalidOperationException("Failed to create the basket");
        }

        _logger.LogInformation("Basket {BasketUserId} inserted", basket.UserId.ToString());
    }

    private string BuildKey(Guid userId)
    {
        return $"{userId.ToString()} basket";
    }
}