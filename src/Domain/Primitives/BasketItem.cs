using Domain.Primitives;

namespace Domain.Entities;

public record BasketItem
{
    public Guid ProductId { get; }
    public Quantity Quantity { get; }

    public BasketItem(Guid productId, Quantity quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }
};