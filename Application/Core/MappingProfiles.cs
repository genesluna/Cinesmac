using Application.Baskets.Dtos;
using Application.Movies.Dtos;
using Application.Orders.Dtos;
using Application.ScreeningRooms.Dtos;
using Application.Sessions.Dtos;
using Application.Users.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.OrderAggregate;

namespace Application.Core;

public class MappingProfiles : Profile
{
  public MappingProfiles()
  {
    CreateMap<Domain.Entities.Identity.Address, UserAddressDto>().ReverseMap();
    CreateMap<DeliveryMethod, DeliveryMethodDto>().ReverseMap();
    CreateMap<Basket, BasketDto>().ReverseMap();
    CreateMap<BasketItem, BasketItemDto>().ReverseMap();
    CreateMap<Movie, MovieDto>();
    CreateMap<Movie, MoviesListDto>();
    CreateMap<MovieCreateDto, Movie>();
    CreateMap<MovieEditDto, Movie>();
    CreateMap<Session, SessionDto>();
    CreateMap<ScreeningRoom, ScreeningRoomDto>();
    CreateMap<OrderAddressDto, Domain.Entities.OrderAggregate.Address>().ReverseMap();

    CreateMap<Order, OrderDto>()
        .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.Name))
        .ForMember(d => d.DeliveryPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));

    CreateMap<OrderItem, OrderItemDto>()
        .ForMember(d => d.SessionId, o => o.MapFrom(s => s.OrderedItem.SessionId))
        .ForMember(d => d.MovieTitle, o => o.MapFrom(s => s.OrderedItem.MovieTitle))
        .ForMember(d => d.ScreeningRoomName, o => o.MapFrom(s => s.OrderedItem.ScreeningRoomName))
        .ForMember(d => d.SessionStartTime, o => o.MapFrom(s => s.OrderedItem.SessionStartTime))
        .ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.OrderedItem.ImageUrl))
        .ForMember(d => d.TicketType, o => o.MapFrom(s => s.OrderedItem.TicketType));
  }
}
