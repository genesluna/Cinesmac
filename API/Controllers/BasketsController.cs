using Application.Baskets.Dtos;
using Application.Baskets.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BasketsController : BaseAPIController
{
  [HttpGet("{basketId}")]
  public async Task<ActionResult<BasketDto>> GetBasket(string basketId)
  {
    return HandleResult(await Mediator.Send(new DetailBasket.Query { BasketId = basketId }));
  }

  [HttpPost]
  public async Task<ActionResult<BasketDto>> CreateBasket(BasketDto basket)
  {
    return HandleResult(await Mediator.Send(new CreateBasket.Command { BasketDto = basket }));
  }

  [HttpPut]
  public async Task<ActionResult<BasketDto>> EditBasket(BasketDto basket)
  {
    return HandleResult(await Mediator.Send(new EditBasket.Command { BasketDto = basket }));
  }

  [HttpDelete("{basketId}")]
  public async Task<IActionResult> DeleteBasket(string basketId)
  {
    return HandleResult(await Mediator.Send(new DeleteBasket.Command { BasketId = basketId }));
  }

}
