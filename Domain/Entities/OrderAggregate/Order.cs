namespace Domain.Entities.OrderAggregate;

public class Order : BaseEntity
{
  public Order() { }

  public Order(ICollection<OrderItem> orderItems,
  string buyerId, Address paymentAddress, decimal total)
  {
    BuyerId = buyerId;
    PaymentAddress = paymentAddress;
    OrderItems = orderItems;
    Total = total;
    Status = OrderStatus.Pending;
  }

  public string BuyerId { get; set; }
  public Address PaymentAddress { get; set; }
  public ICollection<OrderItem> OrderItems { get; set; }
  public decimal Total { get; set; }
  public OrderStatus Status { get; set; }
  public string PaymentIntentId { get; set; }
}
