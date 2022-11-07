using System.Text.Json;
using Application.Core;
using Domain.Entities;
using MediatR;
using StackExchange.Redis;

namespace Application.Baskets.UseCases;

public class EditBasket
{
  public class Command : IRequest<Result<Unit>>
  {
    public Basket Basket { get; set; }
  }

  public class Handler : IRequestHandler<Command, Result<Unit>>
  {
    private readonly IDatabase _db;
    public Handler(IConnectionMultiplexer redis)
    {
      _db = redis.GetDatabase();
    }

    public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
    {
      var result = await _db.StringSetAsync(request.Basket.Id,
          JsonSerializer.Serialize(request.Basket), TimeSpan.FromDays(7));

      if (!result)
        return Result<Unit>.Failure(ErrorType.SaveChangesError, "Failed to edit basket");

      return Result<Unit>.Success(Unit.Value);

    }
  }
}
