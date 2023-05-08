namespace Domain.Entities;

public record Basket
{
    public Guid UserId { get; }
    public List<BasketItem> Items { get; }

    public Basket(Guid userId, List<BasketItem> items)
    {
        UserId = userId;
        Items = items;
    }

    public virtual bool Equals(Basket? other)
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