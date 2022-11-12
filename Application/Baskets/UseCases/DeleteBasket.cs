using Application.Core;
using Domain.Interfaces;
using MediatR;

namespace Application.Baskets.UseCases;

public class DeleteBasket
{
  public class Command : IRequest<Result<Unit>>
  {
    public string BasketId { get; set; }
  }

  public class Handler : IRequestHandler<Command, Result<Unit>>
  {
    private readonly IBasketRepository _basketRepository;
    public Handler(IBasketRepository basketRepository)
    {
      _basketRepository = basketRepository;
    }

    public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
    {
      var result = await _basketRepository.DeleteBasketAsync(request.BasketId);

      if (!result)
        return Result<Unit>.Failure(ErrorType.SaveChangesError, "Failed to delete basket");

      return Result<Unit>.Success(Unit.Value);
    }
  }
}
