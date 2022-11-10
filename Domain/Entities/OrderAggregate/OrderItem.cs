namespace Domain.Entities.OrderAggregate;

public class OrderItem : BaseEntity
{
  public OrderItem()
  {
  }

  public OrderItem(OrderedItem orderedItem, decimal price, int quantity)
  {
    OrderedItem = orderedItem;
    Price = price;
    Quantity = quantity;
  }

  public OrderedItem OrderedItem { get; set; }
  public decimal Price { get; set; }
  public int Quantity { get; set; }
}
