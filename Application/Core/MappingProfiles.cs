using Application.Movies.Dtos;
using Application.ScreeningRooms.Dtos;
using Application.Sessions.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Core;

public class MappingProfiles : Profile
{
  public MappingProfiles()
  {
    CreateMap<Movie, MovieDto>();
    CreateMap<MovieCreateDto, Movie>();
    CreateMap<MovieEditDto, Movie>();
    CreateMap<Session, SessionDto>();
    CreateMap<ScreeningRoom, ScreeningRoomDto>();
  }
}
