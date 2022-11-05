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
  public ICollection<Ticket> Tickets { get; set; }
}
