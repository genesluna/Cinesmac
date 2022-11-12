using Application.Core;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Baskets.UseCases;

public class DetailBasket
{
  public class Query : IRequest<Result<Basket>>
  {
    public string BasketId { get; set; }
  }

  public class Handler : IRequestHandler<Query, Result<Basket>>
  {
    private readonly IBasketRepository _basketRepository;
    public Handler(IBasketRepository basketRepository)
    {
      _basketRepository = basketRepository;
    }

    public async Task<Result<Basket>> Handle(Query request, CancellationToken cancellationToken)
    {
      var basket = await _basketRepository.GetBasketAsync(request.BasketId);

      if (basket == null)
        return Result<Basket>.Failure(ErrorType.NoContent, "Basket not found");

      return Result<Basket>.Success(basket);
    }
  }
}
