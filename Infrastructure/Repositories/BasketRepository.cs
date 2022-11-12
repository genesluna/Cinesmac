using System.Text.Json;
using Domain.Entities;
using Domain.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Repositories;

public class BasketRepository : IBasketRepository
{
  private readonly IDatabase _db;
  public BasketRepository(IConnectionMultiplexer redis)
  {
    _db = redis.GetDatabase();
  }

  public async Task<bool> DeleteBasketAsync(string basketId)
  {
    return await _db.KeyDeleteAsync(basketId);
  }

  public async Task<Basket> GetBasketAsync(string basketId)
  {
    var basket = await _db.StringGetAsync(basketId);

    return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Basket>(basket);
  }

  public async Task<Basket> UpdateBasketAsync(Basket basket)
  {
    var result = await _db.StringSetAsync(basket.Id,
     JsonSerializer.Serialize(basket), TimeSpan.FromDays(7));

    return !result ? null : await GetBasketAsync(basket.Id);
  }
}
