using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data
{
    public class SalesContext : DbContext
    {
        public SalesContext()
        {
        }

        public SalesContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<Store> Stores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.Connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(
                entity =>
                {
                    entity.HasKey(x => x.ProductId);

                    entity.Property(x => x.Name).IsRequired().IsUnicode().HasMaxLength(50);

                    entity.Property(x => x.Price).IsRequired();

                    entity.Property(x => x.Quantity).IsRequired();

                    entity.HasMany(x => x.Sales);

                    entity.Property(x => x.Description).IsRequired(false).IsUnicode()
                        .HasMaxLength(250).HasDefaultValue("No description");
                }
            );

            modelBuilder.Entity<Customer>(
                entity =>
                {
                    entity.HasKey(x => x.CustomerId);

                    entity.Property(x => x.Name).IsRequired().IsUnicode().HasMaxLength(100);

                    entity.Property(x => x.Email).IsRequired().IsUnicode(false).HasMaxLength(80);

                    entity.Property(x => x.CreditCardNumber).IsRequired();
                }
            );

            modelBuilder.Entity<Store>(
                entity =>
                {
                    entity.HasKey(x => x.StoreId);

                    entity.Property(x => x.Name).IsRequired().IsUnicode().HasMaxLength(80);
                }
            );

            modelBuilder.Entity<Sale>(
                entity =>
                {
                    entity.HasKey(x => x.SaleId);

                    entity.Property(x => x.Date).IsRequired().HasColumnType("DATETIME2")
                        .HasDefaultValueSql("GETDATE()");

                    entity.HasOne(x => x.Product).WithMany(x => x.Sales)
                        .HasForeignKey(x => x.ProductId).HasConstraintName("FK_Product_Sales");

                    entity.HasOne(e => e.Customer).WithMany(p => p.Sales)
                        .HasForeignKey(p => p.CustomerId)
                        .HasConstraintName("FK_Sales_Customer");

                    entity.HasOne(e => e.Store).WithMany(p => p.Sales).HasForeignKey(p => p.StoreId)
                        .HasConstraintName("FK_Sales_Store");
                }
            );
        }
    }
}
