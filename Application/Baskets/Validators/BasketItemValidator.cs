using Domain.Entities;
using FluentValidation;

namespace Application.Baskets.Validators;

public class BasketItemValidator : AbstractValidator<BasketItem>
{
  public BasketItemValidator()
  {
    RuleFor(x => x.Id).NotEmpty();
    RuleFor(x => x.SessionStartTime).NotEmpty();
    RuleFor(x => x.TicketType).NotNull();
    RuleFor(x => x.MovieTitle).NotEmpty();
    RuleFor(x => x.ScreeningRoomName).NotEmpty();
    RuleFor(x => x.Price).NotEmpty();
    RuleFor(x => x.Quantity).NotEmpty().GreaterThan(0);
    RuleFor(x => x.ImageUrl).NotEmpty();
  }
}
