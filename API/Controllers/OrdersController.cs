using System.Security.Claims;
using API.Helpers;
using Application.Orders.Dtos;
using Application.Orders.UseCases;
using Domain.Entities.OrderAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class OrdersController : BaseAPIController
{
  [Authorize]
  [HttpPost]
  public async Task<ActionResult<OrderDto>> CreateOrder(OrderCreateDto orderCreateDto)
  {
    return HandleResult(await Mediator.Send(new CreateOrder.Command
    {
      BuyerId = User.FindFirstValue(ClaimTypes.NameIdentifier),
      OrderCreateDto = orderCreateDto
    }));
  }

  [Authorize]
  [HttpGet]
  public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrdersForUser()
  {
    return HandleResult(await Mediator.Send(new ListUserOrders.Query
    {
      UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
    }));
  }

  [Authorize]
  [HttpGet("{id}")]
  public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrdersByIdForUser(Guid id)
  {
    return HandleResult(await Mediator.Send(new DetailUserOrder.Query
    {
      UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
      OrderId = id
    }));
  }

  [Cached(600)]
  [HttpGet("delivery-methods")]
  public async Task<ActionResult<IReadOnlyList<DeliveryMethodDto>>> GetDeliveryMethods()
  {
    return HandleResult(await Mediator.Send(new ListDeliveryMethods.Query()));
  }
}
