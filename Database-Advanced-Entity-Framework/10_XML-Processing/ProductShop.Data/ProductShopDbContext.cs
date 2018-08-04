﻿using System;
using Microsoft.EntityFrameworkCore;
using ProductShop.Models;

namespace ProductShop.Data
{
    public class ProductShopDbContext : DbContext
    {
        public ProductShopDbContext()
        {
        }

        public ProductShopDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryProduct>().HasKey(x => new {x.CategoryId, x.ProductId});

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasMany(x => x.ProductsSold)
                    .WithOne(x => x.Seller)
                    .HasForeignKey(x => x.SellerId);

                entity.HasMany(x => x.ProductsBought)
                    .WithOne(x => x.Buyer)
                    .HasForeignKey(x => x.BuyerId);
            });
        }
    }
}
