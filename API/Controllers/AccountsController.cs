
using System.Security.Claims;
using API.Controllers;
using API.Errors;
using Application.Users.Dtos;
using Application.Users.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class AccountsController : BaseAPIController
{
  [Authorize]
  [HttpGet]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
  [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse))]
  public async Task<ActionResult<UserDto>> GetCurrentUser(CancellationToken cancellationToken)
  {
    return HandleResult(await Mediator.Send(new DetailCurrentUser.Query { Email = User.FindFirstValue(ClaimTypes.Email) },
      cancellationToken));
  }

  [HttpGet("check-email")]
  public async Task<ActionResult<bool>> CheckIfEmailExists(string email, CancellationToken cancellationToken)
  {
    return HandleResult(await Mediator.Send(new VerifyEmail.Query { Email = email }, cancellationToken));
  }

  [Authorize]
  [HttpGet("address")]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressDto))]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse))]
  public async Task<ActionResult<AddressDto>> GetUserAddress(CancellationToken cancellationToken)
  {
    return HandleResult(await Mediator.Send(new DetailAddress.Query { Email = User.FindFirstValue(ClaimTypes.Email) },
      cancellationToken));
  }

  [HttpPost("login")]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
  [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse))]
  [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiValidationErrorResponse))]
  public async Task<ActionResult<UserDto>> Login(LoginDto loginDto, CancellationToken cancellationToken)
  {
    return HandleResult(await Mediator.Send(new LoginUser.Query { LoginDto = loginDto }, cancellationToken));
  }

  [HttpPost("register")]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
  [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiValidationErrorResponse))]
  public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto, CancellationToken cancellationToken)
  {
    return HandleResult(await Mediator.Send(new RegisterUser.Command { RegisterDto = registerDto }, cancellationToken));
  }

  [Authorize]
  [HttpPut("address")]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressDto))]
  [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse))]
  [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiValidationErrorResponse))]
  public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto, CancellationToken cancellationToken)
  {
    return HandleResult(await Mediator.Send(new EditAddress.Command { Email = User.FindFirstValue(ClaimTypes.Email), AddressDto = addressDto }, cancellationToken));
  }
}
