using DruivendoosAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DruivendoosAPI.Data.Mapping
{
    public class BoxOfTheMonthConfiguration : IEntityTypeConfiguration<BoxOfTheMonth>
    {
        public void Configure(EntityTypeBuilder<BoxOfTheMonth> builder)
        {
            builder.ToTable("BoxOfTheMonth");
            builder.HasKey(b => b.BoxOfTheMonthId);

            builder.Property(b => b.CreatedAt).IsRequired();

            builder.HasMany(b => b.Wines).WithOne();
        }
    }
}
