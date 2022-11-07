using Domain.Entities;
using FluentValidation;

namespace Application.Baskets.Validators;

public class BasketValidator : AbstractValidator<Basket>
{
  public BasketValidator()
  {
    RuleFor(x => x.Id).NotEmpty();
    RuleFor(x => x.Items).NotEmpty();
  }
}
