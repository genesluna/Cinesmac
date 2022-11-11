using Application.Core;
using AutoMapper;
using Domain.Entities.OrderAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Orders.UseCases;

public class ListUserOrders
{
  public class Query : IRequest<Result<List<Order>>>
  {
    public string UserId { get; set; }
  }

  public class Handler : IRequestHandler<Query, Result<List<Order>>>
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public Handler(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<Result<List<Order>>> Handle(Query request, CancellationToken cancellationToken)
    {
      var orders = await _context.Orders
          .Include(o => o.DeliveryMethod)
          .Include(o => o.OrderItems)
          .Where(o => o.BuyerId == request.UserId)
          .ToListAsync();

      if (orders.Count > 0)
        return Result<List<Order>>.Success(orders);

      return Result<List<Order>>.Failure(ErrorType.NotFound, "Orders not found for this user");
    }
  }
}
