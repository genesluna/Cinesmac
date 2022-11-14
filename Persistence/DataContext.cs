using System.Reflection;
using Domain.Entities;
using Domain.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Persistence;

public class DataContext : DbContext
{
  public DataContext(DbContextOptions<DataContext> options) : base(options) { }

  public DbSet<Movie> Movies { get; set; }
  public DbSet<ScreeningRoom> ScreeningRooms { get; set; }
  public DbSet<Session> Sessions { get; set; }
  public DbSet<Order> Orders { get; set; }
  public DbSet<OrderItem> OrderItems { get; set; }
  public DbSet<DeliveryMethod> DeliveryMethods { get; set; }


  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
    {
      foreach (var entityType in modelBuilder.Model.GetEntityTypes())
      {
        var properties = entityType.ClrType
              .GetProperties()
              .Where(p => p.PropertyType == typeof(DateTimeOffset)
                     || p.PropertyType == typeof(DateTimeOffset?));

        foreach (var property in properties)
        {
          modelBuilder
              .Entity(entityType.Name)
              .Property(property.Name)
              .HasConversion(new DateTimeOffsetToBinaryConverter());
        }
      }
    }

  }

  public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    var insertedEntries = this.ChangeTracker.Entries()
        .Where(x => x.State == EntityState.Added)
        .Select(x => x.Entity);

    foreach (var insertedEntry in insertedEntries)
    {
      var baseEntity = insertedEntry as BaseEntity;

      if (baseEntity != null)
        baseEntity.CreatedAt = DateTimeOffset.UtcNow;
    }

    var modifiedEntries = this.ChangeTracker.Entries()
        .Where(x => x.State == EntityState.Modified)
        .Select(x => x.Entity);

    foreach (var modifiedEntry in modifiedEntries)
    {
      var baseEntity = modifiedEntry as BaseEntity;

      if (baseEntity != null)
        baseEntity.UpdatedAt = DateTimeOffset.UtcNow;
    }

    return base.SaveChangesAsync(cancellationToken);
  }
}
