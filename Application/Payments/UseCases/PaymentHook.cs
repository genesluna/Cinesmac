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

      PaymentIntent intent;
      Order order;

      switch (charge.Status)
      {
        case "succeeded":
          intent = (PaymentIntent)stripeEvent.Data.Object;
          _logger.LogInformation("Payment Succeeded: ", intent.Id);
          order = await _paymentService.UpdateOrderPaymentSucceededAsync(intent.Id);
          _logger.LogInformation("Order updated to payment received: ", intent.Id);
          break;

        case "payment_failed":
          intent = (PaymentIntent)stripeEvent.Data.Object;
          _logger.LogInformation("Payment Failed: ", intent.Id);
          order = await _paymentService.UpdateOrderPaymentFailedAsync(intent.Id);
          _logger.LogInformation("Order updated to payment failed: ", intent.Id);
          break;
      }

      return new EmptyResult();
    }
  }
}
