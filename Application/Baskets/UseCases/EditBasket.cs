using System.Text.Json;
using Application.Core;
using Domain.Entities;
using MediatR;
using StackExchange.Redis;

namespace Application.Baskets.UseCases;

public class EditBasket
{
  public class Command : IRequest<Result<Basket>>
  {
    public Basket Basket { get; set; }
  }

  public class Handler : IRequestHandler<Command, Result<Basket>>
  {
    private readonly IDatabase _db;
    public Handler(IConnectionMultiplexer redis)
    {
      _db = redis.GetDatabase();
    }

    public async Task<Result<Basket>> Handle(Command request, CancellationToken cancellationToken)
    {
      var result = await _db.StringSetAsync(request.Basket.Id,
          JsonSerializer.Serialize(request.Basket), TimeSpan.FromDays(7));

      if (!result)
        return Result<Basket>.Failure(ErrorType.SaveChangesError, "Failed to edit basket");

      var basket = JsonSerializer.Deserialize<Basket>(await _db.StringGetAsync(request.Basket.Id));

      return Result<Basket>.Success(basket);

    }
  }
}
