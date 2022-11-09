using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Identity;

public class IdentityDbSeed
{
  public static async Task SeedUsersAsync(UserManager<User> userManager)
  {
    if (!userManager.Users.Any())
    {
      var user = new User
      {
        Name = "Genes Luna",
        Email = "genesluna@gmail.com",
        UserName = "genesluna@gmail.com",
        Address = new Address
        {
          Street = "Av. Fernandes Lima, 823",
          City = "Maceió",
          State = "Alagoas",
          ZipCode = "57000-000"
        }
      };

      await userManager.CreateAsync(user, "Pa$$w0rd");
    }
  }
}
