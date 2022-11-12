using Domain.Entities.OrderAggregate;

namespace Application.Orders.Dtos;

public class OrderDto
{
  public Address Address { get; set; }
  public string DeliveryMethod { get; set; }
  public decimal DeliveryPrice { get; set; }
  public ICollection<OrderItemDto> OrderItems { get; set; }
  public decimal SubTotal { get; set; }
  public decimal Total { get; set; }
  public string Status { get; set; }
  public DateTimeOffset CreatedAt { get; set; }
}
