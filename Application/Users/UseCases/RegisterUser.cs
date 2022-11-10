using Application.Core;
using Application.Users.Dtos;
using Domain.Entities.Identity;
using Domain.Entities.Identity.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.UseCases;

public class RegisterUser
{
  public class Command : IRequest<Result<UserDto>>
  {
    public RegisterDto RegisterDto { get; set; }
  }

  public class Handler : IRequestHandler<Command, Result<UserDto>>
  {
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;

    public Handler(UserManager<User> userManager, ITokenService tokenService)
    {
      _tokenService = tokenService;
      _userManager = userManager;
    }

    public async Task<Result<UserDto>> Handle(Command request, CancellationToken cancellationToken)
    {
      var user = new User(request.RegisterDto.Name, request.RegisterDto.Email, request.RegisterDto.Email);

      var result = await _userManager.CreateAsync(user, request.RegisterDto.Password);

      if (!result.Succeeded) return Result<UserDto>.Failure(ErrorType.SaveChangesError, "Failed to create user");

      return Result<UserDto>.Success(new UserDto(user.Id, user.Name, user.Email, _tokenService.CreateToken(user)));
    }
  }
}
