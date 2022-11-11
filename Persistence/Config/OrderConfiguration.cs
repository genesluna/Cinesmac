using Domain.Entities.Enums;
using Domain.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
  public void Configure(EntityTypeBuilder<Order> builder)
  {
    builder.OwnsOne(o => o.Address, a => { a.WithOwner(); });
    builder.Property(s => s.Status)
    .HasConversion(
        o => o.ToString(),
        o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o)
    );

    builder.Property(o => o.BuyerId).IsRequired();
    builder.Property(o => o.Status).IsRequired();
    builder.Property(o => o.SubTotal).IsRequired();

    builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
  }
}
