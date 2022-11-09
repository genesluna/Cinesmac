using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions;

public static class UserManagerExtensions
{
  public static async Task<User> FindByEmailWithAddressAsync(this UserManager<User> input, string email)
  {
    return await input.Users.Include(u => u.Address).SingleOrDefaultAsync(u => u.Email == email);
  }
}
