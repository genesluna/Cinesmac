using Application.Core;
using Application.Orders.Dtos;
using AutoMapper;
using Domain.Entities.OrderAggregate;
using Domain.Interfaces;
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
    private readonly IMapper _mapper;
    private readonly IBasketRepository _basketRepository;

    public Handler(DataContext context, IMapper mapper, IBasketRepository basketRepository)
    {
      _basketRepository = basketRepository;
      _mapper = mapper;
      _context = context;
    }

    public async Task<Result<OrderDto>> Handle(Command request, CancellationToken cancellationToken)
    {
      // get basket  
      var basket = await _basketRepository.GetBasketAsync(request.OrderCreateDto.BasketId);

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
      await _basketRepository.DeleteBasketAsync(request.OrderCreateDto.BasketId);

      // return order creat result
      return Result<OrderDto>.Success(_mapper.Map<OrderDto>(order));
    }
  }
}
