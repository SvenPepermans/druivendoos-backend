using DruivendoosAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DruivendoosAPI.Data.Mapping
{
    public class WineBoxConfiguration : IEntityTypeConfiguration<WineBox>
    {
        public void Configure(EntityTypeBuilder<WineBox> builder)
        {
            builder.ToTable("WineBox");

            builder.HasKey(wd => new { wd.WineId, wd.BoxId });

            builder.HasOne(wd => wd.Wine)
                .WithMany(w => w.WineBoxes)
                .HasForeignKey(w => w.WineId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(wd => wd.Box)
                .WithMany(b => b.Wines)
                .HasForeignKey(d => d.BoxId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
