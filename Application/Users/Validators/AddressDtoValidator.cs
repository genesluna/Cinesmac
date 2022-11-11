using Application.Users.Dtos;
using FluentValidation;

namespace Application.Users.Validators;

public class AddressDtoValidator : AbstractValidator<UserAddressDto>
{
  public AddressDtoValidator()
  {
    RuleFor(x => x.Street).NotEmpty();
    RuleFor(x => x.City).NotEmpty();
    RuleFor(x => x.State).NotEmpty();
    RuleFor(x => x.ZipCode).NotEmpty();
  }
}
