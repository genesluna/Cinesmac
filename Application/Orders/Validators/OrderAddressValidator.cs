using Application.Orders.Dtos;
using FluentValidation;

namespace Application.Orders.Validators;

public class OrderAddressValidator : AbstractValidator<OrderAddressDto>
{
  public OrderAddressValidator()
  {
    RuleFor(x => x.Street).NotEmpty();
    RuleFor(x => x.City).NotEmpty();
    RuleFor(x => x.State).NotEmpty();
    RuleFor(x => x.ZipCode).NotEmpty();
  }
}
