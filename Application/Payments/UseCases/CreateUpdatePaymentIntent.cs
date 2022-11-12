using Application.Baskets.Dtos;
using Application.Core;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Payments.UseCases;

public class CreateUpdatePaymentIntent
{
  public class Command : IRequest<Result<BasketDto>>
  {
    public string BasketId { get; set; }
  }

  public class Handler : IRequestHandler<Command, Result<BasketDto>>
  {
    private readonly IMapper _mapper;
    private readonly IPaymentService _paymentService;
    public Handler(IPaymentService paymentService, IMapper mapper)
    {
      _paymentService = paymentService;
      _mapper = mapper;
    }

    public async Task<Result<BasketDto>> Handle(Command request, CancellationToken cancellationToken)
    {
      var basket = await _paymentService.CreateOrUpdatePaymentIntent(request.BasketId);

      if (basket == null)
        return Result<BasketDto>.Failure(ErrorType.SaveChangesError, "Failed to create/update payment intent");

      return Result<BasketDto>.Success(_mapper.Map<BasketDto>(basket));
    }
  }
}
