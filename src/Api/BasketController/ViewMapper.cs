using Api.Controllers;
using Domain.Entities;
using Domain.Primitives;

namespace Infrastructure.Mappers.BasketEntity;

public class ViewMapper
{
    public Basket Map(BasketView basketView)
    {
        return new Basket(
            userId: basketView.UserId,
            items: Map(basketView.Items));
    }

    public BasketItem Map(BasketItemView basketItemView)
    {
        return new BasketItem(
            productId: basketItemView.ProductId,
            quantity: new Quantity(basketItemView.Quantity));
    }

    public List<BasketItem> Map(IEnumerable<BasketItemView> basketItemViews)
    {
        return basketItemViews.Select(Map).ToList();
    }

    public BasketView Map(Basket basket)
    {
        return new BasketView(
            userId: basket.UserId,
            items: basket.Items.Select(Map).ToList());
    }

    public BasketItemView Map(BasketItem basketItem)
    {
        return new BasketItemView(
            productId: basketItem.ProductId,
            quantity: basketItem.Quantity.Value);
    }

    public List<BasketItemView> Map(IEnumerable<BasketItem> basketItems)
    {
        return basketItems.Select(Map).ToList();
    }
}