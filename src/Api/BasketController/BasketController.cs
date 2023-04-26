using Domain.Entities;
using Infrastructure.Mappers.BasketEntity;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("baskets")]
public class BasketController : ControllerBase
{
    private readonly IBasketRepository _basketRepository;
    private readonly ViewMapper _mapper;

    public BasketController(IBasketRepository basketRepository, ViewMapper mapper)
    {
        _basketRepository = basketRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("user/id/{userId}")]
    public async Task<ActionResult<BasketView>> GetBasket(string userId)
    {
        Basket basket = await _basketRepository.Get(new Guid(userId));
        BasketView basketView = _mapper.Map(basket);
        return Ok(basketView);
    }

    [HttpPost]
    public async Task<ActionResult> InsertBasket(BasketView basketView)
    {
        Basket basket = _mapper.Map(basketView);
        await _basketRepository.Insert(basket);
        return NoContent();
    }
}