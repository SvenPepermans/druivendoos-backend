using DruivendoosAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DruivendoosAPI.Data.Mapping
{
    public class CustomerWineConfiguration : IEntityTypeConfiguration<CustomerWine>
    {
        public void Configure(EntityTypeBuilder<CustomerWine> builder)
        {
            builder.ToTable("CustomerWine");

            builder.HasKey(kw => new { kw.CustomerId, kw.WineId });

            builder.HasOne(kw => kw.Customer)
                .WithMany(w => w.Wines)
                .HasForeignKey(kw => kw.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(kw => kw.Wine)
                .WithMany(cw => cw.CustomerWines)
                .HasForeignKey(w => w.WineId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
