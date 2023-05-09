using System.Net;
using IntegrationTests.Fixture;
using Newtonsoft.Json.Linq;

namespace IntegrationTests;

[Collection(nameof(AppFixture))]
public class BasketControllerTests
{
    private readonly AppFixture _fixture;
    private readonly BasketControllerClient _client;
    private readonly string _basketId = "4d6cc033-55d2-4097-ac42-0f1ba8dd227b";
    private readonly JObject _basket1 = new()
    {
        ["userId"] = "4d6cc033-55d2-4097-ac42-0f1ba8dd227b",
        ["items"] = new JArray()
        {
            new JObject(){["productId"] = "b9b0ccd1-fdb0-410e-8a8f-0392eafe7a48", ["quantity"] = 10},
            new JObject(){["productId"] = "b7750128-787f-42b5-b2fe-f23ea3dbf3be", ["quantity"] = 20}
        }
    };
    private readonly JObject _basket2 = new()
    {
        ["userId"] = "4d6cc033-55d2-4097-ac42-0f1ba8dd227b",
        ["items"] = new JArray()
        {
            new JObject(){["productId"] = "b7750128-787f-42b5-b2fe-f23ea3dbf3be", ["quantity"] = 13},
            new JObject(){["productId"] = "b9b0ccd1-fdb0-410e-8a8f-0392eafe7a48", ["quantity"] = 7},
            new JObject(){["productId"] = "d89cbfad-bd75-4df8-88d7-96a14fd0c155", ["quantit"] = 44},
        }
    };

    public BasketControllerTests(AppFixture fixture)
    {
        _fixture = fixture;
        _client = new BasketControllerClient(fixture.Client);
    }
    
    [Fact]
    public async Task InsertBasketAndInsertAnotherBasket_LastInsertedBasket()
    {
        await _client.Insert(_basket1);
        JObject result1 = await _client.Get(_basketId);
        Assert.Equal(_basket1, result1);

        await _client.Insert(_basket2);
        JObject result2 = await _client.Get(_basketId);
        Assert.Equal(_basket2, result2);

        await _fixture.FlushDb();
    }
}