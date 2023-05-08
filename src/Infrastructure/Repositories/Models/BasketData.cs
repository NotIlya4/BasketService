namespace Infrastructure.Repositories.Models;

public record BasketData
{
    public string UserId { get; }
    public List<BasketItemData> Items { get; }

    public BasketData(string userId, List<BasketItemData> items)
    {
        UserId = userId;
        Items = items;
    }

    public virtual bool Equals(BasketData? other)
    {
        if (other is null)
        {
            return false;
        }

        return UserId.Equals(other.UserId) && Items.SequenceEqual(other.Items);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(UserId, Items);
    }
}