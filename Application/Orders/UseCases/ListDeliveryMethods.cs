using Application.Core;
using Application.Orders.Dtos;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Orders.UseCases;

public class ListDeliveryMethods
{
  public class Query : IRequest<Result<List<DeliveryMethodDto>>> { }

  public class Handler : IRequestHandler<Query, Result<List<DeliveryMethodDto>>>
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public Handler(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<Result<List<DeliveryMethodDto>>> Handle(Query request, CancellationToken cancellationToken)
    {
      var deliveryMethods = await _context.DeliveryMethods.ToListAsync();

      if (deliveryMethods == null)
        return Result<List<DeliveryMethodDto>>.Failure(ErrorType.NotFound, "Delivery Methods not found");

      return Result<List<DeliveryMethodDto>>.Success(_mapper.Map<List<DeliveryMethodDto>>(deliveryMethods));
    }
  }
}
