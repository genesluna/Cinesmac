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
    private readonly IPaymentService _paymentService;

    public Handler(DataContext context, IMapper mapper, IBasketRepository basketRepository, IPaymentService paymentService)
    {
      _paymentService = paymentService;
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

      // check if the order exists and, if so, remove it
      var existingOrder = await _context.Orders.FirstOrDefaultAsync(o => o.PaymentIntentId == basket.PaymentIntentId);
      if (existingOrder != null)
      {
        _context.Orders.Remove(existingOrder);
        await _paymentService.CreateOrUpdatePaymentIntentAsync(basket.PaymentIntentId);
      }

      // create order
      var order = new Order(items, request.BuyerId,
         _mapper.Map<Address>(request.OrderCreateDto.OrderAddress),
         deliveryMethod, subtotal, basket.PaymentIntentId);

      _context.Orders.Add(order);

      // save to db
      var result = await _context.SaveChangesAsync() > 0;

      // return order create result
      if (!result)
        return Result<OrderDto>.Failure(ErrorType.SaveChangesError, "Failed to create order");

      return Result<OrderDto>.Success(_mapper.Map<OrderDto>(order));
    }
  }
}
