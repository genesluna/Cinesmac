using Application.Users.Dtos;
using FluentValidation;

namespace Application.Users.Validators;

public class RegisterValidator : AbstractValidator<RegisterDto>
{
  public RegisterValidator()
  {
    RuleFor(x => x.Email).NotEmpty().EmailAddress();
    RuleFor(x => x.Name).NotEmpty();
    RuleFor(x => x.Password)
        .NotEmpty()
        .MinimumLength(8)
        .MaximumLength(16)
        .Matches(@"[A-Z]+")
        .Matches(@"[a-z]+")
        .Matches(@"[0-9]+")
        .Matches(@"[\!\?\*\$\@\&.]+");
  }
}
