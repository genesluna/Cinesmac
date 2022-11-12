using System.Text.Json;
using Application.Core;
using Application.Orders.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.OrderAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Orders.UseCases;

public class CreateOrder
{
  public class Command : IRequest<Result<OrderDto>>
  {
    public string BuyerId { get; set; }
    public OrderCreateDto OrderCreateDto { get; set; }
  }

  public class Handler : IRequestHandler<Command, Result<OrderDto>>
  {
    private readonly DataContext _context;
    private readonly StackExchange.Redis.IDatabase _db;
    private readonly IMapper _mapper;
    public Handler(DataContext context, StackExchange.Redis.IConnectionMultiplexer redis, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
      _db = redis.GetDatabase();
    }

    public async Task<Result<OrderDto>> Handle(Command request, CancellationToken cancellationToken)
    {
      // get basket  
      var basketJson = await _db.StringGetAsync(request.OrderCreateDto.BasketId);
      var basket = JsonSerializer.Deserialize<Basket>(basketJson);

      // get items
      var items = new List<OrderItem>();
      foreach (var item in basket.Items)
      {
        var session = await _context.Sessions
            .Include(s => s.Movie)
            .Include(s => s.ScreeningRoom)
            .FirstOrDefaultAsync(s => s.Id == Guid.Parse(item.Id));

        var orderedItem = new OrderedItem(session.Id, session.StarTime,
             item.TicketType, session.Movie.Title, session.ScreeningRoom.Name,
             session.Movie.ImageURL);

        var orderItem = new OrderItem(orderedItem, session
            .getSessionPrice(item.TicketType), item.Quantity);

        items.Add(orderItem);
      }

      // get delivery method
      var deliveryMethod = await _context.DeliveryMethods
          .FindAsync(Guid.Parse(request.OrderCreateDto.DeliveryMethodId));

      // calculate subtotal
      var subtotal = items.Sum(item => item.Price * item.Quantity);

      // create order
      var order = new Order(items, request.BuyerId,
         _mapper.Map<Address>(request.OrderCreateDto.OrderAddress),
         deliveryMethod, subtotal);

      // save to db
      _context.Orders.Add(order);

      var result = await _context.SaveChangesAsync() > 0;

      if (!result)
        return Result<OrderDto>.Failure(ErrorType.SaveChangesError, "Failed to create order");

      // delete basket
      await _db.KeyDeleteAsync(request.OrderCreateDto.BasketId);

      // return order creat result
      return Result<OrderDto>.Success(_mapper.Map<OrderDto>(order));
    }
  }
}