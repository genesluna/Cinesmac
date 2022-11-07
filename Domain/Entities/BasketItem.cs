using Domain.Enums;

namespace Domain.Entities;

public class BasketItem
{
  public string Id { get; set; }
  public int SessionTime { get; set; }
  public TicketType TicketType { get; set; }
  public string MovieTitle { get; set; }
  public string ScreeningRoomName { get; set; }
  public decimal Price { get; set; }
  public int Quantity { get; set; }
  public string ImageUrl { get; set; }
}
