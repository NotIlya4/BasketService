using System.Text.Json;
using Domain.Entities;
using Infrastructure.Repositories.Models;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Repositories;

public class BasketSerializer
{
    private readonly DataMapper _mapper;

    public BasketSerializer(DataMapper mapper)
    {
        _mapper = mapper;
    }
    
    public string Serialize(Basket basket)
    {
        return Serialize(_mapper.MapBasket(basket));
    }

    public string Serialize(BasketData basketData)
    {
        return JObject.FromObject(basketData).ToString();
    }

    public Basket Deserialize(string basket)
    {
        return _mapper.MapBasket(DeserializeData(basket));
    }

    public BasketData DeserializeData(string basket)
    {
        return JObject.Parse(basket).ToObject<BasketData>();
    }
}