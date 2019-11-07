namespace P03_SalesDatabase.Data
{
    using Microsoft.EntityFrameworkCore;
    using P03_SalesDatabase.Data.Models;
    using System;

    public class SalesDbContext : DbContext
    {
        public SalesDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public SalesDbContext()
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity
                    .HasKey(p => p.ProductId);

                entity
                    .Property(n => n.Name)
                    .HasMaxLength(50)
                    .IsUnicode(true)
                    .IsRequired(true);

                entity
                    .Property(q => q.Quantity)
                    .HasColumnType("DECIMAL(18,2)")
                    .IsRequired(true);

                entity
                    .Property(p => p.Price)
                    .HasColumnType("DECIMAL(18,2)")
                    .IsRequired(true);

                entity
                    .Property(d => d.Description)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("'No description'");
            });

            modelBuilder.Entity<Product>()
                .HasData(new Product { ProductId = 1, Name = "Cheese", Quantity = 222, Price = 1.5m },
                         new Product { ProductId = 2, Name = "Tomato", Quantity = 100, Price = 0.5m },
                         new Product { ProductId = 3, Name = "Paprika", Quantity = 500, Price = 1.6m });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity
                    .HasKey(c => c.CustomerId);

                entity
                    .Property(n => n.Name)
                    .HasMaxLength(100)
                    .IsUnicode(true)
                    .IsRequired(true);

                entity
                    .Property(e => e.Email)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .IsRequired(false);

                entity
                    .Property(cn => cn.CreditCardNumber)
                    .IsRequired(true);
            });

            modelBuilder.Entity<Customer>()
               .HasData(new Customer { CustomerId = 1, Name = "Pesho", Email = "pesho@abv.bg", CreditCardNumber = "1563595185" },
                        new Customer { CustomerId = 2, Name = "Ivan", Email = "ivan@abv.bg", CreditCardNumber = "849841852526" },
                        new Customer { CustomerId = 3, Name = "Mimi", Email = "mimi@abv.bg", CreditCardNumber = "896189856281" });

            modelBuilder.Entity<Store>(entity =>
            {
                entity
                    .HasKey(s => s.StoreId);

                entity
                    .Property(n => n.Name)
                    .HasMaxLength(80)
                    .IsUnicode(true)
                    .IsRequired(true);
            });

            modelBuilder.Entity<Store>()
              .HasData(new Store { StoreId = 1, Name = "Billa"},
                       new Store { StoreId = 2, Name = "Kaufland" },
                       new Store { StoreId = 3, Name = "Lidl" });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity
                    .HasKey(s => s.SaleId);

                entity
                    .Property(d => d.Date)
                    .HasColumnType("DATETIME2")
                    .HasDefaultValueSql("GETDATE()")
                    .IsRequired(true);

                entity
                    .HasOne(s => s.Product)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(s => s.ProductId);

                entity
                    .HasOne(s => s.Customer)
                    .WithMany(c => c.Sales)
                    .HasForeignKey(s => s.CustomerId);

                entity
                    .HasOne(s => s.Store)
                    .WithMany(st => st.Sales)
                    .HasForeignKey(s => s.StoreId);
            });

            modelBuilder.Entity<Sale>()
             .HasData(new Sale { SaleId = 1, CustomerId = 1, ProductId = 2, StoreId = 3, Date = DateTime.Now},
                      new Sale { SaleId = 2, CustomerId = 2, ProductId = 3, StoreId = 1, Date = DateTime.Now},
                      new Sale { SaleId = 3, CustomerId = 3, ProductId = 1, StoreId = 2, Date = DateTime.Now });
        }
    }
}
