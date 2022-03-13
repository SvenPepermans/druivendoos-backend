using DruivendoosAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DruivendoosAPI.Data.Mapping
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoice");
            builder.HasKey(i => i.Id);

            builder.Property(i => i.CustomerId)
                .IsRequired();
            builder.Property(i => i.CompanyName)
                .IsRequired();
            builder.Property(i => i.Street)
                .IsRequired();
            builder.Property(i => i.HouseNumber)
                .IsRequired();
            builder.Property(i => i.City)
                .IsRequired();
            builder.Property(i => i.PostalCode)
                .IsRequired();
            builder.Property(i => i.VATNumber)
                .IsRequired();
            builder.Property(i => i.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            builder.Property(i => i.Date)
                .IsRequired();


        }
    }
}
