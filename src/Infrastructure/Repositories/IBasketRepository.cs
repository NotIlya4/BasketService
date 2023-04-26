using Domain.Entities;

namespace Infrastructure.Repositories;

public interface IBasketRepository
{
    Task<Basket> Get(Guid userId);
    Task Insert(Basket basket);
}