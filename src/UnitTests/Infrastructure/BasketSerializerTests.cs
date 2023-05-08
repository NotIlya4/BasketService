using Domain.Entities;
using Domain.Primitives;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Models;
using Newtonsoft.Json.Linq;

namespace UnitTests.Infrastructure;

public class BasketSerializerTests
{
    private readonly Basket _basket = new(new Guid("ff3642c4-ebe8-4e98-b434-5e66dd71ba78"), new List<BasketItem>()
    {
        new (new Guid("aa4ea1c4-0630-4305-822b-d8c9c93389b2"), new Quantity(4)),
        new (new Guid("146498e9-4c83-4c97-ad34-26c146e48eed"), new Quantity(2))
    });
    private readonly BasketData _basketData = new("ff3642c4-ebe8-4e98-b434-5e66dd71ba78", new List<BasketItemData>()
    {
        new("aa4ea1c4-0630-4305-822b-d8c9c93389b2", 4),
        new("146498e9-4c83-4c97-ad34-26c146e48eed", 2)
    });
    private readonly BasketSerializer _serializer = new BasketSerializer(new DataMapper());
    private JObject BasketJObject => JObject.FromObject(_basketData);
    
    [Fact]
    public void Serialize_Basket_BasketJsonString()
    {
        string result = _serializer.Serialize(_basket);
        
        Assert.Equal(BasketJObject.ToString(), result);
    }
    
    [Fact]
    public void Serialize_BasketData_BasketJsonString()
    {
        string result = _serializer.Serialize(_basketData);
        
        Assert.Equal(BasketJObject.ToString(), result);
    }

    [Fact]
    public void Deserialize_BasketJsonString_Basket()
    {
        Basket result = _serializer.Deserialize(BasketJObject.ToString());

        Assert.Equal(_basket, result);
    }

    [Fact]
    public void DeserializeData_BasketDataJsonString_BasketData()
    {
        BasketData result = _serializer.DeserializeData(BasketJObject.ToString());
        
        Assert.Equal(_basketData, result);
    }
}