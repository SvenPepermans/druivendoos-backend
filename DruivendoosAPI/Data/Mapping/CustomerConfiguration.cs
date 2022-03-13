using DruivendoosAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DruivendoosAPI.Data.Mapping
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");

            builder.HasKey(k => k.CustomerId);

            #region Properties

            builder.Property(k => k.FirstName)
                .IsRequired();

            builder.Property(k => k.LastName)
                .IsRequired();

            builder.Property(k => k.Email)
                .IsRequired();

            builder.Property(k => k.TelephoneNumber)
                .IsRequired();

            builder.Property(k => k.City)
                .IsRequired();

            builder.Property(k => k.Street)
                .IsRequired();

            builder.Property(k => k.HouseNumber)
                .IsRequired();

            builder.Property(k => k.PostalCode)
                .IsRequired();
            #endregion

            builder.HasMany(k => k.Boxes).WithOne();
        }
    }
}
