namespace Application.Orders.Dtos;

public class DeliveryMethodDto
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string DeliveryTime { get; set; }
  public string Description { get; set; }
  public Decimal Price { get; set; }
}
