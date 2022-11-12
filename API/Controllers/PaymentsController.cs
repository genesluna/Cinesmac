using Application.Baskets.Dtos;
using Application.Payments.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PaymentsController : BaseAPIController
{
  [Authorize]
  [HttpPost("{basketId}")]
  public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent(string basketId)
  {
    return HandleResult(await Mediator.Send(new CreateUpdatePaymentIntent.Command { BasketId = basketId }));
  }

  [HttpPost("webhook")]
  public async Task<IActionResult> StripeWebhook()
  {
    return await Mediator.Send(new PaymentHook.Command { HttpRequest = Request });
  }
}
