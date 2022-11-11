using Application.Core;
using AutoMapper;
using Domain.Entities.OrderAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Orders.UseCases;

public class DetailUserOrder
{
  public class Query : IRequest<Result<Order>>
  {
    public string UserId { get; set; }
    public Guid OrderId { get; set; }
  }

  public class Handler : IRequestHandler<Query, Result<Order>>
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public Handler(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<Result<Order>> Handle(Query request, CancellationToken cancellationToken)
    {
      var order = await _context.Orders
          .Include(o => o.DeliveryMethod)
          .Include(o => o.OrderItems)
          .Where(o => o.BuyerId == request.UserId && o.Id == request.OrderId)
          .FirstOrDefaultAsync();

      if (order == null)
        return Result<Order>.Failure(ErrorType.NotFound, "Order not found");

      return Result<Order>.Success(order);
    }
  }
}
