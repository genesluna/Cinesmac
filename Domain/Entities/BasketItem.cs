namespace Domain.Entities;

public class BasketItem
{
  public int Id { get; set; }
  public Guid SessionID { get; set; }
  public int SessionTime { get; set; }
  public string MovieTitle { get; set; }
  public string ScreeningRoomName { get; set; }
  public decimal Price { get; set; }
  public int Quantity { get; set; }
  public string ImageUrl { get; set; }
}
