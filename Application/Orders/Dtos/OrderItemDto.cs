namespace Application.Orders.Dtos;

public class OrderItemDto
{
  public Guid SessionId { get; set; }
  public int SessionStartTime { get; set; }
  public string TicketType { get; set; }
  public string MovieTitle { get; set; }
  public string ScreeningRoomName { get; set; }
  public string ImageUrl { get; set; }
  public decimal Price { get; set; }
  public int Quantity { get; set; }
}

