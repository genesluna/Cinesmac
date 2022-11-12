using Domain.Entities;
using Domain.Entities.OrderAggregate;

namespace Domain.Interfaces;

public interface IPaymentService
{
  Task<Basket> CreateOrUpdatePaymentIntentAsync(string basketId);
  Task<Order> UpdateOrderPaymentSucceededAsync(string paymentIntentId);
  Task<Order> UpdateOrderPaymentFailedAsync(string paymentIntentId);
}
