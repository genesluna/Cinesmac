using Domain.Enums;

namespace Domain.Entities;

public class Session : BaseEntity
{
  public int StarTime { get; set; }
  public int EndTime { get; set; }
  public decimal BasePrice { get; set; }
  public Guid MovieId { get; set; }
  public Movie Movie { get; set; }
  public Guid ScreeningRoomId { get; set; }
  public ScreeningRoom ScreeningRoom { get; set; }

  public decimal getSessionPrice(TicketType ticketType)
  {
    return ticketType switch
    {
      TicketType.Full => BasePrice,
      TicketType.Half => BasePrice / 2,
      TicketType.Vip => BasePrice * 2,
      _ => BasePrice
    };
  }
}
