﻿using Microsoft.EntityFrameworkCore;
using WebApplicationShopPlatform.Catalog.Models;

namespace WebApplicationShopPlatform.Catalog.Data
{
    public class ProductDatabaseContext : DbContext
    {
        public ProductDatabaseContext(DbContextOptions<ProductDatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>()
                   .HasIndex(u => u.Name)
                   .IsUnique();
        }

        public DbSet<Product> Products { get; set; }
    }
}
