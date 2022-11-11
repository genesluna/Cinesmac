namespace Application.Orders.Dtos;

public class OrderCreateDto
{
  public string DeliveryMethodId { get; set; }
  public string BasketId { get; set; }
  public OrderAddressDto OrderAddress { get; set; }
}
