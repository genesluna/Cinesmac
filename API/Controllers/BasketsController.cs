using Application.Baskets.UseCases;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BasketsController : BaseAPIController
{
  [HttpGet("{id}")]
  public async Task<ActionResult<Basket>> GetBasket(string id)
  {
    return HandleResult(await Mediator.Send(new DetailBasket.Query { Id = id }));
  }

  [HttpPost]
  public async Task<ActionResult<Basket>> CreateBasket(Basket basket)
  {
    return HandleResult(await Mediator.Send(new CreateBasket.Command { Basket = basket }));
  }

  [HttpPut]
  public async Task<ActionResult<Basket>> EditBasket(Basket basket)
  {
    return HandleResult(await Mediator.Send(new EditBasket.Command { Basket = basket }));
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteBasket(string id)
  {
    return HandleResult(await Mediator.Send(new DeleteBasket.Command { Id = id }));
  }

}
