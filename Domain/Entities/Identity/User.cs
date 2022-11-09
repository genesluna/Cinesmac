using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity;

public class User : IdentityUser
{
  public User()
  {
  }

  public User(string name, string email, string userName)
  {
    Name = name;
    Email = email;
    UserName = userName;
  }

  public string Name { get; set; }
  public Address Address { get; set; }
}
