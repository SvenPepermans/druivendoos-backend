using DruivendoosAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DruivendoosAPI.Data.Mapping
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Supplier");
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Name).IsRequired();
            builder.Property(l => l.Email).IsRequired();

            builder.HasMany(l => l.Wines).WithOne(w => w.Supplier);
        }
    }
}
