using Application.Baskets.Dtos;
using FluentValidation;

namespace Application.Baskets.Validators;

public class BasketValidator : AbstractValidator<BasketDto>
{
  public BasketValidator()
  {
    RuleFor(x => x.Id).NotEmpty();
    RuleFor(x => x.Total).NotEmpty();
    RuleFor(x => x.SubTotal).NotEmpty();
    RuleFor(x => x.Items).NotEmpty();
  }
}
