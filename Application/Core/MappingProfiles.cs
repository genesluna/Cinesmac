using Application.Movies.Dtos;
using Application.ScreeningRooms.Dtos;
using Application.Sessions.Dtos;
using Application.Users.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Identity;

namespace Application.Core;

public class MappingProfiles : Profile
{
  public MappingProfiles()
  {
    CreateMap<Address, AddressDto>().ReverseMap();
    CreateMap<Movie, MovieDto>();
    CreateMap<MovieCreateDto, Movie>();
    CreateMap<MovieEditDto, Movie>();
    CreateMap<Session, SessionDto>();
    CreateMap<ScreeningRoom, ScreeningRoomDto>();
  }
}
