using Application.ScreeningRooms.Dtos;

namespace Application.Sessions.Dtos;

public class SessionDto
{
  public Guid Id { get; set; }
  public string StarTime { get; set; }
  public string EndTime { get; set; }
  public decimal BasePrice { get; set; }
  public decimal HalfPrice { get => BasePrice / 2; }
  public decimal VipPrice { get => BasePrice * 2; }
  public ScreeningRoomDto ScreeningRoom { get; set; }
}
