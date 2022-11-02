namespace Domain.Entities;

public enum TicketType
{
  Full, Half, Vip
}

public class Ticket : BaseEntity
{
  public DateTimeOffset Date { get; set; }
  public TicketType TicketType { get; set; }
  public decimal Price { get; private set; }
  public Guid SessionId { get; set; }
  public Session Session { get; set; }
}
