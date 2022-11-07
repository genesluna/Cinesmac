using Application.Baskets.UseCases;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BasketsController : BaseAPIController
{
  [HttpGet("{id}")]
  public async Task<ActionResult<Basket>> GetBasket(string id, CancellationToken cancellationToken)
  {
    return HandleResult(await Mediator.Send(new DetailBasket.Query { Id = id }, cancellationToken));
  }

  [HttpPost]
  public async Task<IActionResult> CreateBasket(Basket basket, CancellationToken cancellationToken)
  {
    return HandleResult(await Mediator.Send(new CreateBasket.Command { Basket = basket }, cancellationToken));
  }

  [HttpPut]
  public async Task<IActionResult> EditBasket(Basket basket, CancellationToken cancellationToken)
  {
    return HandleResult(await Mediator.Send(new EditBasket.Command { Basket = basket }, cancellationToken));
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteBasket(string id, CancellationToken cancellationToken)
  {
    return HandleResult(await Mediator.Send(new DeleteBasket.Command { Id = id }, cancellationToken));
  }

}
