using Application.Core;
using Domain.Entities.OrderAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Orders.UseCases;

public class ListDeliveryMethods
{
  public class Query : IRequest<Result<List<DeliveryMethod>>> { }

  public class Handler : IRequestHandler<Query, Result<List<DeliveryMethod>>>
  {
    private readonly DataContext _context;

    public Handler(DataContext context)
    {
      _context = context;
    }

    public async Task<Result<List<DeliveryMethod>>> Handle(Query request, CancellationToken cancellationToken)
    {
      var deliveryMethods = await _context.DeliveryMethods.ToListAsync();

      if (deliveryMethods == null)
        return Result<List<DeliveryMethod>>.Failure(ErrorType.NotFound, "Delivery Methods not found");

      return Result<List<DeliveryMethod>>.Success(deliveryMethods);
    }
  }
}
