using System.Security.AccessControl;

namespace Domain.Entities.OrderAggregate;

public class DeliveryMethod : BaseEntity
{
  public string Name { get; set; }
  public string DeliveryTime { get; set; }
  public string Description { get; set; }
  public Decimal Price { get; set; }
}
