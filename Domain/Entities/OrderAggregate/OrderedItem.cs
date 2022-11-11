using Domain.Enums;

namespace Domain.Entities.OrderAggregate;

public class OrderedItem
{
  public OrderedItem() { }

  public OrderedItem(Guid sessionId, int sessionStartTime, TicketType ticketType, string movieTitle, string screeningRoomName, string imageUrl)
  {
    SessionId = sessionId;
    SessionStartTime = sessionStartTime;
    TicketType = ticketType;
    MovieTitle = movieTitle;
    ScreeningRoomName = screeningRoomName;
    ImageUrl = imageUrl;
  }

  public Guid SessionId { get; set; }
  public int SessionStartTime { get; set; }
  public TicketType TicketType { get; set; }
  public string MovieTitle { get; set; }
  public string ScreeningRoomName { get; set; }
  public string ImageUrl { get; set; }
}
