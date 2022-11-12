using Application.Core;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Baskets.UseCases;

public class EditBasket
{
  public class Command : IRequest<Result<Basket>>
  {
    public Basket Basket { get; set; }
  }

  public class Handler : IRequestHandler<Command, Result<Basket>>
  {
    private readonly IBasketRepository _basketRepository;
    public Handler(IBasketRepository basketRepository)
    {
      _basketRepository = basketRepository;
    }

    public async Task<Result<Basket>> Handle(Command request, CancellationToken cancellationToken)
    {
      var basket = await _basketRepository.UpdateBasketAsync(request.Basket);

      if (basket == null)
        return Result<Basket>.Failure(ErrorType.SaveChangesError, "Failed to edit basket");

      return Result<Basket>.Success(basket);
    }
  }
}
