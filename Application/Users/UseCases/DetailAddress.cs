using Application.Core;
using Application.Users.Dtos;
using AutoMapper;
using Domain.Entities.Identity;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.UseCases;

public class DetailAddress
{
  public class Query : IRequest<Result<AddressDto>>
  {
    public string Email { get; set; }
  }

  public class Handler : IRequestHandler<Query, Result<AddressDto>>
  {
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public Handler(UserManager<User> userManager, IMapper mapper)
    {
      _mapper = mapper;
      _userManager = userManager;
    }

    public async Task<Result<AddressDto>> Handle(Query request, CancellationToken cancellationToken)
    {
      var user = await _userManager.FindByEmailWithAddressAsync(request.Email);

      return Result<AddressDto>.Success(_mapper.Map<AddressDto>(user.Address));
    }
  }
}
