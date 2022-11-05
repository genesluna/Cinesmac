using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DataContext : DbContext
{
  public DataContext(DbContextOptions options) : base(options) { }

  public DbSet<Movie> Movies { get; set; }
  public DbSet<ScreeningRoom> ScreeningRooms { get; set; }
  public DbSet<Session> Sessions { get; set; }
  public DbSet<Ticket> Tickets { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Session>()
        .HasOne<Movie>(s => s.Movie)
        .WithMany(m => m.Sessions)
        .HasForeignKey(s => s.MovieId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Session>()
        .HasOne<ScreeningRoom>(s => s.ScreeningRoom)
        .WithMany(sr => sr.Sessions)
        .HasForeignKey(s => s.ScreeningRoomId)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<Ticket>()
        .HasOne<Session>(t => t.Session)
        .WithMany(s => s.Tickets)
        .HasForeignKey(t => t.SessionId)
        .OnDelete(DeleteBehavior.Restrict);
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
