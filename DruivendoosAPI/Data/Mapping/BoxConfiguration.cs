using DruivendoosAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DruivendoosAPI.Data.Mapping
{
    public class BoxConfiguration : IEntityTypeConfiguration<Box>
    {
        public void Configure(EntityTypeBuilder<Box> builder)
        {
            builder.ToTable("Box");
            builder.HasKey(d => d.Id);

            #region Properties
            builder.Property(d => d.City)
               .IsRequired();

            builder.Property(d => d.CustomerId)
                .IsRequired();

            builder.Property(d => d.Street)
                .IsRequired();

            builder.Property(d => d.HouseNumber)
                .IsRequired();

            builder.Property(d => d.PostalCode)
                .IsRequired();
            builder.Property(d => d.IsSent).IsRequired();

            builder.Property(d => d.CreatedAt)
                .IsRequired();
            #endregion

        }
    }
}
