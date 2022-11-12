using Application.Users.Dtos;

namespace Application.Orders.Dtos;

public class OrderAddressDto
{
  public string Street { get; set; }
  public string City { get; set; }
  public string State { get; set; }
  public string ZipCode { get; set; }
}
