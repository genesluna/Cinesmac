using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Persistence;
using Stripe;

namespace Infrastructure.Services;

public class PaymentService : IPaymentService
{
  private readonly IBasketRepository _basketRepository;
  private readonly DataContext _context;
  private readonly IConfiguration _config;

  public PaymentService(IBasketRepository basketRepository, DataContext context, IConfiguration config)
  {
    _basketRepository = basketRepository;
    _context = context;
    _config = config;
  }

  public async Task<Basket> CreateOrUpdatePaymentIntent(string basketId)
  {
    StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];

    var basket = await _basketRepository.GetBasketAsync(basketId);
    var deliveryPrice = 0m;

    if (basket.DeliveryMethodId.HasValue)
    {
      var deliveryMethod = await _context.DeliveryMethods.FindAsync(basket.DeliveryMethodId);
      deliveryPrice = deliveryMethod.Price;
    }

    foreach (var item in basket.Items)
    {
      var session = await _context.Sessions.FindAsync(Guid.Parse(item.Id));

      if (item.Price != session.getSessionPrice(item.TicketType))
        item.Price = session.getSessionPrice(item.TicketType);
    }

    var service = new PaymentIntentService();
    PaymentIntent intent;

    if (string.IsNullOrEmpty(basket.PaymentIntentId))
    {
      var options = new PaymentIntentCreateOptions
      {
        Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)deliveryPrice * 100,
        Currency = "brl",
        PaymentMethodTypes = new List<string> { "card" }
      };
      intent = await service.CreateAsync(options);
      basket.PaymentIntentId = intent.Id;
      basket.ClientSecret = intent.ClientSecret;
    }
    else
    {
      var options = new PaymentIntentUpdateOptions
      {
        Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)deliveryPrice * 100,
      };
      await service.UpdateAsync(basket.PaymentIntentId, options);
    }
    await _basketRepository.UpdateBasketAsync(basket);

    return basket;
  }
}
