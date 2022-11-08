namespace Domain.Entities;

public class Basket
{
  public Basket() { }

  public Basket(string id)
  {
    Id = id;
  }

  public string Id { get; set; }
  public decimal Total { get; set; }
  public List<BasketItem> Items { get; set; } = new();
}
