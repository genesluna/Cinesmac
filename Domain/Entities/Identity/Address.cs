namespace Domain.Entities.Identity;

public class Address
{
  public Guid Id { get; set; }
  public string Street { get; set; }
  public string City { get; set; }
  public string State { get; set; }
  public string ZipCode { get; set; }
  public string UserId { get; set; }
  public User User { get; set; }
}
