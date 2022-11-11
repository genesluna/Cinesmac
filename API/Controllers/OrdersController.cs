using System.Security.Claims;
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
  public async Task<ActionResult<Order>> CreateOrder(OrderCreateDto orderCreateDto)
  {
    return HandleResult(await Mediator.Send(new CreateOrder.Command
    {
      BuyerId = User.FindFirstValue(ClaimTypes.NameIdentifier),
      OrderCreateDto = orderCreateDto
    }));
  }
}
