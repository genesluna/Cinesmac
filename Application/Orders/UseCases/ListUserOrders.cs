using Application.Core;
using Application.Orders.Dtos;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Orders.UseCases;

public class ListUserOrders
{
  public class Query : IRequest<Result<List<OrderDto>>>
  {
    public string UserId { get; set; }
  }

  public class Handler : IRequestHandler<Query, Result<List<OrderDto>>>
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public Handler(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<Result<List<OrderDto>>> Handle(Query request, CancellationToken cancellationToken)
    {
      var orders = await _context.Orders
          .Include(o => o.DeliveryMethod)
          .Include(o => o.OrderItems)
          .Where(o => o.BuyerId == request.UserId)
          .ToListAsync();

      if (orders.Count > 0)
        return Result<List<OrderDto>>.Success(_mapper.Map<List<OrderDto>>(orders));

      return Result<List<OrderDto>>.Failure(ErrorType.NotFound, "Orders not found for this user");
    }
  }
}
