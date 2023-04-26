namespace Infrastructure.Repositories;

public class BasketRepositoryOptions
{
    public TimeSpan UserBasketExpireTime { get; }

    public BasketRepositoryOptions(TimeSpan userBasketExpireTime)
    {
        UserBasketExpireTime = userBasketExpireTime;
    }
}