using Domain.Entities;
using Domain.Primitives;
using Infrastructure.Repositories;
using IntegrationTests.Fixture;

namespace IntegrationTests;

[Collection(nameof(AppFixture))]
public class BasketRepositoryTests
{
    private readonly AppFixture _fixture;
    private readonly IBasketRepository _repository;
    private readonly Guid _basketId = new Guid("4d6cc033-55d2-4097-ac42-0f1ba8dd227b");
    private readonly Basket _basket1 = new Basket(new Guid("4d6cc033-55d2-4097-ac42-0f1ba8dd227b"), new List<BasketItem>()
    {
        new BasketItem(new Guid("b9b0ccd1-fdb0-410e-8a8f-0392eafe7a48"), new Quantity(10)),
        new BasketItem(new Guid("b7750128-787f-42b5-b2fe-f23ea3dbf3be"), new Quantity(20)),
    });

    private readonly Basket _basket2 = new Basket(new Guid("4d6cc033-55d2-4097-ac42-0f1ba8dd227b"), new List<BasketItem>()
    {
        new BasketItem(new Guid("b7750128-787f-42b5-b2fe-f23ea3dbf3be"), new Quantity(13)),
        new BasketItem(new Guid("b9b0ccd1-fdb0-410e-8a8f-0392eafe7a48"), new Quantity(7)),
        new BasketItem(new Guid("d89cbfad-bd75-4df8-88d7-96a14fd0c155"), new Quantity(44))
    });

    public BasketRepositoryTests(AppFixture fixture)
    {
        _fixture = fixture;
        _repository = fixture.BasketRepository;
    }

    [Fact]
    public async Task InsertBasketAndInsertAnotherBasket_LastInsertedBasket()
    {
        await _repository.Insert(_basket1);
        Basket result1 = await _repository.Get(_basketId);
        Assert.Equal(_basket1, result1);
        
        await _repository.Insert(_basket2);
        Basket result2 = await _repository.Get(_basketId);
        Assert.Equal(_basket2, result2);

        await _fixture.FlushDb();
    }
}