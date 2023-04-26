using Domain.Entities;
using StackExchange.Redis;

namespace Infrastructure.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly BasketSerializer _serializer;
    private readonly IDatabase _redis;
    private readonly TimeSpan _expire;

    public BasketRepository(IConnectionMultiplexer redis, BasketSerializer serializer, BasketRepositoryOptions options)
    {
        _serializer = serializer;
        _redis = redis.GetDatabase();
        _expire = options.UserBasketExpireTime;
    }
    
    public async Task<Basket> Get(Guid userId)
    {
        string? data = await _redis.StringGetAsync(BuildKey(userId));

        if (data is null)
        {
            throw new EntityNotFoundException(nameof(Basket));
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
            throw new BasketRepositoryException("Failed to create the basket");
        }
    }

    private string BuildKey(Guid userId)
    {
        return $"{userId.ToString()} basket";
    }
}