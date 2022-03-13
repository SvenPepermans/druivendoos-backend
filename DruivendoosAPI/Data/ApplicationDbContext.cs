using DruivendoosAPI.Data.Mapping;
using DruivendoosAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DruivendoosAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Wine> Wines { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerWine> CustomersWines { get; set; }
        public DbSet<Box> Boxes { get; set; }
        public DbSet<WineBox> WinesBoxes { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<BoxOfTheMonth> BoxesOfTheMonth { get; set; }
        public DbSet<Picture> Pictures { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new WineConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new BoxConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerWineConfiguration());
            modelBuilder.ApplyConfiguration(new WineBoxConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriptionConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
            modelBuilder.ApplyConfiguration(new BoxOfTheMonthConfiguration());
            modelBuilder.ApplyConfiguration(new PictureConfiguration());

        }
    }
}
