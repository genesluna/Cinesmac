using Application.Core;
using Application.Users.Dtos;
using Domain.Entities.Identity;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.UseCases;

public class DetailCurrentUser
{
  public class Query : IRequest<Result<UserDto>>
  {
    public string Email { get; set; }
  }

  public class Handler : IRequestHandler<Query, Result<UserDto>>
  {
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;

    public Handler(UserManager<User> userManager, ITokenService tokenService)
    {
      _tokenService = tokenService;
      _userManager = userManager;
    }

    public async Task<Result<UserDto>> Handle(Query request, CancellationToken cancellationToken)
    {
      var user = await _userManager.FindByEmailAsync(request.Email);

      return Result<UserDto>.Success(new UserDto(user.Id, user.Name, user.Email, _tokenService.CreateToken(user)));
    }
  }
}
