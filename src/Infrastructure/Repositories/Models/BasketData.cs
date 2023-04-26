namespace Infrastructure.Repositories.Models;

public class BasketData
{
    public string UserId { get; }
    public List<BasketItemData> BasketItems { get; }

    public BasketData(string userId, List<BasketItemData> basketItems)
    {
        UserId = userId;
        BasketItems = basketItems;
    }
}