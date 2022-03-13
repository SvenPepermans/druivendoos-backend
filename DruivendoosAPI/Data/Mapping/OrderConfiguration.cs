using DruivendoosAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DruivendoosAPI.Data.Mapping
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(o => o.Id);

            builder.Property(o => o.CustomerId)
                .IsRequired();

            builder.Property(o => o.Amount).HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(o => o.Description)
                .IsRequired();

            builder.Property(o => o.MolliePaymentId)
                .IsRequired(false);

            builder.Property(o => o.MollieOrderId)
                .IsRequired(false);
            builder.HasOne(o => o.Subscription).WithOne(s => s.Order).HasForeignKey<Subscription>(s => s.OrderId).IsRequired(false);

            builder.HasOne(o => o.Invoice).WithOne(i => i.Order).IsRequired(false);




        }
    }
}
