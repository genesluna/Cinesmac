using Application.Core;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.UseCases;

public class VerifyEmail
{
  public class Query : IRequest<Result<bool>>
  {
    public string Email { get; set; }
  }

  public class Handler : IRequestHandler<Query, Result<bool>>
  {
    private readonly UserManager<User> _userManager;

    public Handler(UserManager<User> userManager)
    {
      _userManager = userManager;
    }

    public async Task<Result<bool>> Handle(Query request, CancellationToken cancellationToken)
    {
      var user = await _userManager.FindByEmailAsync(request.Email);

      return Result<bool>.Success(await _userManager.FindByEmailAsync(request.Email) != null);
    }
  }
}
