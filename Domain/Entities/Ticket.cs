using Domain.Enums;

namespace Domain.Entities;

public class Ticket : BaseEntity
{
  public TicketType TicketType { get; set; }
  public decimal Price { get; private set; }
  public Guid SessionId { get; set; }
  public Session Session { get; set; }
}
