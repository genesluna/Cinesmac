using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config;

public class SessionConfigutarion : IEntityTypeConfiguration<Session>
{
  public void Configure(EntityTypeBuilder<Session> builder)
  {
    builder.HasOne<Movie>(s => s.Movie)
        .WithMany(m => m.Sessions)
        .HasForeignKey(s => s.MovieId)
        .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne<ScreeningRoom>(s => s.ScreeningRoom)
        .WithMany(sr => sr.Sessions)
        .HasForeignKey(s => s.ScreeningRoomId)
        .OnDelete(DeleteBehavior.Restrict);
  }
}
