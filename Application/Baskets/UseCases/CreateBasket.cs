using Application.Baskets.Dtos;
using Application.Core;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Baskets.UseCases;

public class CreateBasket
{
  public class Command : IRequest<Result<BasketDto>>
  {
    public BasketDto BasketDto { get; set; }
  }

  public class Handler : IRequestHandler<Command, Result<BasketDto>>
  {
    private readonly IBasketRepository _basketRepository;
    private readonly IMapper _mapper;
    public Handler(IBasketRepository basketRepository, IMapper mapper)
    {
      _mapper = mapper;
      _basketRepository = basketRepository;
    }

    public async Task<Result<BasketDto>> Handle(Command request, CancellationToken cancellationToken)
    {
      var basket = await _basketRepository.UpdateBasketAsync(_mapper.Map<Basket>(request.BasketDto));

      if (basket == null)
        return Result<BasketDto>.Failure(ErrorType.SaveChangesError, "Failed to create basket");

      return Result<BasketDto>.Success(_mapper.Map<BasketDto>(basket));
    }
  }
}
