using DruivendoosAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DruivendoosAPI.Data.Mapping
{
    public class WineConfiguration : IEntityTypeConfiguration<Wine>
    {
        public void Configure(EntityTypeBuilder<Wine> builder)
        {

            builder.ToTable("Wine");

            builder.HasKey(w => w.WineId);

            #region Properties

            builder.Property(w => w.GrapeVariety)
                .IsRequired();

            builder.Property(w => w.Story)
                .IsRequired();

            builder.Property(w => w.GrapeColor)
                .IsRequired();

            builder.Property(w => w.Year)
                .IsRequired();

            builder.Property(w => w.GrapeCountry)
                 .IsRequired();

            builder.Property(w => w.Rating);

            builder.Property(w => w.WineName)
                .IsRequired();

            builder.Property(w => w.GrapeDomain)
                .IsRequired();

            #endregion

            //Relaties
            builder.HasMany(w => w.Reviews)
                .WithOne()
                .HasForeignKey("WineId");

            builder.HasOne(w => w.Supplier)
                .WithMany(l => l.Wines)
                .IsRequired();

            builder.HasOne(w => w.Image).WithOne().HasForeignKey<Wine>(w => w.ImageId);
        }
    }
}
