using Domain.Entities;

namespace Domain.Interfaces;

public interface IPaymentService
{
  Task<Basket> CreateOrUpdatePaymentIntent(string basketId);
}
