using Domain.Entities;
using Domain.Primitives;
using Infrastructure.Repositories.Models;

namespace Infrastructure.Repositories;

public class DataMapper
{
    public BasketItemData MapBasketItem(BasketItem basketItem)
    {
        return new BasketItemData(
            productId: basketItem.ProductId.ToString(),
            quantity: basketItem.Quantity.Value);
    }

    public List<BasketItemData> MapBasketItem(List<BasketItem> basketItems)
    {
        return basketItems.Select(MapBasketItem).ToList();
    }

    public BasketItem MapBasketItem(BasketItemData basketItemData)
    {
        return new BasketItem(
            productId: new Guid(basketItemData.ProductId),
            quantity: new Quantity(basketItemData.Quantity));
    }

    public List<BasketItem> MapBasketItem(List<BasketItemData> basketItemDatas)
    {
        return basketItemDatas.Select(MapBasketItem).ToList();
    } 

    public BasketData MapBasket(Basket basket)
    {
        return new BasketData(
            userId: basket.UserId.ToString(),
            items: MapBasketItem(basket.Items));
    }

    public Basket MapBasket(BasketData basketData)
    {
        return new Basket(
            userId: new Guid(basketData.UserId),
            items: MapBasketItem(basketData.Items));
    }
}