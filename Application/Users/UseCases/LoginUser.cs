using Application.Core;
using Application.Users.Dtos;
using Domain.Entities.Identity;
using Domain.Interfaces;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.UseCases;

public class LoginUser
{
  public class Query : IRequest<Result<UserDto>>
  {
    public LoginDto LoginDto { get; set; }
  }

  public class Handler : IRequestHandler<Query, Result<UserDto>>
  {
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;

    public Handler(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
    {
      _tokenService = tokenService;
      _signInManager = signInManager;
      _userManager = userManager;
    }

    public async Task<Result<UserDto>> Handle(Query request, CancellationToken cancellationToken)
    {
      var user = await _userManager.FindByEmailAsync(request.LoginDto.Email);

      if (user == null) return Result<UserDto>.Failure(ErrorType.Unauthorized);

      var result = await _signInManager.CheckPasswordSignInAsync(user, request.LoginDto.Password, false);

      if (!result.Succeeded) return Result<UserDto>.Failure(ErrorType.Unauthorized);

      return Result<UserDto>.Success(new UserDto(user.Id, user.Name, user.Email, _tokenService.CreateToken(user)));
    }
  }
}
