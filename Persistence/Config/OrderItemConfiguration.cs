using System.Security.Cryptography;
using Domain.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
  public void Configure(EntityTypeBuilder<OrderItem> builder)
  {
    builder.OwnsOne(i => i.OrderedItem, oi => { oi.WithOwner(); });
    builder.Property(i => i.Price).IsRequired();
    builder.Property(i => i.Quantity).IsRequired();
  }
}
