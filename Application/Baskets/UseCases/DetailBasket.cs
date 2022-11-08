using System.Text.Json;
using Application.Core;
using Domain.Entities;
using MediatR;
using StackExchange.Redis;

namespace Application.Baskets.UseCases;

public class DetailBasket
{
  public class Query : IRequest<Result<Basket>>
  {
    public string Id { get; set; }
  }

  public class Handler : IRequestHandler<Query, Result<Basket>>
  {
    private readonly IDatabase _db;
    public Handler(IConnectionMultiplexer redis)
    {
      _db = redis.GetDatabase();
    }

    public async Task<Result<Basket>> Handle(Query request, CancellationToken cancellationToken)
    {
      var basket = await _db.StringGetAsync(request.Id);

      if (basket.IsNullOrEmpty)
        return Result<Basket>.Failure(ErrorType.NoContent, "Basket not found");

      return Result<Basket>.Success(JsonSerializer.Deserialize<Basket>(basket));
    }
  }
}
