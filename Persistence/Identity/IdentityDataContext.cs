using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Identity;

public class IdentityDataContext : IdentityDbContext<User>
{
  public IdentityDataContext(DbContextOptions<IdentityDataContext> options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.Entity<Address>()
        .Property(a => a.UserId)
        .IsRequired();

    base.OnModelCreating(builder);
  }
}
