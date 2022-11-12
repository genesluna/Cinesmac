using Application.Orders.Dtos;
using FluentValidation;

namespace Application.Orders.Validators;

public class OrderCreateValidator : AbstractValidator<OrderCreateDto>
{
  public OrderCreateValidator()
  {
    RuleFor(x => x.BasketId).NotEmpty();
    RuleFor(x => x.DeliveryMethodId).NotEmpty();
  }
}
