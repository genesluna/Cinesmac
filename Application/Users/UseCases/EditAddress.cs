using Application.Core;
using Application.Users.Dtos;
using AutoMapper;
using Domain.Entities.Identity;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.UseCases;

public class EditAddress
{
  public class Command : IRequest<Result<UserAddressDto>>
  {
    public string Email { get; set; }
    public UserAddressDto AddressDto { get; set; }
  }

  public class Handler : IRequestHandler<Command, Result<UserAddressDto>>
  {
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public Handler(UserManager<User> userManager, IMapper mapper)
    {
      _mapper = mapper;
      _userManager = userManager;
    }

    public async Task<Result<UserAddressDto>> Handle(Command request, CancellationToken cancellationToken)
    {
      var user = await _userManager.FindByEmailWithAddressAsync(request.Email);

      user.Address = _mapper.Map<Address>(request.AddressDto);

      var result = await _userManager.UpdateAsync(user);

      if (!result.Succeeded) return Result<UserAddressDto>.Failure(ErrorType.SaveChangesError, "Failed to update address");

      return Result<UserAddressDto>.Success(_mapper.Map<UserAddressDto>(user.Address));
    }
  }
}
