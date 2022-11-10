using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Identity;

namespace API.Extensions;

public static class MigrationExtentions
{
  public static async Task<IServiceScope> MigrateAndSeedAsync(this IServiceScope scope)
  {
    try
    {
      var context = scope.ServiceProvider.GetRequiredService<DataContext>();
      var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
      var identityContext = scope.ServiceProvider.GetRequiredService<IdentityDataContext>();

      await context.Database.MigrateAsync();
      await identityContext.Database.MigrateAsync();
      await DbSeeder.SeedAsync(context);
      await IdentityDbSeed.SeedUsersAsync(userManager);
    }
    catch (Exception ex)
    {
      var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
      logger.LogError(ex, "Problem migrating and seeding data");
    }

    return scope;
  }

}
