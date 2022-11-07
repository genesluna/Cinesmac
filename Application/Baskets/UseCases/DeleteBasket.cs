using Application.Core;
using MediatR;
using StackExchange.Redis;

namespace Application.Baskets.UseCases;

public class DeleteBasket
{
  public class Command : IRequest<Result<Unit>>
  {
    public string Id { get; set; }
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
      var result = await _db.KeyDeleteAsync(request.Id);

      if (!result)
        return Result<Unit>.Failure(ErrorType.SaveChangesError, "Failed to delete basket");

      return Result<Unit>.Success(Unit.Value);

    }
  }
}
