namespace Api.Controllers;

public class BasketView
{
    public Guid UserId { get; }
    public List<BasketItemView> Items { get; }

    public BasketView(Guid userId, List<BasketItemView> items)
    {
        UserId = userId;
        Items = items;
    }
}