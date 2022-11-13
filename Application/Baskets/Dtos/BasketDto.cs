namespace Application.Baskets.Dtos;

public class BasketDto
{
  public string Id { get; set; }
  public decimal SubTotal { get; set; }
  public decimal? ShippingPrice { get; set; }
  public decimal Total { get; set; }
  public List<BasketItemDto> Items { get; set; } = new();
  public Guid? DeliveryMethodId { get; set; }
  public string ClientSecret { get; set; }
  public string PaymentIntentId { get; set; }
}
