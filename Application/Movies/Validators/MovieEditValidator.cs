using Application.Movies.Dtos;
using FluentValidation;

namespace Application.Movies.Validators;

public class MovieEditValidator : AbstractValidator<MovieEditDto>
{
  public MovieEditValidator()
  {
    RuleFor(x => x.Id).NotEmpty();
    RuleFor(x => x.Title).NotEmpty();
    RuleFor(x => x.Description).NotEmpty();
    RuleFor(x => x.Director).NotEmpty();
    RuleFor(x => x.Genre).NotEmpty();
    RuleFor(x => x.Length).NotEmpty();
    RuleFor(x => x.IMDBScore).NotEmpty();
    RuleFor(x => x.ImageURL).NotEmpty();
    RuleFor(x => x.Is3D).NotEmpty();
    RuleFor(x => x.IsIMAX).NotEmpty();
  }
}

