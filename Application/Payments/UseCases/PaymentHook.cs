using Domain.Entities.OrderAggregate;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stripe;

namespace Application.Payments.UseCases;

public class PaymentHook
{
  public class Command : IRequest<ActionResult>
  {
    public HttpRequest HttpRequest { get; set; }
  }

  public class Handler : IRequestHandler<Command, ActionResult>
  {
    private readonly ILogger<PaymentHook> _logger;
    private readonly IConfiguration _config;
    private readonly IPaymentService _paymentService;
    public Handler(ILogger<PaymentHook> logger, IConfiguration config, IPaymentService paymentService)
    {
      _paymentService = paymentService;
      _config = config;
      _logger = logger;
    }

    public async Task<ActionResult> Handle(Command request, CancellationToken cancellationToken)
    {
      var json = await new StreamReader(request.HttpRequest.Body).ReadToEndAsync();
      var stripeEvent = EventUtility.ConstructEvent(json,
            request.HttpRequest.Headers["Stripe-Signature"],
            _config["StripeSettings:WhSecret"]);

      var charge = (Charge)stripeEvent.Data.Object;

      Order order;

      switch (charge.Status)
      {
        case "succeeded":
          order = await _paymentService.UpdateOrderPaymentSucceededAsync(charge.PaymentIntentId);
          break;

        case "failed":
          order = await _paymentService.UpdateOrderPaymentFailedAsync(charge.PaymentIntentId);
          break;
      }

      return new EmptyResult();
    }
  }
}
