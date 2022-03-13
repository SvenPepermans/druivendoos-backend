using DruivendoosAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DruivendoosAPI.Data.Mapping
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payment");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.CreatedAt)
                .IsRequired(false);

            builder.Property(p => p.Status)
                .IsRequired();

            builder.HasOne(p => p.Order).WithMany();
        }
    }
}
