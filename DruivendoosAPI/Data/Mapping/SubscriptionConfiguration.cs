using DruivendoosAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DruivendoosAPI.Data.Mapping
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.ToTable("Subscription");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Type)
                .IsRequired();
            builder.Property(s => s.Length)
                .IsRequired();
            builder.Property(s => s.StartDate)
                .IsRequired();
            builder.Property(s => s.EndDate)
                .IsRequired();

            builder.HasMany(s => s.Boxes);
        }
    }
}
