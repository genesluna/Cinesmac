using Domain.Entities.Enums;

namespace Domain.Entities.OrderAggregate;

public class Order : BaseEntity
{
  public Order() { }

  public Order(ICollection<OrderItem> orderItems,
      string buyerId, Address address, DeliveryMethod deliveryMethod,
      decimal subTotal, string paymentIntentId)
  {
    BuyerId = buyerId;
    Address = address;
    DeliveryMethod = deliveryMethod;
    OrderItems = orderItems;
    SubTotal = subTotal;
    PaymentIntentId = paymentIntentId;
  }

  public string BuyerId { get; set; }
  public Address Address { get; set; }
  public DeliveryMethod DeliveryMethod { get; set; }
  public ICollection<OrderItem> OrderItems { get; set; }
  public decimal SubTotal { get; set; }
  public OrderStatus Status { get; set; } = OrderStatus.Pending;
  public string PaymentIntentId { get; set; }

  public decimal getTotal() => SubTotal + DeliveryMethod.Price;
}
