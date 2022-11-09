using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity;

public class User : IdentityUser
{
  public string Name { get; set; }
  public Address Address { get; set; }
}
